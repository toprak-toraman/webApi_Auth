namespace wepAPI_denemeler.DTOs
{
    public class UserUpdateDto
    {
        // Burada isim 'Id' (Büyük I) olmalı ki uyumlu olsun
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Password { get; set; } // Şifre değişmeyecekse boş geçilebilir
        public int? Age { get; set; }
        public string? Bio { get; set; }
        public string? Gender { get; set; }
    }
}