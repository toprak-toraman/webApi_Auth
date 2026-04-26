using Microsoft.EntityFrameworkCore;
using wepAPI_denemeler.Data;
using wepAPI_denemeler.DTOs; // QueryParams için bunu eklemelisin
using wepAPI_denemeler.Interfaces;
using wepAPI_denemeler.Models;

namespace wepAPI_denemeler.Services
{
    public class GameAdService : BaseService<GameAd>, IGameAdService
    {
        public GameAdService(AppDbContext context, ILogger<GameAdService> logger) : base(context, logger)
        {
        }


        public override async Task<List<GameAd>> GetAllAsync(QueryParams @params)
        {
            _logger.LogInformation("İlanlar oyun ve kullanıcı bilgileriyle birlikte getiriliyor...");

            // BaseService'deki o meşhur sorgu başlangıcını yapıyoruz
            IQueryable<GameAd> query = _context.GameAds
                .Include(x => x.Game)
                .Include(x => x.User);



            if (@params.IsPaginationEnabled)
            {
                int skip = (@params.PageNumber - 1) * @params.PageSize;
                query = query.Skip(skip).Take(@params.PageSize);
            }

            return await query.ToListAsync();
        }


        public async Task<List<GameAd>> GetAdsByGameIdAsync(int gameId)
        {
            return await _context.GameAds
                .Where(x => x.GameId == gameId)
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<List<GameAd>> GetAdsByUserIdAsync(int userId)
        {
            return await _context.GameAds
                .Where(x => x.UserId == userId)
                .Include(x => x.Game)
                .ToListAsync();
        }
    }
}