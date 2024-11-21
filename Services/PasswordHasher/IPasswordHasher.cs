namespace WebApplication1.Services.PasswordHasher
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Compare(string password, string with);
    }
}
