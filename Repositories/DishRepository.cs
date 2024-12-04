using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using WebApplication1.DbConn;
using WebApplication1.Exceptions;
using WebApplication1.Models.ControllersIn.Dish;
using WebApplication1.Models.ControllersOut;
using WebApplication1.Models.Entities;
using WebApplication1.Repositories.Abstraction;

namespace WebApplication1.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly DbContext1 _context;
        private readonly IMapper _mapper;

        public DishRepository(
            DbContext1 context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateDish(DishModel model, CancellationToken token)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(token);

            Dish newDish = _mapper.Map<Dish>(model);
            Recipe newRecipe = new()
            {
                Dish = newDish,
                Time = TimeSpan.Parse(model.Time)
            };

            IEnumerable<Dish_Tag> newTags = model.Tags
                .Select(x => new Dish_Tag() { Dish = newDish, Tag_ID = x });

            IEnumerable<Component> newComponents = model.Components
                .Select(x => new Component() {Recipe = newRecipe, Ingredient_ID = x.ID, Amount = x.Amount });

            IEnumerable<Cooking> newCooking = model.Steps
                .Select(x => new Cooking()
                {
                    Step_Number = x.Step_number,
                    Recipe = newRecipe,
                    Step = new Step()
                    {
                        Name = x.Name,
                        Description = x.Description
                    }
                });

            await _context.Dishes.AddAsync(newDish, token);
            await _context.Recipies.AddAsync(newRecipe, token);
            await _context.Cookings.AddRangeAsync(newCooking, token);
            await _context.Components.AddRangeAsync(newComponents, token);
            await _context.Dish_Tags.AddRangeAsync(newTags, token);

            await _context.SaveChangesAsync(token);

            await transaction.CommitAsync(token);
        }

        public async Task<DishInfo> DishInfo(Guid id, CancellationToken token)
        {
            Dish? dish = await _context.Dishes
                .Include(x => x.Recipe)
                .ThenInclude(y => y.IngredientRel)
                .ThenInclude(z => z.Ingredient)
                .Include(x => x.Recipe)
                .ThenInclude(y => y.StepRel)
                .ThenInclude(z => z.Step)
                .Include(x => x.TagsRel)
                .ThenInclude(y => y.Tag)
                .FirstOrDefaultAsync(x => x.ID == id, token);

            if (dish == null)
                throw new EntityNotFoundException("Страву не знайдено");

            DishInfo dishInfo = new()
            {
                Name = dish.Name,
                Description = dish.Description,
                Time = dish.Recipe.Time.ToString(@"hh\:mm\:ss"),
                Tags = dish.TagsRel.Select(x => x.Tag.Name),
                Ingredients = dish.Recipe.IngredientRel.Select(x => new DishIngredientInfo()
                {
                    Name = x.Ingredient.Name,
                    Amount = x.Amount,
                    Unit = x.Ingredient.Unit
                }),
                Steps = dish.Recipe.StepRel.Select(x => new DishStepInfo()
                {
                    Name = x.Step.Name,
                    Description = x.Step.Description,
                    Number = x.Step_Number
                })
            };

            return dishInfo;
        }

        public Task<IEnumerable<DishShortInfo>> SearchDish(DishSearchModel model, CancellationToken token)
        {
            var dishes = _context.Dishes.Where(x => x.Name.ToLower().Contains(model.Query.ToLower()))
                .AsEnumerable();

            var result = _mapper.Map<IEnumerable<DishShortInfo>>(dishes);

            return Task.FromResult(result);
        }
    }
}
