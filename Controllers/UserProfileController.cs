using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wepAPI_denemeler.DTOs;
using wepAPI_denemeler.Extensions;
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

            var userId = User.GetUserId();
            if (userId == 0) return Unauthorized();

            var result = await _userProfileService.UpdateMyProfile(userId, dto);

            return result ? Ok("Profil bilgilerin güncellendi!") : BadRequest("Hata oluştu.");
        }
    }
}