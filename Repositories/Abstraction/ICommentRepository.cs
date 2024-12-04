using WebApplication1.Models.ControllersIn.Comment;
using WebApplication1.Models.ControllersOut;

namespace WebApplication1.Repositories.Abstraction
{
    public interface ICommentRepository
    {
        Task PostComment(CommentPostModel commentPost, Guid dishId, Guid userId, CancellationToken token);
        Task<IEnumerable<DishComment>> GetComments(Guid dishId, Guid userId, CancellationToken token);
        Task EditComment(CommentEditModel model, Guid commentId, Guid userId, CancellationToken token);
    }
}
