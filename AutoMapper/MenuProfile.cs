using AutoMapper;
using WebApplication1.Models.ControllersIn.Menu;
using WebApplication1.Models.ControllersOut;
using WebApplication1.Models.Entities;

namespace WebApplication1.AutoMapper
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<MenuCreateModel, Menu>();

            CreateMap<Menu, MenuSearchInfo>();
        }
    }
}
