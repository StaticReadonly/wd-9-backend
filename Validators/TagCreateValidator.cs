using FluentValidation;
using WebApplication1.Models.ControllersIn;

namespace WebApplication1.Validators
{
    public class TagCreateValidator : AbstractValidator<TagCreateModel>
    {
        public TagCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9]{1,30}$")
                .WithMessage("Невірне ім'я");
            RuleFor(x => x.Description).NotEmpty().Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9\\s,\\.!?:;-]{1,500}$")
                .WithMessage("Невірний опис");
        }
    }
}
