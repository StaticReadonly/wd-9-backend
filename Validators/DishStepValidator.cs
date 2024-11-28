using FluentValidation;
using WebApplication1.Models.ControllersIn;

namespace WebApplication1.Validators
{
    public class DishStepValidator : AbstractValidator<DishStep>
    {
        public DishStepValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Вкажіть назву")
                .Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9'\\s]+$").WithMessage("Назва може містити лише букви та цифри")
                .MaximumLength(30).WithMessage("Назва має бути не довше 30 символів");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Вкажіть опис")
                .Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9\\s,\\.!?:;'-]+$").WithMessage("Опис містить недопустимі символи")
                .MaximumLength(500).WithMessage("Опис має бути не довше 500 символів");

            RuleFor(x => x.Step_number)
                .NotEmpty().WithMessage("Вкажіть номер кроку")
                .LessThan(100).WithMessage("Номер кроку не може перевищувати 100");
        }
    }
}
