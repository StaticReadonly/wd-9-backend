using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Exceptions;
using WebApplication1.Models.ControllersIn;
using WebApplication1.Repositories.Abstraction;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IValidator<DishModel> _dishModelValidator;
        private readonly IDishRepository _dishRepository;

        public DishController(
            IValidator<DishModel> dishModelValidator, 
            IDishRepository dishRepository)
        {
            _dishModelValidator = dishModelValidator;
            _dishRepository = dishRepository;
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
            catch(DbUpdateException exc)
            {
                return BadRequest("Виникла помилка при додаванні страви");
            }
        }
    }
}
