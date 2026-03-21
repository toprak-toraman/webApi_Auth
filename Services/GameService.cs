using wepAPI_denemeler.Data;
using wepAPI_denemeler.Interfaces;
using wepAPI_denemeler.Models;

namespace wepAPI_denemeler.Services
{
    public class GameService : BaseService<Game>, IGameService
    {
        public GameService(AppDbContext context, ILogger<GameService> logger) : base(context, logger) { }
    }
}