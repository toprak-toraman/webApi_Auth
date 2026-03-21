namespace wepAPI_denemeler.DTOs
{
    public class ChangePasswordDto
    {
        public string OldPassword { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
    }
}