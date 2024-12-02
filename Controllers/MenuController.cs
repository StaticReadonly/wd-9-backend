using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IValidator<MenuCreateModel> _menuCreateModelValidator;
        private readonly IValidator<MenuSearchModel> _menuSearchModelValidator;

        public MenuController(
            IMenuRepository menuRepository,
            IValidator<MenuCreateModel> menuCreateModelValidator,
            IValidator<MenuSearchModel> menuSearchModelValidator)
        {
            _menuRepository = menuRepository;
            _menuCreateModelValidator = menuCreateModelValidator;
            _menuSearchModelValidator = menuSearchModelValidator;
        }

        [HttpPost("{menuId:guid}")]
        public async Task<IActionResult> Info([FromRoute] Guid menuId)
        {
            MenuInfo result = await _menuRepository.GetInfo(menuId, HttpContext.RequestAborted);
            return Ok(result);
        }

        [HttpPost("create")]
        [Authorize("AdminOnly")]
        public async Task<IActionResult> Create([FromBody] MenuCreateModel model)
        {
            var validationRes = _menuCreateModelValidator.Validate(model);

            if (!validationRes.IsValid)
            {
                throw new ControllerInModelException(validationRes);
            }

            try
            {
                await _menuRepository.CreateMenu(model, HttpContext.RequestAborted);
                return Ok();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Виникла помилка при додаванні меню");
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] MenuSearchModel model)
        {
            var validationRes = _menuSearchModelValidator.Validate(model);

            if (!validationRes.IsValid)
            {
                throw new ControllerInModelException(validationRes);
            }

            var result = await _menuRepository.Search(model, HttpContext.RequestAborted);
            return Ok(result);
        }
    }
}
