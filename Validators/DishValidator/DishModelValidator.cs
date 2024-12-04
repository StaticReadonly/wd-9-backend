using FluentValidation;
using WebApplication1.Models.ControllersIn.Dish;

namespace WebApplication1.Validators.DishValidator
{
    public class DishModelValidator : AbstractValidator<DishModel>
    {
        public DishModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Вкажіть назву")
                .Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9'\\s]+").WithMessage("Назва може містити лише букви та цифри")
                .MaximumLength(50).WithMessage("Назва має бути не довше 50 симвлоів");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Вкажіть опис")
                .MaximumLength(500).WithMessage("Опис має бути не довше 500 символів")
                .Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9\\s,\\.!?:;'-]+$").WithMessage("Опис містить недопустимі символи"); ;

            RuleFor(x => x.Time)
                .NotEmpty().WithMessage("Вкажіть час")
                .Matches("^[0-2][0-3]:[0-5][0-9]:[0-5][0-9]$").WithMessage("Невірний час");

            RuleFor(x => x.Steps).ForEach(x => x.SetValidator(new DishStepValidator()));

            RuleFor(x => x.Components).ForEach(x => x.SetValidator(new DishComponentValidator()));
        }
    }
}
