using AutoMapper;
using WebApplication1.DbConn;
using WebApplication1.Models.ControllersIn.Ingredient;
using WebApplication1.Models.ControllersOut;
using WebApplication1.Models.Entities;
using WebApplication1.Repositories.Abstraction;

namespace WebApplication1.Repositories
{
    public class IngredientsRepository : IIngredientsRepository
    {
        private readonly IMapper _mapper;
        private readonly DbContext1 _context;

        public IngredientsRepository(
            IMapper mapper, 
            DbContext1 context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IngredientInfo> CreateIngredient(IngredientModel model, CancellationToken token)
        {
            Ingredient newIngredient = _mapper.Map<Ingredient>(model);

            await _context.Ingredients.AddAsync(newIngredient, token);
            await _context.SaveChangesAsync(token);

            return _mapper.Map<IngredientInfo>(newIngredient);
        }

        public Task<IEnumerable<IngredientInfo>> SearchIngredients(IngredientSearchModel model, CancellationToken token)
        {
            var res = _context.Ingredients.Where(x => x.Name.ToLower().Contains(model.Query.ToLower()))
                                            .AsEnumerable();

            token.ThrowIfCancellationRequested();

            return Task.FromResult(_mapper.Map<IEnumerable<IngredientInfo>>(res));
        }
    }
}
