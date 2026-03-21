using wepAPI_denemeler.Models;

namespace wepAPI_denemeler.Interfaces
{
    public interface IGameAdService : IBaseService<GameAd>
    {
        // Bir oyuna ait tüm ilanları getir (Oyun sayfası için)
        Task<List<GameAd>> GetAdsByGameIdAsync(int gameId);

        // Bir kullanıcıya ait tüm ilanları getir (Profil sayfası için)
        Task<List<GameAd>> GetAdsByUserIdAsync(int userId);
    }
}