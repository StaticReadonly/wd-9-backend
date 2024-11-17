using WebApplication1.Options;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            config.AddJsonFile("Configs/ClaimsOptions.json");

            var services = builder.Services;

            services.AddControllers();

            services.AddAuthentication()
                .AddCookie(cfg =>
                {
                    var cookie = cfg.Cookie;
                    cookie.HttpOnly = true;
                    cookie.Name = "auth";
                    cookie.SameSite = SameSiteMode.Strict;
                    cookie.MaxAge = TimeSpan.FromDays(14);
                });

            var claimsOptions = config.GetSection("ClaimsOptions");
            services.Configure<ClaimsOptions>(claimsOptions);
            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("AdminOnly", cfg =>
                {
                    cfg.RequireAuthenticatedUser()
                    .RequireClaim(claimsOptions["Role"], claimsOptions["RolesAllowedAdmin"])
                    .AddAuthenticationSchemes("Cookie");
                });
                cfg.AddPolicy("UserAuthorized", cfg =>
                {
                    cfg.RequireAuthenticatedUser()
                    .RequireClaim(claimsOptions["Role"], claimsOptions["RolesAllowedUser"])
                    .AddAuthenticationSchemes("Cookie");
                });
            });

            services.AddCors(cfg =>
            {
                cfg.AddPolicy("DevFrontend", cfg =>
                {
                    cfg.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:3000");
                });
            });

            var app = builder.Build();

            app.UseHttpsRedirection();

            if (app.Environment.IsDevelopment())
            {
                app.UseCors("DevFrontend");
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
