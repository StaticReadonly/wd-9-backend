using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.ControllersIn;
using WebApplication1.Repositories.Abstraction;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IValidator<CommentPostModel> _commentPostValidator;
        private readonly IValidator<CommentEditModel> _commentEditValidator;
        private readonly ICommentRepository _commentRepository;

        public CommentController(
            IValidator<CommentPostModel> commentPostValidator,
            IValidator<CommentEditModel> commentEditValidator,
            ICommentRepository commentRepository)
        {
            _commentEditValidator = commentEditValidator;
            _commentPostValidator = commentPostValidator;
            _commentRepository = commentRepository;
        }

        //[HttpPost("{id:guid}/post")]
        //public async Task<IActionResult> PostComment([FromBody] CommentPostModel model, [FromRoute] Guid id)
        //{

        //}

        [HttpPost("{id:guid}")]
        public async Task<IActionResult> GetCommets([FromRoute] Guid id)
        {
            if (id == default(Guid))
                return BadRequest("Вкажіть ідентифікатор");

            var comments = await _commentRepository.GetComments(id, HttpContext.RequestAborted);

            return Ok(comments);
        }

        //[HttpPut("{id:guid}/edit")]
        //public async Task<IActionResult> EditComment([FromBody]CommentEditModel model, [FromRoute] Guid id)
        //{

        //}
    }
}
