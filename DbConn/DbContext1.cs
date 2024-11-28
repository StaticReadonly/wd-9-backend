using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Entities;

namespace WebApplication1.DbConn
{
    public class DbContext1 : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Cooking> Cookings { get; set; }
        public DbSet<Cooking_Material> Cooking_Materials { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Dish_Tag> Dish_Tags { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Menu_Dish> Menu_Dishes { get; set; }
        public DbSet<Recipe> Recipies { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbContext1(DbContextOptions<DbContext1> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(Comment.ConfigureEntity);
            modelBuilder.Entity<Component>(Component.ConfigureEntity);
            modelBuilder.Entity<Cooking>(Cooking.ConfigureEntity);
            modelBuilder.Entity<Cooking_Material>(Cooking_Material.ConfigureEntity);
            modelBuilder.Entity<Dish>(Dish.ConfigureEntity);
            modelBuilder.Entity<Dish_Tag>(Dish_Tag.ConfigureEntity);
            modelBuilder.Entity<Ingredient>(Ingredient.ConfigureEntity);
            modelBuilder.Entity<Material>(Material.ConfigureEntity);
            modelBuilder.Entity<Menu>(Menu.ConfigureEntity);
            modelBuilder.Entity<Menu_Dish>(Menu_Dish.ConfigureEntity);
            modelBuilder.Entity<Recipe>(Recipe.ConfigureEntity);
            modelBuilder.Entity<Step>(Step.ConfigureEntity);
            modelBuilder.Entity<Tag>(Tag.ConfigureEntity);
            modelBuilder.Entity<User>(User.ConfigureEntity);
        }
    }
}
