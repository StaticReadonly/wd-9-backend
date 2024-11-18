using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Models.Entities
{
    public class Component
    {
        public Guid Recipe_ID { get; set; }
        public Guid Ingredient_ID { get; set; }
        public Ingredient Ingredient { get; set; }
        public Recipe Recipe { get; set; }

        public static void ConfigureEntity(EntityTypeBuilder<Component> cfg)
        {
            cfg.ToTable("Components");

            cfg.HasKey(x => new {x.Recipe_ID, x.Ingredient_ID});

            cfg.Property(x => x.Recipe_ID)
                .HasColumnType("uuid")
                .IsRequired(true);

            cfg.Property(x => x.Ingredient_ID)
                .HasColumnType("uuid")
                .IsRequired(true);

            cfg.HasOne(x => x.Recipe)
                .WithMany(y => y.IngredientRel)
                .HasForeignKey(x => x.Recipe_ID);

            cfg.HasOne(x => x.Ingredient)
                .WithMany(y => y.RecipeRel)
                .HasForeignKey(x => x.Ingredient_ID);
        }
    }
}
