namespace WebApplication1.Models.ControllersIn
{
    public class DishModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }
        public IEnumerable<DishStep> Steps { get; set; }
        public IEnumerable<DishComponent> Components { get; set; }
        public IEnumerable<Guid> Tags { get; set; }
    }
}
