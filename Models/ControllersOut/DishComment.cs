namespace WebApplication1.Models.ControllersOut
{
    public class DishComment
    {
        public CommentOwnerInfo OwnerInfo { get; set; }
        public Guid ID { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool CanEdit { get; set; }
    }
}
