using AutoMapper;
using WebApplication1.Models.ControllersIn.Dish;
using WebApplication1.Models.ControllersOut;
using WebApplication1.Models.Entities;

namespace WebApplication1.AutoMapper
{
    public class DishModelProfile : Profile
    {
        public DishModelProfile()
        {
            CreateMap<DishModel, Dish>();

            CreateMap<Dish, DishShortInfo>();
        }
    }
}
