using Microsoft.AspNetCore.Mvc;
using wepAPI_denemeler.Interfaces;
using wepAPI_denemeler.Models;

namespace wepAPI_denemeler.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : BaseController<GamesController>
    {
        private readonly IGameService _gameService;

        public GamesController(ILogger<GamesController> logger, IGameService gameService) : base(logger)
        {
            _gameService = gameService;
        }

        // GET: api/games
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var games = await _gameService.GetAllAsync();
            return Ok(games);
        }

        // POST: api/games
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Oyun adı boş olamaz.");

            var newGame = new Game { Name = name };
            var result = await _gameService.AddAsync(newGame);

            if (!result)
                return BadRequest("Oyun eklenirken bir hata oluştu.");

            return Ok(newGame);
        }
    }
}