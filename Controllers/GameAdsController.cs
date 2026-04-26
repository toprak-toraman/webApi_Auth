using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using wepAPI_denemeler.DTOs;
using wepAPI_denemeler.Extensions;
using wepAPI_denemeler.Interfaces;
using wepAPI_denemeler.Models;

namespace wepAPI_denemeler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameAdsController : BaseController<GameAdsController>
    {
        private readonly IGameAdService _gameAdService;

        public GameAdsController(ILogger<GameAdsController> logger, IGameAdService gameAdService) : base(logger)
        {
            _gameAdService = gameAdService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryParams @params)
        {
            var ads = await _gameAdService.GetAllAsync(@params);
            return Ok(ads);
        }

        [HttpGet("game/{gameId}")]
        public async Task<IActionResult> GetByGame(int gameId)
        {
            var ads = await _gameAdService.GetAdsByGameIdAsync(gameId);
            return Ok(ads);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var ads = await _gameAdService.GetAdsByUserIdAsync(userId);
            return Ok(ads);
        }

        [Authorize]
        [HttpPost("create/{gameId}")]
        public async Task<IActionResult> CreateAd(int gameId, [FromBody] CreateGameAdDto dto)
        {
            var userId = User.GetUserId(); // Tertemiz!
            if (userId == 0) return Unauthorized();

            var newAd = new GameAd
            {
                GameId = gameId,
                UserId = userId,
                Title = dto.Title,
                Description = dto.Description
            };

            var result = await _gameAdService.AddAsync(newAd);
            return result ? Ok("İlan başarıyla oluşturuldu.") : BadRequest("İlan eklenemedi.");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ad = await _gameAdService.GetByIdAsync(id);
            if (ad == null) return NotFound("İlan bulunamadı.");

            var userId = User.GetUserId();
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (ad.UserId != userId && userRole != "Admin")
                return Forbid("Bu ilanı silme yetkiniz yok.");

            await _gameAdService.DeleteAsync(id);
            return Ok("İlan silindi.");
        }
    }
}