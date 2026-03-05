using Microsoft.EntityFrameworkCore;
using wepAPI_denemeler.Common.Enums;
using wepAPI_denemeler.Data;
using wepAPI_denemeler.DTOs;
using wepAPI_denemeler.Interfaces;
using wepAPI_denemeler.Models;

namespace wepAPI_denemeler.Services
{
    public class AuthService : BaseService<User>, IAuthService
    {
        private readonly ITokenService _tokenService;

        public AuthService(AppDbContext context, ILogger<AuthService> logger, ITokenService tokenService)
            : base(context, logger)
        {
            _tokenService = tokenService;
        }

        public async Task<AuthResult> RegisterAsync(UserRegisterDto request)
        {
            // _context artık BaseService'den geliyor
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                return AuthResult.UsernameTaken;

            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return AuthResult.EmailTaken;

            var user = User.CreateFrom(request);

            return await AddAsync(user) ? AuthResult.Success : AuthResult.InvalidCredentials;
        }

        public async Task<(AuthResult Result, string? Token)> LoginAsync(UserLoginDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return (AuthResult.InvalidCredentials, null);

            var token = _tokenService.CreateToken(user);
            return (AuthResult.Success, token);
        }
    }
}