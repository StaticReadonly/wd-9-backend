using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Models.Entities
{
    public class Cooking_Material
    {
        public Guid Cooking_ID { get; set; }
        public Guid Material_ID { get; set; }
        public Cooking Cooking { get; set; }
        public Material Material { get; set; }

        public static void ConfigureEntity(EntityTypeBuilder<Cooking_Material> cfg)
        {
            cfg.ToTable("Cookings_Materials");

            cfg.HasKey(x => new {x.Cooking_ID, x.Material_ID});

            cfg.Property(x => x.Cooking_ID)
                .HasColumnType("uuid")
                .IsRequired(true);

            cfg.Property(x => x.Material_ID)
                .HasColumnType("uuid")
                .IsRequired(true);

            cfg.HasOne(x => x.Cooking)
                .WithMany(y => y.MaterialRel)
                .HasForeignKey(x => x.Cooking_ID);

            cfg.HasOne(x => x.Material)
                .WithMany(y => y.CookingRel)
                .HasForeignKey(x => x.Material_ID);
        }
    }
}
