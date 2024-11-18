using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Models.Entities
{
    public class Menu_Dish
    {
        public Guid Dish_ID { get; set; }
        public Dish Dish { get; set; }
        public Guid Menu_ID { get; set; }
        public Menu Menu { get; set; }

        public static void ConfigureEntity(EntityTypeBuilder<Menu_Dish> cfg)
        {
            cfg.ToTable("Menus_Dishes");

            cfg.HasKey(x => new { x.Dish_ID, x.Menu_ID });

            cfg.Property(x => x.Dish_ID)
                .HasColumnType("uuid")
                .IsRequired(true);

            cfg.Property(x => x.Menu_ID)
                .HasColumnType("uuid")
                .IsRequired(true);

            cfg.HasOne(x => x.Dish)
                .WithMany(y => y.MenusRel)
                .HasForeignKey(x => x.Dish_ID);

            cfg.HasOne(x => x.Menu)
                .WithMany(y => y.DishesRel)
                .HasForeignKey(x => x.Menu_ID);
        }
    }
}
