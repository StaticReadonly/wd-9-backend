using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DbConn;
using WebApplication1.Exceptions;
using WebApplication1.Models.ControllersOut;
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

        public async Task<IEnumerable<DishComment>> GetComments(Guid dishId, CancellationToken token)
        {
            var dish = await _context.Dishes
                .Include(x => x.Recipe)
                .ThenInclude(y => y.Comments)
                .ThenInclude(z => z.User)
                .FirstOrDefaultAsync(x => x.ID == dishId, token);

            if (dish == null)
                throw new EntityNotFoundException("Страви не існує");

            IEnumerable<DishComment> comments = _mapper.Map<IEnumerable<DishComment>>(
                dish.Recipe.Comments.AsEnumerable());

            return comments;
        }
    }
}
