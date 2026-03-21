using Microsoft.EntityFrameworkCore;
using wepAPI_denemeler.Data;
using wepAPI_denemeler.Interfaces;
using wepAPI_denemeler.Models;

namespace wepAPI_denemeler.Services
{
    public class GameAdService : BaseService<GameAd>, IGameAdService
    {
        public GameAdService(AppDbContext context, ILogger<GameAdService> logger) : base(context, logger)
        {
        }

        // Genel Liste (Admin için her şeyi getirir)
        public override async Task<List<GameAd>> GetAllAsync()
        {
            return await _context.GameAds
                .Include(x => x.Game)
                .Include(x => x.User)
                .ToListAsync();
        }

        // --- YENİ EKLENEN FİLTRELER ---

        public async Task<List<GameAd>> GetAdsByGameIdAsync(int gameId)
        {
            return await _context.GameAds
                .Where(x => x.GameId == gameId)
                .Include(x => x.User) // İlanı kimin verdiğini görsek yeter, oyun zaten belli
                .ToListAsync();
        }

        public async Task<List<GameAd>> GetAdsByUserIdAsync(int userId)
        {
            return await _context.GameAds
                .Where(x => x.UserId == userId)
                .Include(x => x.Game) // Hangi oyun için ilan vermiş, onu görmeliyiz
                .ToListAsync();
        }
    }
}