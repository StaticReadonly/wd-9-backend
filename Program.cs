using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Cryptography;
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
                    cookie.SameSite = SameSiteMode.Strict;
                    cookie.MaxAge = TimeSpan.FromDays(14);

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
                    .AddAuthenticationSchemes("Cookie");
                });
                cfg.AddPolicy("UserAuthorized", cfg =>
                {
                    cfg.RequireAuthenticatedUser()
                    .RequireClaim(
                        claimsOptions.GetValue<string>("Role"),
                        claimsOptions.GetSection("RolesAllowedUser").Get<string[]>()
                    )
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
                        .WithOrigins("http://localhost:8080");
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
