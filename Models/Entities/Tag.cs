using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Models.Entities
{
    public class Tag
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Dish_Tag> DishesRel { get; set; }

        public static void ConfigureEntity(EntityTypeBuilder<Tag> cfg)
        {
            cfg.ToTable("Tags");

            cfg.HasIndex(x => x.Name)
                .IsUnique(true);

            cfg.HasKey(x => x.ID);
            cfg.Property(x => x.ID)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            cfg.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .IsRequired(true);

            cfg.Property(x => x.Description)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired(true);
        }
    }
}
