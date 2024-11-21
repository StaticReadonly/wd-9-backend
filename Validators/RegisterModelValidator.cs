using FluentValidation;
using WebApplication1.Models.ControllersIn;

namespace WebApplication1.Validators
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator() 
        {
            RuleFor(x => x.First_Name).NotEmpty().Matches("^[A-ZА-ЯІЄЇ]{1}[a-zа-яієї]{1,99}$")
                .WithMessage("Невірне ім'я");

            RuleFor(x => x.Last_Name).NotEmpty().Matches("^[A-ZА-ЯІЄЇ]{1}[a-zа-яієї]{1,99}$")
                .WithMessage("Невірна фамілія");

            RuleFor(x => x.Email).NotEmpty().Matches("^[a-z]+(\\.[a-z]+)*@[a-z]+(\\.[a-z]+)+$")
                .WithMessage("Невірна пошта");

            RuleFor(x => x.Password).NotEmpty().Matches("^[a-zA-Z0-9!@#$%^&*()-=+\'\\\";:.,_]{6,100}$")
                .WithMessage("Невірний пароль");

        }
    }
}
