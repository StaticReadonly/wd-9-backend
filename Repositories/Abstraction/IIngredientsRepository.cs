using WebApplication1.Models.ControllersIn.Ingredient;
using WebApplication1.Models.ControllersOut;
using WebApplication1.Models.Entities;

namespace WebApplication1.Repositories.Abstraction
{
    public interface IIngredientsRepository
    {
        Task<IEnumerable<IngredientInfo>> SearchIngredients(IngredientSearchModel model, CancellationToken token);
        Task<IngredientInfo> CreateIngredient(IngredientModel model, CancellationToken token);
    }
}
