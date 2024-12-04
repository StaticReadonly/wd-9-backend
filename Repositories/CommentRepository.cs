using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DbConn;
using WebApplication1.Exceptions;
using WebApplication1.Models.ControllersIn.Comment;
using WebApplication1.Models.ControllersOut;
using WebApplication1.Models.Entities;
using WebApplication1.Repositories.Abstraction;

namespace WebApplication1.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DbContext1 _context;
        private readonly IMapper _mapper;

        public CommentRepository(
            DbContext1 context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task EditComment(CommentEditModel model, Guid commentId, Guid userId, CancellationToken token)
        {
            Comment? comment = await _context.Comments
                .FirstOrDefaultAsync(x => x.ID == commentId, token);

            if (comment == null)
                throw new EntityNotFoundException("Коментаря не існує");

            if (comment.User_ID != userId)
                throw new AccessDeniedException();

            comment.Text = model.NewText;

            await _context.SaveChangesAsync(token);
        }

        public async Task<IEnumerable<DishComment>> GetComments(Guid dishId, Guid userId, CancellationToken token)
        {
            var dish = await _context.Dishes
                .Include(x => x.Recipe)
                .ThenInclude(y => y.Comments)
                .ThenInclude(z => z.User)
                .FirstOrDefaultAsync(x => x.ID == dishId, token);

            if (dish == null)
                throw new EntityNotFoundException("Страви не існує");

            IEnumerable<DishComment> comments = dish.Recipe.Comments.Select(x =>
            {
                DishComment c = _mapper.Map<DishComment>(x);
                c.CanEdit = (userId != Guid.Empty) && (userId == x.User_ID);
                return c;
            }).OrderByDescending(x => x.TimeStamp);

            return comments;
        }

        public async Task PostComment(CommentPostModel commentPost, Guid dishId, Guid userId, CancellationToken token)
        {
            var dish = await _context
                .Dishes
                .Include(x => x.Recipe)
                .FirstOrDefaultAsync(x => x.ID == dishId, token);

            if (dish == null)
                throw new EntityNotFoundException("Страви не існує");

            await _context.Comments.AddAsync(new()
            {
                Recipe = dish.Recipe,
                TimeStamp = DateTime.UtcNow,
                Text = commentPost.Text,
                User_ID = userId
            }, token);

            await _context.SaveChangesAsync(token);
        }
    }
}
