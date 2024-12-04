using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Exceptions;
using WebApplication1.Models.ControllersIn.User;
using WebApplication1.Models.ControllersOut;
using WebApplication1.Models.Entities;
using WebApplication1.Repositories.Abstraction;
using WebApplication1.Services.ClaimsManager;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<LoginModel> _loginModelValidator;
        private readonly IValidator<RegisterModel> _registerModelValidator;
        private readonly ClaimsManager _claimsManager;

        public UserController(
            IUserRepository userRepository,
            IMapper mapper,
            IValidator<LoginModel> loginModelValidator,
            IValidator<RegisterModel> registerModelValidator,
            ClaimsManager claimsManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _loginModelValidator = loginModelValidator;
            _registerModelValidator = registerModelValidator;
            _claimsManager = claimsManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var validationResult = _loginModelValidator.Validate(model);

            if (!validationResult.IsValid)
            {
                throw new ControllerInModelException(validationResult);
            }

            User user = await _userRepository.UserLogin(model, HttpContext.RequestAborted);

            string jwtToken = _claimsManager.BuildJwtToken(user);

            UserInfo result = _mapper.Map<User, UserInfo>(user);

            return Ok(new
            {
                token = jwtToken,
                userInfo = result
            });
        }

        [HttpPost("logout")]
        [Authorize(Policy = "UserAuthorized")]
        public async Task<IActionResult> Logout()
        {
            //await HttpContext.SignOutAsync("Cookie");

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var validationResult = _registerModelValidator.Validate(model);

            if (!validationResult.IsValid)
            {
                throw new ControllerInModelException(validationResult);
            }

            User user = _mapper.Map<RegisterModel, User>(model);

            await _userRepository.UserRegister(user, HttpContext.RequestAborted);

            string jwtToken = _claimsManager.BuildJwtToken(user);

            UserInfo result = _mapper.Map<User, UserInfo>(user);

            return Ok(new
            {
                token = jwtToken,
                userInfo = result
            });
        }

        [HttpPost("info")]
        [Authorize(Policy = "UserAuthorized")]
        public async Task<IActionResult> Info()
        {
            User user = await _userRepository.UserInfo(HttpContext.RequestAborted);

            UserInfo result = _mapper.Map<User, UserInfo>(user);

            return Ok(result);
        }
    }
}
