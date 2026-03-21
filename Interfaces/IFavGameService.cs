using wepAPI_denemeler.Models;

namespace wepAPI_denemeler.Interfaces
{
    public interface IFavGameService : IBaseService<FavGame>
    {
        Task<bool> AddToFavoritesAsync(int userId, int gameId);
        Task<bool> RemoveFromFavoritesAsync(int userId, int gameId);
        Task<List<FavGame>> GetUserFavoritesAsync(int userId);
    }
}