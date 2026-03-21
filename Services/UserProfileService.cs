using wepAPI_denemeler.Data;
using wepAPI_denemeler.DTOs;
using wepAPI_denemeler.Interfaces;
using wepAPI_denemeler.Models;

namespace wepAPI_denemeler.Services
{
    public class UserProfileService : BaseService<User>, IUserProfileService
    {
        public UserProfileService(AppDbContext context, ILogger<BaseService<User>> logger) : base(context, logger)
        {
        }
        public async Task<bool> UpdateMyProfile(int userId, ProfileUpdateDto dto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.Username = dto.Username;
            user.Bio = dto.Bio;

            // Yaş, Cinsiyet vb. alanlara dokunmuyoruz!
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
