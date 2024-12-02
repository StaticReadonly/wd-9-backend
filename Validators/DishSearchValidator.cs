using FluentValidation;
using WebApplication1.Models.ControllersIn;

namespace WebApplication1.Validators
{
    public class DishSearchValidator : AbstractValidator<DishSearchModel>
    {
        public DishSearchValidator()
        {
            RuleFor(x => x.Query)
                .NotEmpty().WithMessage("Вкажіть назву")
                .Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9'\\s]+").WithMessage("Назва може містити лише букви та цифри")
                .MaximumLength(50).WithMessage("Назва має бути не довше 50 симвлоів");
        }
    }
}
