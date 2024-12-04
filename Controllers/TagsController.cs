using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Exceptions;
using WebApplication1.Models.ControllersIn.Tag;
using WebApplication1.Repositories.Abstraction;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        private readonly IValidator<TagCreateModel> _createValidator;
        private readonly IValidator<TagSearchModel> _searchValidator;

        public TagsController(
            ITagRepository tagRepository, 
            IValidator<TagCreateModel> createValidator, 
            IValidator<TagSearchModel> searchValidator)
        {
            _tagRepository = tagRepository;
            _createValidator = createValidator;
            _searchValidator = searchValidator;
        }

        [HttpPost("create")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateTag([FromBody] TagCreateModel model)
        {
            var validationRes = _createValidator.Validate(model);

            if (!validationRes.IsValid)
            {
                throw new ControllerInModelException(validationRes);
            }

            try
            {
                var tag = await _tagRepository.CreateTag(model, HttpContext.RequestAborted);
                return Ok(tag);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Тег уже існує");
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchTags([FromBody] TagSearchModel model)
        {
            var validationRes = _searchValidator.Validate(model);

            if (!validationRes.IsValid)
            {
                throw new ControllerInModelException(validationRes);
            }

            var result = await _tagRepository.SearchTags(model, HttpContext.RequestAborted);

            return Ok(result);
        }
    }
}
