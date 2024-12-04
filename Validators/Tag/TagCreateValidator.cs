using FluentValidation;
using WebApplication1.Models.ControllersIn.Tag;

namespace WebApplication1.Validators.Tag
{
    public class TagCreateValidator : AbstractValidator<TagCreateModel>
    {
        public TagCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Вкажіть назву")
                .MaximumLength(30).WithMessage("Назва має бути не довше 30 символів")
                .Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9']+$").WithMessage("Назва може містити лише букви та цифри");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Вкажіть опис")
                .MaximumLength(500).WithMessage("Опис має бути не довше 500 символів")
                .Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9\\s,\\.!?:;'-]+$").WithMessage("Опис містить недопустимі символи");
        }
    }
}
