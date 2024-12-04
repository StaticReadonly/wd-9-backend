using FluentValidation;
using WebApplication1.Models.ControllersIn.Comment;

namespace WebApplication1.Validators.Comment
{
    public class CommentEditValidator : AbstractValidator<CommentEditModel>
    {
        public CommentEditValidator()
        {
            RuleFor(x => x.NewText)
                .NotEmpty().WithMessage("Вкажіть текст коментаря")
                .MaximumLength(500).WithMessage("Текст коментаря не може перевищувати 500 символів");
        }
    }
}
