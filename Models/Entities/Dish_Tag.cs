using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Models.Entities
{
    public class Dish_Tag
    {
        public Guid Dish_ID { get; set; }
        public Dish Dish { get; set; }
        public Guid Tag_ID { get; set; }
        public Tag Tag { get; set; }

        public static void ConfigureEntity(EntityTypeBuilder<Dish_Tag> cfg)
        {
            cfg.ToTable("Dishes_Tags");

            cfg.HasKey(x => new {x.Dish_ID, x.Tag_ID});

            cfg.Property(x => x.Dish_ID)
                .HasColumnType("uuid")
                .IsRequired(true);

            cfg.Property(x => x.Tag_ID)
                .HasColumnType("uuid")
                .IsRequired(true);

            cfg.HasOne(x => x.Dish)
                .WithMany(y => y.TagsRel)
                .HasForeignKey(x => x.Dish_ID);

            cfg.HasOne(x => x.Tag)
                .WithMany(y => y.DishesRel)
                .HasForeignKey(x => x.Tag_ID);
        }
    }
}
