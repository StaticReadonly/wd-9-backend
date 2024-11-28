using WebApplication1.Models.ControllersOut;

namespace WebApplication1.Repositories.Abstraction
{
    public interface ICommentRepository
    {
        Task<IEnumerable<DishComment>> GetComments(Guid dishId, CancellationToken token);
    }
}
