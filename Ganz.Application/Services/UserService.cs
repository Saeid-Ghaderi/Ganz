using Ganz.Application.Interfaces;
using Ganz.Domain.Contracts;
using Ganz.Domain.Enttiies.Identity;

namespace Ganz.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task AddUserAsync(User user)
        {
            await _userRepository.InsertUserAsync(user);
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
          return await _userRepository.IsUsernameTakenAsync(username);
        }
    }
}
