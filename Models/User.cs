using wepAPI_denemeler.DTOs;

namespace wepAPI_denemeler.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;

        // YENİ ALANLAR

        public int? Age { get; set; }
        public string? Bio { get; set; }
        public string? Gender { get; set; }
        public string? Role { get; internal set; }

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