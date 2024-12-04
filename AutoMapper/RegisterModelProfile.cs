using AutoMapper;
using WebApplication1.Models.ControllersIn.User;
using WebApplication1.Models.Entities;

namespace WebApplication1.AutoMapper
{
    public class RegisterModelProfile : Profile
    {
        public RegisterModelProfile()
        {
            CreateMap<RegisterModel, User>().ReverseMap();
        }
    }
}
