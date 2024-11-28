using FluentValidation;
using WebApplication1.Models.ControllersIn;

namespace WebApplication1.Validators.Comment
{
    public class CommentPostValidator : AbstractValidator<CommentPostModel>
    {
        public CommentPostValidator()
        {

        }
    }
}
