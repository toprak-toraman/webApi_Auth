using Microsoft.AspNetCore.Mvc;
using wepAPI_denemeler.DTOs;
using wepAPI_denemeler.Interfaces;

namespace wepAPI_denemeler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            var result = await _authService.RegisterAsync(request);
            if (result == "UsernameTaken") return BadRequest("Kullanıcı adı alınmış.");
            if (result == "EmailTaken") return BadRequest("Email zaten kayıtlı.");
            return Ok("Başarıyla kaydedildi.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var token = await _authService.LoginAsync(request);
            if (token == null) return BadRequest("Hatalı kullanıcı adı veya şifre.");
            return Ok(new { Token = token });
        }
    }
}