using FluentValidation;
using WebApplication1.Models.ControllersIn.Menu;

namespace WebApplication1.Validators.Menu
{
    public class MenuCreateValidator : AbstractValidator<MenuCreateModel>
    {
        public MenuCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Вкажіть назву")
                .Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9'\\s]+").WithMessage("Назва може містити лише букви та цифри")
                .MaximumLength(30).WithMessage("Назва має бути не довше 30 симвлоів"); ;

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Вкажіть опис")
                .MaximumLength(500).WithMessage("Опис має бути не довше 500 симвлоів"); ;

            RuleFor(x => x.Dishes)
                .NotEmpty().WithMessage("Вкажіть страви");
        }
    }
}
