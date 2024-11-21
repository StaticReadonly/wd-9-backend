using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebApplication1.Models.Entities;
using WebApplication1.Options;

namespace WebApplication1.Services.ClaimsManager
{
    public class ClaimsManager
    {
        private readonly ClaimsOptions _claimsOptions;

        public ClaimsManager(
            IOptions<ClaimsOptions> claimsOptions)
        {
            _claimsOptions = claimsOptions.Value;
        }

        public ClaimsPrincipal BuildPrincipal(User userData)
        {
            var claims = new List<Claim>() 
            {
                new Claim(_claimsOptions.ID, userData.ID.ToString()),
                new Claim(_claimsOptions.Role, userData.Role)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookie");
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
            return principal;
        }
    }
}
