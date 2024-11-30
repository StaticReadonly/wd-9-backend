using FluentValidation;
using WebApplication1.Models.ControllersIn;

namespace WebApplication1.Validators.Comment
{
    public class CommentPostValidator : AbstractValidator<CommentPostModel>
    {
        public CommentPostValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Вкажіть текст коментаря")
                .MaximumLength(500).WithMessage("Текст коментаря не може перевищувати 500 символів");
        }
    }
}
