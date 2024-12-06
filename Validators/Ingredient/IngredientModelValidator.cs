using FluentValidation;
using WebApplication1.Models.ControllersIn.Ingredient;

namespace WebApplication1.Validators.Ingredient
{
    public class IngredientModelValidator : AbstractValidator<IngredientModel>
    {
        public IngredientModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Вкажіть назву")
                .Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9'\\s-]+$").WithMessage("Назва може містити лише букви та цифри")
                .MaximumLength(50).WithMessage("Назва має бути не довше 50 символів");

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Вкажіть одиницю виміру")
                .Matches("^[A-ZА-ЯІЄЇa-zа-яієї]+$").WithMessage("Одиниця виміру може містити лише букви")
                .MaximumLength(10).WithMessage("Одиниця виміру може бути не довше 10 символів");
        }
    }
}
