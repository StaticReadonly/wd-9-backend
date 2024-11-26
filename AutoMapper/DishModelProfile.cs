using AutoMapper;
using WebApplication1.Models.ControllersIn;
using WebApplication1.Models.Entities;

namespace WebApplication1.AutoMapper
{
    public class DishModelProfile : Profile
    {
        public DishModelProfile()
        {
            CreateMap<DishModel, Dish>();
        }
    }
}
