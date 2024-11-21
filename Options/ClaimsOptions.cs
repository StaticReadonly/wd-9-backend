namespace WebApplication1.Options
{
    public class ClaimsOptions
    {
        public string ID { get; set; }
        public string Role { get; set; }
        public string[] RolesAllowedAdmin { get; set; }
        public string[] RolesAllowedUser { get; set; }
    }
}
