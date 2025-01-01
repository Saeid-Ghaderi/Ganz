using Ganz.Application.Dtos;

namespace Ganz.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> RegisterAsync(string username, string password);

        Task<LoginResponseDto> LoginAsync(string UserName, string Password);

        public Task<RefreshTokenResponse> GenerateNewToken(string Token, string RefreshToken);
    }
}
