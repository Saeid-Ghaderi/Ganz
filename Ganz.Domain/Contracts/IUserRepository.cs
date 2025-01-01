using Ganz.Domain.Enttiies.Identity;
namespace Ganz.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<bool> IsUsernameTakenAsync(string username);
        Task InsertUserAsync(User user);
        public Task<User> SingleOrDefaultAsync(string userName);
        public Task<UserRefreshToken> SingleOrDefaultUserRefreshTokenAsync(string refreshToken);
    }
}
