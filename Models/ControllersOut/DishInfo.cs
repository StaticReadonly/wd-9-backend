namespace WebApplication1.Models.ControllersOut
{
    public class DishInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }
        public IEnumerable<DishStepInfo> Steps { get; set; }
        public IEnumerable<DishIngredientInfo> Ingredients { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
