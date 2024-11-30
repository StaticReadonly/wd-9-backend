using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.DbConn;
using WebApplication1.Options;
using WebApplication1.Repositories;
using WebApplication1.Repositories.Abstraction;
using WebApplication1.Services.ClaimsManager;
using WebApplication1.Services.ExceptionHandler;
using WebApplication1.Services.PasswordHasher;

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

            services.AddDbContext<DbContext1>(cfg =>
            {
                cfg.UseNpgsql(config.GetConnectionString("Postgres"));
            });

            services.AddScoped<ClaimsManager>();

            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IIngredientsRepository, IngredientsRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            services.AddControllers(cfg =>
            {
                cfg.Filters.Add<ExceptionFilter>();
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddHttpContextAccessor();

            services.AddSingleton<IPasswordHasher, PasswordHasher>(fac =>
            {
                return new PasswordHasher(SHA256.Create());
            });

            services.AddAuthentication()
                .AddCookie("Cookie", cfg =>
                {
                    var cookie = cfg.Cookie;
                    cookie.HttpOnly = true;
                    cookie.Name = "auth";
                    cookie.SameSite = SameSiteMode.None;
                    cookie.MaxAge = TimeSpan.FromDays(14);
                    cookie.SecurePolicy = CookieSecurePolicy.Always;
                    cookie.Path = "/api";

                    cfg.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    };
                    cfg.Events.OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    };
                })
                .AddJwtBearer("Bearer", cfg =>
                {
                    var bearerOptions = config.GetSection("Jwt");

                    cfg.TokenValidationParameters = new()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = bearerOptions.GetValue<string>("Issuer"),
                        ValidAudience = bearerOptions.GetValue<string>("Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            bearerOptions.GetValue<string>("SecretKey")))
                    };
                });

            var claimsOptions = config.GetSection("ClaimsOptions");
            services.Configure<ClaimsOptions>(claimsOptions);

            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("AdminOnly", cfg =>
                {
                    cfg.RequireAuthenticatedUser()
                    .RequireClaim(
                        claimsOptions.GetValue<string>("Role"),
                        claimsOptions.GetSection("RolesAllowedAdmin").Get<string[]>()
                    )
                    .AddAuthenticationSchemes("Bearer");
                });
                cfg.AddPolicy("UserAuthorized", cfg =>
                {
                    cfg.RequireAuthenticatedUser()
                    .RequireClaim(
                        claimsOptions.GetValue<string>("Role"),
                        claimsOptions.GetSection("RolesAllowedUser").Get<string[]>()
                    )
                    .AddAuthenticationSchemes("Bearer");
                });
            });

            services.AddCors(cfg =>
            {
                cfg.AddPolicy("DevFrontend", cfg =>
                {
                    cfg.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:5173");
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
