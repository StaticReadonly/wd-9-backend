using AutoMapper;
using WebApplication1.DbConn;
using WebApplication1.Models.ControllersIn.Tag;
using WebApplication1.Models.ControllersOut;
using WebApplication1.Models.Entities;
using WebApplication1.Repositories.Abstraction;

namespace WebApplication1.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly IMapper _mapper;
        private readonly DbContext1 _context;

        public TagRepository(
            IMapper mapper, 
            DbContext1 context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<TagInfo> CreateTag(TagCreateModel model, CancellationToken token)
        {
            Tag newTag = _mapper.Map<Tag>(model);

            await _context.Tags.AddAsync(newTag, token);
            await _context.SaveChangesAsync(token);

            return _mapper.Map<TagInfo>(newTag);
        }

        public Task<IEnumerable<TagInfo>> SearchTags(TagSearchModel model, CancellationToken token)
        {
            var res = _context.Tags.Where(x => x.Name.ToLower().Contains(model.Query.ToLower()))
                    .AsEnumerable();

            token.ThrowIfCancellationRequested();

            return Task.FromResult(_mapper.Map<IEnumerable<TagInfo>>(res));
        }
    }
}
