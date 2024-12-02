namespace WebApplication1.Models.ControllersOut
{
    public class MenuInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<DishShortInfo> DishesInfo { get; set; }
    }
}
