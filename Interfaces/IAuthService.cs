using wepAPI_denemeler.DTOs;

namespace wepAPI_denemeler.Interfaces

{
    public interface IAuthService
    {
        Task<string> RegisterAsync(UserRegisterDto request);
        Task<string> LoginAsync(UserLoginDto request);
    }
}
