namespace wepAPI_denemeler.DTOs
{
    public class ProfileUpdateDto
    {
        public string Username { get; set; } = default!;
        public string? Bio { get; set; }
    }
}