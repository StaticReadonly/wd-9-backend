namespace WebApplication1.Models.ControllersIn
{
    public class MenuCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Guid> Dishes { get; set; }
    }
}
