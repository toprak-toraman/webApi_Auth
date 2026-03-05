using wepAPI_denemeler.Common.Enums;
using wepAPI_denemeler.DTOs;

namespace wepAPI_denemeler.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(UserRegisterDto request);
        Task<(AuthResult Result, string? Token)> LoginAsync(UserLoginDto request);
    }
}