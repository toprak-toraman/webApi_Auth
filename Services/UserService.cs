using wepAPI_denemeler.Data;
using wepAPI_denemeler.DTOs;
using wepAPI_denemeler.Interfaces;
using wepAPI_denemeler.Models;

namespace wepAPI_denemeler.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        // Constructor kısmında logger tipini düzelttik
        public UserService(AppDbContext context, ILogger<UserService> logger)
            : base(context, logger)
        {
        }

        // DİKKAT: Burada 'override' olmayacak, çünkü bu metod sadece bu sınıfa özel.
        public async Task<bool> UpdateUserAsync(int id, UserUpdateDto dto)
        {
            var existingUser = await GetByIdAsync(id);
            if (existingUser == null) return false;

            // Alanları güncelliyoruz
            existingUser.Username = dto.Username;
            existingUser.Email = dto.Email;
            existingUser.Age = dto.Age;
            existingUser.Bio = dto.Bio;
            existingUser.Gender = dto.Gender;

            // Şifre alanı doluysa hashleyip güncelliyoruz
            if (!string.IsNullOrEmpty(dto.Password))
            {
                existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            // BaseService'den gelen asıl UpdateAsync metodunu çağırıyoruz
            return await UpdateAsync(existingUser);
        }
    }
}