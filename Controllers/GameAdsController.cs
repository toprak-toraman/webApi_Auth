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

        // 1. TÜM İLANLARI GETİR (Admin veya Genel Liste İçin)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _gameAdService.GetAllAsync());
        }

        // 2. OYUNA ÖZEL İLANLAR (Oyun sayfasında gösterilecek)
        // Örn: api/GameAds/game/5
        [HttpGet("game/{gameId}")]
        public async Task<IActionResult> GetByGame(int gameId)
        {
            var ads = await _gameAdService.GetAdsByGameIdAsync(gameId);
            return Ok(ads);
        }

        // 3. KULLANICIYA ÖZEL İLANLAR (Profil sayfasında gösterilecek)
        // Örn: api/GameAds/user/3
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var ads = await _gameAdService.GetAdsByUserIdAsync(userId);
            return Ok(ads);
        }

        // 4. YENİ İLAN OLUŞTUR (Giriş şart, ID'ler otomatik)
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

        // 5. İLAN SİL (Sadece ilanı veren veya Admin silebilir)
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Önce ilanı bulalım ki sahibi kim bakalım
            var ad = await _gameAdService.GetByIdAsync(id);
            if (ad == null) return NotFound("İlan bulunamadı.");

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            // Güvenlik: İlan senin değilse ve Admin değilsen silemezsin!
            if (ad.UserId != userId && userRole != "Admin")
                return Forbid("Bu ilanı silme yetkiniz yok.");

            await _gameAdService.DeleteAsync(id);
            return Ok("İlan silindi.");
        }
    }
}