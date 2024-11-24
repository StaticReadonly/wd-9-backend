using AutoMapper;
using WebApplication1.Models.ControllersIn;
using WebApplication1.Models.ControllersOut;
using WebApplication1.Models.Entities;

namespace WebApplication1.AutoMapper
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<IngredientModel, Ingredient>().ReverseMap();
            CreateMap<Ingredient, IngredientInfo>();
        }
    }
}
