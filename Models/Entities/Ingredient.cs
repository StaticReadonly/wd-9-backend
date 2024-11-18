using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Models.Entities
{
    public class Ingredient
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public IEnumerable<Component> RecipeRel { get; set; }

        public static void ConfigureEntity(EntityTypeBuilder<Ingredient> cfg)
        {
            cfg.ToTable("Ingredients");

            cfg.HasKey(x => x.ID);
            cfg.Property(x => x.ID)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            cfg.HasIndex(x => x.Name)
                .IsUnique(true);

            cfg.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired(true);

            cfg.Property(x => x.Unit)
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsRequired(true);
        }
    }
}
