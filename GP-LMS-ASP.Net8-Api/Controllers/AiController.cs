using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GP_LMS_ASP.Net8_Api.DTOs;
using GP_LMS_ASP.Net8_Api.Helpers;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace GP_LMS_ASP.Net8_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly AiAssistantService _assistant;

        public AiController(AiAssistantService assistant)
        {
            _assistant = assistant;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] AiChatRequest req)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized();

            if (!int.TryParse(userIdClaim, out var studentId)) return BadRequest("Invalid user id.");

            var answer = await _assistant.AnswerQuestionAsync(studentId, req.Question);
            return Ok(new { answer });
        }

        [AllowAnonymous]
        [HttpPost("publicChat")]
        public async Task<IActionResult> PublicChat([FromBody] AiChatRequest dto)
        {
            var answer = await _assistant.PublicAnswerAsync(dto.Question);
            return Ok(new { answer });
        }
    }
}