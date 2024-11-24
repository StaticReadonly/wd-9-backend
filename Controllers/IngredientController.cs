using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Exceptions;
using WebApplication1.Models.ControllersIn;
using WebApplication1.Repositories.Abstraction;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IValidator<IngredientModel> _ingredientModelValidator;
        private readonly IValidator<IngredientSearchModel> _searchModelValidator;
        private readonly IIngredientsRepository _ingredientsRepository;

        public IngredientController(
            IValidator<IngredientModel> validator,
            IIngredientsRepository ingredientsRepository,
            IValidator<IngredientSearchModel> searchModelValidator)
        {
            _ingredientModelValidator = validator;
            _ingredientsRepository = ingredientsRepository;
            _searchModelValidator = searchModelValidator;
        }

        [HttpPost("search")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> SearchIngredients([FromBody] IngredientSearchModel model)
        {
            var validationRes = _searchModelValidator.Validate(model);

            if (!validationRes.IsValid)
            {
                throw new ControllerInModelException(validationRes);
            }

            var result = await _ingredientsRepository.SearchIngredients(model, HttpContext.RequestAborted);

            return Ok(result);
        }

        [HttpPost("create")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientModel model)
        {
            var validationRes = _ingredientModelValidator.Validate(model);

            if (!validationRes.IsValid)
            {
                throw new ControllerInModelException(validationRes);
            }

            try
            {
                var ingredient = await _ingredientsRepository.CreateIngredient(model, HttpContext.RequestAborted);
                return Ok(ingredient);
            }
            catch(DbUpdateException)
            {
                return BadRequest("Інгредієнт уже існує");
            }
        }
    }
}
