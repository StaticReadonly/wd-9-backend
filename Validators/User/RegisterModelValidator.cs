using FluentValidation;
using WebApplication1.Models.ControllersIn.User;

namespace WebApplication1.Validators.User
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.First_Name)
                .NotEmpty().WithMessage("Вкажіть ім'я")
                .Matches("^[A-ZА-ЯІЄЇ]{1}[a-zа-яієї']+$").WithMessage("Ім'я може містити тільки букви та апостроф")
                .MaximumLength(50).WithMessage("Ім'я має бути не довше 50 символів");

            RuleFor(x => x.Last_Name)
                .NotEmpty().WithMessage("Вкажіть фамілію")
                .Matches("^[A-ZА-ЯІЄЇ]{1}[a-zа-яієї']+$").WithMessage("Фамілія може містити тільки букви та апостроф")
                .MaximumLength(50).WithMessage("Фамілія має бути не довше 50 символів");

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
