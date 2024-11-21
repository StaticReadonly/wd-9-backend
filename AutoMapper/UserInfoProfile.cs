using AutoMapper;
using WebApplication1.Models.ControllersOut;
using WebApplication1.Models.Entities;

namespace WebApplication1.AutoMapper
{
    public class UserInfoProfile : Profile
    {
        public UserInfoProfile() 
        {
            CreateMap<User, UserInfo>().ReverseMap();
        }
    }
}
