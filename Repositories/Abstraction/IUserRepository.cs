using WebApplication1.Models.ControllersIn;
using WebApplication1.Models.Entities;

namespace WebApplication1.Repositories.Abstraction
{
    public interface IUserRepository
    {
        Task<User> UserLogin(LoginModel model, CancellationToken token);
        Task UserRegister(User user, CancellationToken token);
        Task<User> UserInfo(CancellationToken token);
    }
}
