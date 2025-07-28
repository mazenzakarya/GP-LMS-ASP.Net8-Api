using System.Text;

using GP_LMS_ASP.Net8_Api.Context;
using GP_LMS_ASP.Net8_Api.Helpers;

using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GP_LMS_ASP.Net8_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            // Add Hangfire services.
            builder.Services.AddHangfire(x => x.UseSqlServerStorage(
    builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddHangfireServer();

            //builder.Services.AddScoped<IPaymentCycleService>();

            // Add services to the container.
            builder.Services.AddDbContext<MyContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //swagger stuff
            builder.Services.AddSwaggerGen(options =>
            {
                //JWT Security Scheme
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token like: Bearer eyJhbGciOiJIUzI1..."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });

            //jwt stuff
            var key = Encoding.ASCII.GetBytes("ThisIsAVeryStrongSecretKeyForJwt123asdsae@@ad...!");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            //auth for jwt
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAll");
            app.UseHangfireDashboard();

            //// Register recurring job (every day at 2 AM for example)
            //RecurringJob.AddOrUpdate<IPaymentCycleService>(
            //    service => service.MarkStudentsAsUnpaidJob(),
            //    Cron.Daily(2));

            //         RecurringJob.AddOrUpdate<FeeStatusUpdater>(
            // "update-fee-status",
            // updater => updater.UpdateFeeStatusesAsync(),
            //Cron.Daily(2)
            //);
            app.Run();
        }
    }
}