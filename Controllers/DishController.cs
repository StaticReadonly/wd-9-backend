using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Exceptions;
using WebApplication1.Models.ControllersIn;
using WebApplication1.Models.ControllersOut;
using WebApplication1.Repositories.Abstraction;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IValidator<DishModel> _dishModelValidator;
        private readonly IValidator<DishSearchModel> _dishSearchModelValidator;
        private readonly IDishRepository _dishRepository;

        public DishController(
            IValidator<DishModel> dishModelValidator,
            IDishRepository dishRepository,
            IValidator<DishSearchModel> dishSearchModelValidator)
        {
            _dishModelValidator = dishModelValidator;
            _dishRepository = dishRepository;
            _dishSearchModelValidator = dishSearchModelValidator;
        }

        [HttpPost("create")]
        [Authorize("AdminOnly")]
        public async Task<IActionResult> CreateDish([FromBody] DishModel model)
        {
            var validationRes = _dishModelValidator.Validate(model);

            if (!validationRes.IsValid)
            {
                throw new ControllerInModelException(validationRes);
            }

            try
            {
                await _dishRepository.CreateDish(model, HttpContext.RequestAborted);
                return Ok();
            }
            catch(DbUpdateException)
            {
                return BadRequest("Виникла помилка при додаванні страви");
            }
        }

        [HttpPost("info/{id:guid}")]
        public async Task<IActionResult> GetInfo([FromRoute] Guid id)
        {
            DishInfo info = await _dishRepository.DishInfo(id, HttpContext.RequestAborted);
            return Ok(info);
        }

        [HttpPost("search")]
        [Authorize("AdminOnly")]
        public async Task<IActionResult> SearchDishes([FromBody] DishSearchModel model)
        {
            var validationRes = _dishSearchModelValidator.Validate(model);

            if (!validationRes.IsValid)
            {
                throw new ControllerInModelException(validationRes);
            }

            var result = await _dishRepository.SearchDish(model, HttpContext.RequestAborted);
            return Ok(result);
        }
    }
}
