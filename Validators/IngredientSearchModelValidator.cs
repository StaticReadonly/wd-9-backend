using FluentValidation;
using WebApplication1.Models.ControllersIn;

namespace WebApplication1.Validators
{
    public class IngredientSearchModelValidator : AbstractValidator<IngredientSearchModel>
    {
        public IngredientSearchModelValidator() 
        {
            RuleFor(x => x.Query).NotEmpty().Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9]{3,50}$")
                .WithMessage("Введіть від 3 до 50 символів");
        }
    }
}
