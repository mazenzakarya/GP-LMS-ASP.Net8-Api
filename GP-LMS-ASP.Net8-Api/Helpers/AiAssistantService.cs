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

            if (student == null) return "لم يتم العثور على الطالب.";

            var group = student.StudentGroups.FirstOrDefault()?.Group;
            var course = group?.Course;
            var courseName = course?.Name ?? "غير محدد";
            var groupName = group?.Name ?? "غير محدد";

            // المدرس من جدول المستخدمين حسب الـ Role
            var teacher = await _db.Users.FirstOrDefaultAsync(u =>
                u.UserId == group.TeacherId && u.Role == "Instructour");

            var teacherName = teacher?.Name ?? "غير معروف";

            var totalUnpaid = student.Fees
                .Where(f => f.Status != FeeStatus.Paid && !f.IsDeleted)
                .Sum(f => f.NetAmount);

            // عدد الحضور والغياب
            var attendanceCount = await _db.Attendances
                .CountAsync(a => a.StudentId == studentId && a.Status == AttendanceStatus.Present);

            var absenceCount = await _db.Attendances
                .CountAsync(a => a.StudentId == studentId && a.Status == AttendanceStatus.Absent);

            var lateCount = await _db.Attendances
                .CountAsync(a => a.StudentId == studentId && a.Status == AttendanceStatus.Late);

            // ملاحظات المعلمين
            var teacherNotes = await _db.Grades
                .Where(n => n.StudentId == studentId)
                .Select(n => n.Notes)
                .ToListAsync();

            // الدرجات
            var grades = await _db.Grades
                .Where(g => g.StudentId == studentId)
                .ToListAsync();

            double avgGrade = grades.Any() ? grades.Average(g => g.Score) : 0;

            // التنبيه حسب الغياب
            string absenceAlert = absenceCount > 2
                ? "⚠️ تنبيه: عدد الغيابات تجاوز حصتين. يُرجى التواصل مع الإدارة."
                : "";

            var prompt = $"""
                Student: {student.Name}
                Course: {courseName}
                User Name: {student.Username}
                Group: {groupName}
                Instructor: {teacherName}
                Outstanding Fees: {totalUnpaid} EGP

                Attendance Count: {attendanceCount}
                Absence Count: {absenceCount}
                LateCount: {lateCount}
                {absenceAlert}

                Teacher Notes:
                {string.Join("\n", teacherNotes)}

                Grades:
                {string.Join(", ", grades.Select(g => $"{g.Subject}: {g.Score}"))}
                Average Score: {avgGrade}

                Question: {question}

                Please answer using the same language as the question, and provide a brief evaluation of the student's performance if applicable.
                """;

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
                return "error while connectiong from our side please contact us on whatsapp or by calling";

            var responseStream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(responseStream);

            try
            {
                var answer = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return answer ?? "لم يتم الحصول على رد.";
            }
            catch
            {
                return "لم يتم فهم رد Gemini.";
            }
        }

        public async Task<string> PublicAnswerAsync(string question)
        {
            var prompt = $"""
                You are an intelligent assistant representing an educational platform called "UC math kids" teaches english and math 01143921476 contact number main branch Katameya new cairo.
                Your role is to answer visitor questions clearly and professionally.
                Always reply in the same language as the question (Arabic or English), using a friendly and helpful tone.

                Question: {question}
                """;

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
                return "error from our side please contact us on whatsapp or by calling 01143921476";

            var responseStream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(responseStream);

            try
            {
                var answer = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return answer ?? "لم يتم الحصول على رد.";
            }
            catch
            {
                return "فشل قراءة الرد.";
            }
        }
    }
}