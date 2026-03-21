using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using wepAPI_denemeler.Common.Enums;
using wepAPI_denemeler.DTOs;
using wepAPI_denemeler.Interfaces;

namespace wepAPI_denemeler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController<AuthController>
    {
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService) : base(logger)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            var result = await _authService.RegisterAsync(request);
            return result switch
            {
                AuthResult.Success => Ok("Kayıt başarılı."),
                AuthResult.UsernameTaken => BadRequest("Kullanıcı adı zaten var."),
                AuthResult.EmailTaken => BadRequest("Email zaten kayıtlı."),
                _ => BadRequest("Kayıt sırasında bir hata oluştu.")
            };
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var loginResult = await _authService.LoginAsync(request);
            if (loginResult.Result != AuthResult.Success)
                return BadRequest("Hatalı kullanıcı adı veya şifre.");

            return Ok(new { Token = loginResult.Token });
        }

        // --- YENİ EKLENEN ŞİFRE DEĞİŞTİRME ENDPOINT'İ ---
        [Authorize] // Sadece giriş yapanlar (Token'ı olanlar)
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto request)
        {
            // Token içinden giriş yapan kullanıcının ID'sini çekiyoruz
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out int userId)) return Unauthorized();

            var result = await _authService.ChangePasswordAsync(userId, request.OldPassword, request.NewPassword);

            return result switch
            {
                AuthResult.Success => Ok("Şifreniz başarıyla değiştirildi."),
                AuthResult.InvalidPassword => BadRequest("Eski şifreniz hatalı."),
                _ => BadRequest("Şifre değiştirilirken bir hata oluştu.")
            };
        }
    }
}