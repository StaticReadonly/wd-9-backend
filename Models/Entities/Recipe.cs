using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Models.Entities
{
    public class Recipe
    {
        public Guid ID { get; set; }
        public TimeSpan Time { get; set; }
        public Dish Dish { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Cooking> StepRel { get; set; }
        public IEnumerable<Component> IngredientRel { get; set; }

        public static void ConfigureEntity(EntityTypeBuilder<Recipe> cfg)
        {
            cfg.ToTable("Recipes");

            cfg.HasKey(x => x.ID);
            cfg.Property(x => x.ID)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            cfg.Property(x => x.Time)
                .HasColumnType("Time").
                IsRequired(true);
        }
    }
}
