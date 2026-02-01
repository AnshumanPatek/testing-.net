using GainatelieCMS.API.DTOs;

namespace GainatelieCMS.API.Services;

public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(string email, string password);
    Task<AuthResponse?> RefreshTokenAsync(string refreshToken);
}
