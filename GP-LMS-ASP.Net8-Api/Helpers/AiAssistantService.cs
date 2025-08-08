using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using GP_LMS_ASP.Net8_Api.Context;
using GP_LMS_ASP.Net8_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GP_LMS_ASP.Net8_Api.Helpers
{
    public class AiAssistantService
    {
        private readonly MyContext _db;
        private readonly IHttpClientFactory _httpFactory;
        private readonly IConfiguration _config;

        public AiAssistantService(MyContext db, IHttpClientFactory httpFactory, IConfiguration config)
        {
            _db = db;
            _httpFactory = httpFactory;
            _config = config;
        }

        public async Task<string> AnswerQuestionAsync(int studentId, string question)
        {
            var student = await _db.Users
                .Include(u => u.StudentGroups)
                    .ThenInclude(sg => sg.Group)
                        .ThenInclude(g => g.Course)
                .Include(u => u.Fees)
                .FirstOrDefaultAsync(u => u.UserId == studentId);

            if (student == null) return "Student not found.";

            var groupInfos = new List<string>();
            foreach (var sg in student.StudentGroups)
            {
                var group = sg.Group;
                if (group == null) continue;

                var course = group.Course;
                var teacher = await _db.Users
                    .Where(u => u.UserId == group.TeacherId && u.Role == "Instructor")
                    .FirstOrDefaultAsync();

                var groupName = group.Name ?? "N/A";
                var courseName = course?.Name ?? "N/A";
                var teacherName = teacher?.Name ?? "N/A";

                groupInfos.Add($"Group: {groupName}, Course: {courseName}, Instructor: {teacherName}");
            }

            var unpaidTotal = student.Fees
                .Where(f => f.Status != FeeStatus.Paid && !f.IsDeleted)
                .Sum(f => f.NetAmount);

            var attendanceCount = await _db.Attendances
                .CountAsync(a => a.StudentId == studentId && a.Status == AttendanceStatus.Present);

            var absenceCount = await _db.Attendances
                .CountAsync(a => a.StudentId == studentId && a.Status == AttendanceStatus.Absent);

            var lateCount = await _db.Attendances
                .CountAsync(a => a.StudentId == studentId && a.Status == AttendanceStatus.Late);

            var grades = await _db.Grades
                .Where(g => g.StudentId == studentId)
                .ToListAsync();

            var notes = grades.Select(g => g.Notes).Where(n => !string.IsNullOrWhiteSpace(n)).ToList();
            var avgGrade = grades.Any() ? grades.Average(g => g.Score) : 0;

            var prompt = $"""
                Student: {student.Name}
                Username: {student.Username}
                Outstanding Fees: {unpaidTotal} EGP

                Attendance:
                Present: {attendanceCount}
                Absent: {absenceCount}
                Late: {lateCount}

                Enrollments:
                {string.Join("\n", groupInfos)}

                Teacher Notes:
                {string.Join("\n", notes)}

                Grades:
                {string.Join(", ", grades.Select(g => $"{g.Subject}: {g.Score}"))}
                Average Grade: {avgGrade}

                Question: {question}

                Please answer in the same language as the question and include a brief evaluation of the student's academic and attendance performance.
                """;

            return await SendToGeminiAsync(prompt);
        }

        public async Task<string> PublicAnswerAsync(string question)
        {
            var prompt = $"""
                You are an intelligent assistant representing "UC math kids", a learning center teaching English and Math. Contact: 01143921476, located in Katameya, New Cairo.
                Respond clearly and professionally in the same language as the question, using a friendly and helpful tone.

                Question: {question}
                """;

            return await SendToGeminiAsync(prompt);
        }

        private async Task<string> SendToGeminiAsync(string prompt)
        {
            var client = _httpFactory.CreateClient();
            var apiKey = _config["Gemini:ApiKey"];

            var body = new
            {
                contents = new[]
                {
                    new {
                        parts = new[] {
                            new { text = prompt }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={apiKey}";
            var response = await client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
                return "API call failed. Please contact support.";

            var responseStream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(responseStream);

            try
            {
                return doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString()
                    ?? "No response from Gemini.";
            }
            catch
            {
                return "Failed to parse Gemini response.";
            }
        }
    }
}