using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models.Entities;
using WebApplication1.Options;

namespace WebApplication1.Services.ClaimsManager
{
    public class ClaimsManager
    {
        private readonly ClaimsOptions _claimsOptions;
        private readonly IConfiguration _configuration;
        private readonly HttpContext _context;

        public ClaimsManager(
            IOptions<ClaimsOptions> claimsOptions, 
            IConfiguration configuration,
            IHttpContextAccessor contextAccessor)
        {
            _claimsOptions = claimsOptions.Value;
            _configuration = configuration;
            _context = contextAccessor.HttpContext;
        }

        public ClaimsPrincipal BuildPrincipal(User userData)
        {
            var claims = GetClaims(userData);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookie");
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
            return principal;
        }

        public string BuildJwtToken(User userDate)
        {
            var claims = GetClaims(userDate);
            var jwtConfig = _configuration.GetSection("Jwt");

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtConfig.GetValue<string>("SecretKey")));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtConfig.GetValue<string>("Issuer"),
                audience: jwtConfig.GetValue<string>("Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(14),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public List<Claim> GetClaims(User userData)
        {
            var claims = new List<Claim>()
            {
                new Claim(_claimsOptions.ID, userData.ID.ToString()),
                new Claim(_claimsOptions.Role, userData.Role)
            };

            return claims;
        }

        public Guid GetCurrentUserID()
        {
            Claim claim = _context.User.Claims.First(x => x.Type == _claimsOptions.ID);
            return Guid.Parse(claim.Value);
        }
    }
}
