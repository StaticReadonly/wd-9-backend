using WebApplication1.Models.ControllersIn.Menu;
using WebApplication1.Models.ControllersOut;

namespace WebApplication1.Repositories.Abstraction
{
    public interface IMenuRepository
    {
        Task<MenuInfo> GetInfo(Guid menuId, CancellationToken token);
        Task CreateMenu(MenuCreateModel model, CancellationToken token);
        Task<IEnumerable<MenuSearchInfo>> Search(MenuSearchModel model, CancellationToken token);
    }
}
