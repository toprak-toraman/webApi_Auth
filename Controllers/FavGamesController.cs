using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wepAPI_denemeler.Extensions;
using wepAPI_denemeler.Interfaces;

namespace wepAPI_denemeler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavGamesController : BaseController<FavGamesController>
    {
        private readonly IFavGameService _favGameService;

        public FavGamesController(ILogger<FavGamesController> logger, IFavGameService favGameService) : base(logger)
        {
            _favGameService = favGameService;
        }

        [HttpPost("add/{gameId}")]
        public async Task<IActionResult> AddFavorite(int gameId)
        {
            var userId = User.GetUserId();
            if (userId == 0) return Unauthorized();

            var result = await _favGameService.AddToFavoritesAsync(userId, gameId);

            return result ? Ok("Oyun favorilerine eklendi.") : BadRequest("Bu oyun zaten favorilerinde veya bir hata oluştu.");
        }

        [HttpDelete("remove/{gameId}")]
        public async Task<IActionResult> RemoveFavorite(int gameId)
        {
            var userId = User.GetUserId(); // Burası da düzeldi
            if (userId == 0) return Unauthorized();

            var result = await _favGameService.RemoveFromFavoritesAsync(userId, gameId);
            return result ? Ok("Favorilerden çıkarıldı.") : BadRequest("Hata oluştu.");
        }
    }
}