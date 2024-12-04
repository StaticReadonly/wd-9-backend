using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DbConn;
using WebApplication1.Exceptions;
using WebApplication1.Models.ControllersIn.Menu;
using WebApplication1.Models.ControllersOut;
using WebApplication1.Models.Entities;
using WebApplication1.Repositories.Abstraction;

namespace WebApplication1.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly DbContext1 _context;
        private readonly IMapper _mapper;

        public MenuRepository(
            DbContext1 context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateMenu(MenuCreateModel model, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            using var transaction = await _context.Database.BeginTransactionAsync(token);

            Menu newMenu = _mapper.Map<Menu>(model);

            IEnumerable<Menu_Dish> menuDish = model.Dishes.Select(x => new Menu_Dish()
            {
                Menu = newMenu,
                Dish_ID = x
            });

            await _context.Menus.AddAsync(newMenu, token);
            await _context.Menu_Dishes.AddRangeAsync(menuDish, token);

            await _context.SaveChangesAsync(token);

            await transaction.CommitAsync(token);
        }

        public async Task<MenuInfo> GetInfo(Guid menuId, CancellationToken token)
        {
            Menu? menu = await _context.Menus
                .Include(x => x.DishesRel)
                .ThenInclude(y => y.Dish)
                .FirstOrDefaultAsync(x => x.ID == menuId, token);

            if (menu == null)
                throw new EntityNotFoundException("Меню не знайдено");

            MenuInfo result = new()
            {
                Name = menu.Name,
                Description = menu.Description,
                DishesInfo = menu.DishesRel.Select(x => new DishShortInfo()
                {
                    Id = x.Dish.ID,
                    Name = x.Dish.Name,
                    Description = x.Dish.Description
                })
            };

            return result;
        }

        public Task<IEnumerable<MenuSearchInfo>> Search(MenuSearchModel model, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var menus = _context.Menus.Where(x => x.Name.ToLower().Contains(model.Query.ToLower()))
                .AsEnumerable();

            var result = _mapper.Map<IEnumerable<MenuSearchInfo>>(menus);

            return Task.FromResult(result);
        }
    }
}
