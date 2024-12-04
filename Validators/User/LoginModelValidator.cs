using FluentValidation;
using WebApplication1.Models.ControllersIn.User;

namespace WebApplication1.Validators.User
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Вкажіть пошту")
                .Matches("^[a-z]+(\\.[a-z]+)*@[a-z]+(\\.[a-z]+)+$").WithMessage("Невірна пошта")
                .MaximumLength(100).WithMessage("Пошта має бути не довше 100 символів");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Вкажіть пароль")
                .Matches("^[a-zA-Z0-9!@#$%^&*()-=+\'\\\";:.,_]+$").WithMessage("Пароль містить недопустимі символи")
                .MinimumLength(6).WithMessage("Пароль має бути не коротше 6 символів")
                .MaximumLength(100).WithMessage("Пароль має бути не довше 100 миволів");
        }
    }
}
