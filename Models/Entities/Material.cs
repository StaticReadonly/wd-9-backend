using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Models.Entities
{
    public class Material
    {
        public Guid ID { get; set; }
        public string URL { get; set; }
        public IEnumerable<Cooking_Material> CookingRel { get; set; }

        public static void ConfigureEntity(EntityTypeBuilder<Material> cfg)
        {
            cfg.ToTable("Materials");

            cfg.HasKey(x => x.ID);
            cfg.Property(x => x.ID)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            cfg.Property(x => x.URL)
                .HasColumnType("varchar");
        }
    }
}
