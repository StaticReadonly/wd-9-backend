using AutoMapper;
using WebApplication1.Models.ControllersIn.Tag;
using WebApplication1.Models.ControllersOut;
using WebApplication1.Models.Entities;

namespace WebApplication1.AutoMapper
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<TagCreateModel, Tag>().ReverseMap();
            CreateMap<Tag, TagInfo>();
        }
    }
}
