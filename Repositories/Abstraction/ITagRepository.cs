using WebApplication1.Models.ControllersIn;
using WebApplication1.Models.ControllersOut;

namespace WebApplication1.Repositories.Abstraction
{
    public interface ITagRepository
    {
        Task<IEnumerable<TagInfo>> SearchTags(TagSearchModel model, CancellationToken token);
        Task<TagInfo> CreateTag(TagCreateModel model, CancellationToken token);
    }
}
