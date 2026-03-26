using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using wepAPI_denemeler.DTOs;
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

        // 1. TÜM İLANLARI GETİR (Filtreleme, Sıralama ve Sayfalama ile)
        // [FromQuery] sayesinde URL'deki parametreleri QueryParams nesnesine eşler
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryParams @params)
        {
            // Artık servise @params nesnesini gönderiyoruz
            var ads = await _gameAdService.GetAllAsync(@params);
            return Ok(ads);
        }

        // 2. OYUNA ÖZEL İLANLAR 
        [HttpGet("game/{gameId}")]
        public async Task<IActionResult> GetByGame(int gameId)
        {
            // Not: İleride istersen buraya da @params ekleyip 
            // sadece o oyunun ilanlarını sayfalı getirebiliriz.
            var ads = await _gameAdService.GetAdsByGameIdAsync(gameId);
            return Ok(ads);
        }

        // 3. KULLANICIYA ÖZEL İLANLAR
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var ads = await _gameAdService.GetAdsByUserIdAsync(userId);
            return Ok(ads);
        }

        // 4. YENİ İLAN OLUŞTUR 
        [Authorize]
        [HttpPost("create/{gameId}")]
        public async Task<IActionResult> CreateAd(int gameId, [FromBody] CreateGameAdDto dto)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out int userId)) return Unauthorized();

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

        // 5. İLAN SİL
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ad = await _gameAdService.GetByIdAsync(id);
            if (ad == null) return NotFound("İlan bulunamadı.");

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (ad.UserId != userId && userRole != "Admin")
                return Forbid("Bu ilanı silme yetkiniz yok.");

            await _gameAdService.DeleteAsync(id);
            return Ok("İlan silindi.");
        }
    }
}