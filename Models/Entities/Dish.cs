﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Models.Entities
{
    public class Dish
    {
        public Guid ID { get; set; }
        public Guid Recipe_ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Recipe Recipe { get; set; }
        public IEnumerable<Menu_Dish> MenusRel { get; set; }
        public IEnumerable<Dish_Tag> TagsRel { get; set; }

        public static void ConfigureEntity(EntityTypeBuilder<Dish> cfg)
        {
            cfg.ToTable("Dishes");

            cfg.HasIndex(x => x.Name)
                .IsUnique(true);

            cfg.HasIndex(x => x.Recipe_ID)
                .IsUnique(true);

            cfg.HasKey(x => x.ID);
            cfg.Property(x => x.ID)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            cfg.Property(x => x.Recipe_ID)
                .HasColumnType("uuid")
                .IsRequired(true);

            cfg.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired(true);

            cfg.Property(x => x.Description)
                .HasColumnType("varchar")
                .HasMaxLength(500)
                .IsRequired(true);

            cfg.HasOne(x => x.Recipe)
                .WithOne(y => y.Dish)
                .HasForeignKey<Dish>(x => x.Recipe_ID); 
        }
    }
}
