using FluentValidation;
using WebApplication1.Models.ControllersIn;

namespace WebApplication1.Validators
{
    public class IngredientModelValidator : AbstractValidator<IngredientModel>
    {
        public IngredientModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9]{1,50}$")
                .WithMessage("Невірне ім'я");
            RuleFor(x => x.Unit).NotEmpty().Matches("^[A-ZА-ЯІЄЇa-zа-яієї]{1,10}$")
                .WithMessage("Невірна одиниця виміру");
        }
    }
}
