using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplication1.DbConn;
using WebApplication1.Exceptions;
using WebApplication1.Models.ControllersIn;
using WebApplication1.Models.Entities;
using WebApplication1.Options;
using WebApplication1.Repositories.Abstraction;
using WebApplication1.Services.PasswordHasher;

namespace WebApplication1.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext1 _context;
        private readonly HttpContext _httpContext;
        private readonly ClaimsOptions _claimsOptions;
        private readonly IPasswordHasher _passwordHasher;

        public UserRepository(
            DbContext1 context,
            IHttpContextAccessor contextAccessor,
            IOptions<ClaimsOptions> claimsOptions,
            IPasswordHasher passwordHasher)
        {
            _context = context;
            _claimsOptions = claimsOptions.Value;
            _httpContext = contextAccessor.HttpContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> UserInfo(CancellationToken token)
        {
            var claims = _httpContext.User.Claims;
            var id = claims.First(c => c.Type == _claimsOptions.ID).Value;

            User user = await _context.Users.FirstAsync(u => u.ID == Guid.Parse(id), token);

            return user;
        }

        public async Task<User> UserLogin(LoginModel model, CancellationToken token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email, token);

            if (user == null)
            {
                throw new ControllerInModelException("Email", "Неіснуюча пошта");
            }

            if (_passwordHasher.Compare(model.Password, user.Password))
            {
                return user;
            }
            else
            {
                throw new ControllerInModelException("Password", "Невірний пароль");
            }
        }

        public async Task UserRegister(User user, CancellationToken token)
        {
            user.Password = _passwordHasher.Hash(user.Password);

            await _context.Users.AddAsync(user, token);

            try
            {
                await _context.SaveChangesAsync(token);
            }
            catch(DbUpdateException)
            {
                throw new ControllerInModelException("Email", "Користувач вже існує");
            }
        }
    }
}
