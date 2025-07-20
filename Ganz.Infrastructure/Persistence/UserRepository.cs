using Ganz.Domain;
using Ganz.Domain.Contracts;
using Ganz.Domain.Enttiies.Identity;
using Ganz.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ganz.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _applicationDbContext;


        public UserRepository(ApplicationDBContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task InsertUserAsync(User user)
        {
            await _applicationDbContext.Set<User>().AddAsync(user);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            return await _applicationDbContext.Set<User>().AnyAsync(u => u.UserName == username);
        }

        public async Task<User> SingleOrDefaultAsync(string userName)
        {
            var user = await _applicationDbContext.Set<User>().FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
                throw new Exception("User Not Found");

            return user;
        }

        public async Task<UserRefreshToken> SingleOrDefaultUserRefreshTokenAsync(string refreshToken)
        {

            var userRefreshToken = await _applicationDbContext.Set<UserRefreshToken>().FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (userRefreshToken == null)
                throw new Exception("User Not Found");

            return userRefreshToken;
        }

    }
}
