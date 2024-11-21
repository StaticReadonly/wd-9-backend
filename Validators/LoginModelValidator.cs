using FluentValidation;
using WebApplication1.Models.ControllersIn;

namespace WebApplication1.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().Matches("^[a-z]+(\\.[a-z]+)*@[a-z]+(\\.[a-z]+)+$")
                .WithMessage("Невірна пошта"); ;

            RuleFor(x => x.Password).NotEmpty().Matches("^[a-zA-Z0-9!@#$%^&*()-=+\'\\\";:.,_]{6,100}$")
                .WithMessage("Невріний пароль");
        }
    }
}
