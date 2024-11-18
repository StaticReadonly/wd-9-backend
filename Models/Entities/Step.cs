﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Models.Entities
{
    public class Step
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Cooking Cooking { get; set; }

        public static void ConfigureEntity(EntityTypeBuilder<Step> cfg)
        {
            cfg.ToTable("Steps");

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
                .HasMaxLength(500)
                .IsRequired(true);
        }
    }
}
