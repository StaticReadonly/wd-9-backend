using FluentValidation;
using WebApplication1.Models.ControllersIn;

namespace WebApplication1.Validators
{
    public class TagSearchValidator : AbstractValidator<TagSearchModel>
    {
        public TagSearchValidator()
        {
            RuleFor(x => x.Query)
                .NotEmpty().WithMessage("Вкажіть запит")
                .MinimumLength(3).WithMessage("Запит має бути не коротше 3 символів")
                .MaximumLength(30).WithMessage("Запит має бути не довше 30 символів")
                .Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9']+$").WithMessage("Назва може містити лише цифри та букви");
        }
    }
}
