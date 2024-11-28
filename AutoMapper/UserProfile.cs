using AutoMapper;
using WebApplication1.Models.ControllersOut;
using WebApplication1.Models.Entities;

namespace WebApplication1.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserInfo>().ReverseMap();

            CreateMap<User, CommentOwnerInfo>();
        }
    }
}
