using Microsoft.AspNetCore.Authorization; // Gerekli kütüphane
using Microsoft.AspNetCore.Mvc;
using wepAPI_denemeler.DTOs;
using wepAPI_denemeler.Interfaces;
using wepAPI_denemeler.Models;

namespace wepAPI_denemeler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Bu satır tüm Controller'ı sadece Adminlere kilitler
    public class UserController : BaseController<UserController>
    {
        private readonly IBaseService<User> _service;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IBaseService<User> service, IUserService userService)
            : base(logger)
        {
            _service = service;
            _userService = userService;
        }

        // GET: api/user/users
        [HttpGet("users")]
        public async Task<ActionResult<List<User>>> GetAllAsync()
        {
            var users = await _service.GetAllAsync();
            return Ok(users);
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetByIdAsync(int id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserRegisterDto dto)
        {
            var user = wepAPI_denemeler.Models.User.CreateFrom(dto);

            var result = await _service.AddAsync(user);
            if (!result)
                return BadRequest("Kullanıcı eklenemedi.");

            return Ok(user);
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UserUpdateDto dto)
        {
            var result = await _userService.UpdateUserAsync(id, dto);

            if (!result)
                return NotFound("Kullanıcı bulunamadı veya güncellenemedi.");

            return NoContent();
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound("Kullanıcı bulunamadı veya silinemedi.");

            return NoContent();
        }
    }
}