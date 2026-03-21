using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using wepAPI_denemeler.Interfaces;

namespace wepAPI_denemeler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Favori eklemek için giriş şart!
    public class FavGamesController : BaseController<FavGamesController>
    {
        private readonly IFavGameService _favGameService;

        public FavGamesController(ILogger<FavGamesController> logger, IFavGameService favGameService) : base(logger)
        {
            _favGameService = favGameService;
        }

        // POST: api/FavGames/add/5 (5 nolu oyunu favoriye ekle)
        [HttpPost("add/{gameId}")]
        public async Task<IActionResult> AddFavorite(int gameId)
        {
            // Token'dan o anki kullanıcının ID'sini çekiyoruz
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out int userId)) return Unauthorized();

            var result = await _favGameService.AddToFavoritesAsync(userId, gameId);

            return result ? Ok("Oyun favorilerine eklendi.") : BadRequest("Bu oyun zaten favorilerinde veya bir hata oluştu.");
        }

        // DELETE: api/FavGames/remove/5 (Favoriden çıkar)
        [HttpDelete("remove/{gameId}")]
        public async Task<IActionResult> RemoveFavorite(int gameId)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out int userId)) return Unauthorized();

            var result = await _favGameService.RemoveFromFavoritesAsync(userId, gameId);
            return result ? Ok("Favorilerden çıkarıldı.") : BadRequest("Hata oluştu.");
        }
    }
}