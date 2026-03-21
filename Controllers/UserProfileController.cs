using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using wepAPI_denemeler.DTOs;
using wepAPI_denemeler.Interfaces;

namespace wepAPI_denemeler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserProfileController : BaseController<UserProfileController>
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(ILogger<UserProfileController> logger, IUserProfileService userProfileService)
            : base(logger)
        {
            _userProfileService = userProfileService;
        }

        [HttpPut("update-me")]
        public async Task<IActionResult> UpdateMe([FromBody] ProfileUpdateDto dto)
        {
            // Token'dan ID çekiyoruz (Yine o güvenli yol)
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
                return Unauthorized();

            var result = await _userProfileService.UpdateMyProfile(userId, dto);

            return result ? Ok("Profil bilgilerin güncellendi!") : BadRequest("Hata oluştu.");
        }
    }
}