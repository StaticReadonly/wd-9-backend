using FluentValidation;
using WebApplication1.Models.ControllersIn;

namespace WebApplication1.Validators
{
    public class IngredientSearchModelValidator : AbstractValidator<IngredientSearchModel>
    {
        public IngredientSearchModelValidator() 
        {
            RuleFor(x => x.Query)
                .NotEmpty().WithMessage("Вкажіть запит")
                .MinimumLength(3).WithMessage("Запит має бути не коротше 3 символів")
                .MaximumLength(50).WithMessage("Запит має бути не довше 50 символів")
                .Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9']+$").WithMessage("Назва може містити лише цифри та букви");
        }
    }
}
