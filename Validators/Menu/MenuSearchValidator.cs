using FluentValidation;
using WebApplication1.Models.ControllersIn.Menu;

namespace WebApplication1.Validators.Menu
{
    public class MenuSearchValidator : AbstractValidator<MenuSearchModel>
    {
        public MenuSearchValidator()
        {
            RuleFor(x => x.Query)
                .NotEmpty().WithMessage("Вкажіть назву")
                .Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9'\\s]+").WithMessage("Назва може містити лише букви та цифри")
                .MaximumLength(30).WithMessage("Назва має бути не довше 30 симвлоів");
        }
    }
}
