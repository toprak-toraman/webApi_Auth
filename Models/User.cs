using wepAPI_denemeler.DTOs;

namespace wepAPI_denemeler.Models
{
    public class User : BaseEntity // Madde 6: Artık BaseEntity'den miras alıyor
    {
        public string Username { get; set; } = default!;
        public string? Email { get; set; } // Madde 1: Nullable string
        public string PasswordHash { get; set; } = default!;

        // Madde 5: DTO'dan Model oluşturma mantığını buraya taşıyoruz
        public static User CreateFrom(UserRegisterDto dto)
        {
            return new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
        }
    }
}