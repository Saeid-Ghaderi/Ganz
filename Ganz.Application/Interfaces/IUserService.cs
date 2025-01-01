using Ganz.Domain.Enttiies.Identity;

namespace Ganz.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsUsernameTakenAsync(string username);
        Task AddUserAsync(User user);
    }
}
