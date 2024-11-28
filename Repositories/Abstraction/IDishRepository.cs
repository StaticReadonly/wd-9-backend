using WebApplication1.Models.ControllersIn;
using WebApplication1.Models.ControllersOut;
using WebApplication1.Models.Entities;

namespace WebApplication1.Repositories.Abstraction
{
    public interface IDishRepository
    {
        Task CreateDish(DishModel model, CancellationToken token);
        Task<DishInfo> DishInfo(Guid id, CancellationToken token);
    }
}
