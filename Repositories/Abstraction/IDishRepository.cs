using WebApplication1.Models.ControllersIn;

namespace WebApplication1.Repositories.Abstraction
{
    public interface IDishRepository
    {
        Task CreateDish(DishModel model, CancellationToken token);
    }
}
