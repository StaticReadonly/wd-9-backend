using AutoMapper;
using WebApplication1.Models.ControllersOut;
using WebApplication1.Models.Entities;

namespace WebApplication1.AutoMapper
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, DishComment>()
                .ForMember(x => x.OwnerInfo, cfg => cfg.MapFrom(y => y.User));
        }
    }
}
