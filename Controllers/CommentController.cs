using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Exceptions;
using WebApplication1.Models.ControllersIn.Comment;
using WebApplication1.Repositories.Abstraction;
using WebApplication1.Services.ClaimsManager;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IValidator<CommentPostModel> _commentPostValidator;
        private readonly IValidator<CommentEditModel> _commentEditValidator;
        private readonly ICommentRepository _commentRepository;
        private readonly ClaimsManager _claimsManager;

        public CommentController(
            IValidator<CommentPostModel> commentPostValidator,
            IValidator<CommentEditModel> commentEditValidator,
            ICommentRepository commentRepository,
            ClaimsManager claimsManager)
        {
            _commentEditValidator = commentEditValidator;
            _commentPostValidator = commentPostValidator;
            _commentRepository = commentRepository;
            _claimsManager = claimsManager;
        }

        [HttpPost("{dishId:guid}/post")]
        [Authorize(Policy = "UserAuthorized")]
        public async Task<IActionResult> PostComment([FromBody] CommentPostModel model, [FromRoute] Guid dishId)
        {
            var validationRes = _commentPostValidator.Validate(model);

            if (!validationRes.IsValid)
            {
                throw new ControllerInModelException(validationRes);
            }

            if (dishId == default(Guid))
                return BadRequest("Вкажіть ідентифікатор");

            Guid userId = _claimsManager.GetCurrentUserID();

            await _commentRepository.PostComment(model, dishId, userId, HttpContext.RequestAborted);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("{dishId:guid}")]
        public async Task<IActionResult> GetCommets([FromRoute] Guid dishId)
        {
            var authRes = await HttpContext.AuthenticateAsync("Bearer");

            if (authRes.Succeeded)
            {
                HttpContext.User = authRes.Principal;
            }

            if (dishId == default(Guid))
                return BadRequest("Вкажіть ідентифікатор");

            Guid userId = Guid.Empty;

            if (_claimsManager.IsAuthenticated())
                userId = _claimsManager.GetCurrentUserID();

            var comments = await _commentRepository.GetComments(dishId, userId, HttpContext.RequestAborted);

            return Ok(comments);
        }

        [HttpPut("{commentId:guid}/edit")]
        [Authorize(Policy = "UserAuthorized")]
        public async Task<IActionResult> EditComment([FromBody] CommentEditModel model, [FromRoute] Guid commentId)
        {
            var validationRes = _commentEditValidator.Validate(model);

            if (!validationRes.IsValid)
            {
                throw new ControllerInModelException(validationRes);
            }

            if (commentId == default(Guid))
                return BadRequest("Вкажіть ідентифікатор");

            Guid userId = _claimsManager.GetCurrentUserID();

            await _commentRepository.EditComment(model, commentId, userId, HttpContext.RequestAborted);

            return Ok();
        }
    }
}
