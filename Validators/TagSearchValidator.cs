using FluentValidation;
using WebApplication1.Models.ControllersIn;

namespace WebApplication1.Validators
{
    public class TagSearchValidator : AbstractValidator<TagSearchModel>
    {
        public TagSearchValidator()
        {
            RuleFor(x => x.Query).NotEmpty().Matches("^[A-ZА-ЯІЄЇa-zа-яієї0-9]{3,30}$")
                .WithMessage("Введіть від 3 до 30 символів");
        }
    }
}
