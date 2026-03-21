using Microsoft.EntityFrameworkCore;
using wepAPI_denemeler.Data;
using wepAPI_denemeler.Interfaces;
using wepAPI_denemeler.Models;

namespace wepAPI_denemeler.Services
{
    public class FavGameService : BaseService<FavGame>, IFavGameService
    {
        public FavGameService(AppDbContext context, ILogger<FavGameService> logger) : base(context, logger)
        {
        }

        public async Task<bool> AddToFavoritesAsync(int userId, int gameId)
        {
            // Zaten favorilerinde var mı kontrolü
            var exists = await _context.FavGames.AnyAsync(x => x.UserId == userId && x.GameId == gameId);
            if (exists) return false;

            var fav = new FavGame { UserId = userId, GameId = gameId };
            return await AddAsync(fav);
        }

        public async Task<bool> RemoveFromFavoritesAsync(int userId, int gameId)
        {
            var fav = await _context.FavGames.FirstOrDefaultAsync(x => x.UserId == userId && x.GameId == gameId);
            if (fav == null) return false;

            return await DeleteAsync(fav.Id);
        }

        public async Task<List<FavGame>> GetUserFavoritesAsync(int userId)
        {
            return await _context.FavGames
                .Where(x => x.UserId == userId)
                .Include(x => x.Game) // Oyun detaylarını da getir (Ad, Kapak fotoğrafı vb.)
                .ToListAsync();
        }
    }
}