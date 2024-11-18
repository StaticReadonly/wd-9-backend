using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Models.Entities
{
    public class Cooking
    {
        public Guid ID { get; set; }
        public Guid Recipe_ID { get; set; }
        public Guid Step_ID { get; set; }
        public int Step_Number { get; set; }
        public Recipe Recipe { get; set; }
        public Step Step { get; set; }
        public IEnumerable<Cooking_Material> MaterialRel { get; set; }

        public static void ConfigureEntity(EntityTypeBuilder<Cooking> cfg)
        {
            cfg.ToTable("Cooking");

            cfg.HasIndex(x => x.Step_ID)
                .IsUnique(true);

            cfg.HasIndex(x => new {x.Recipe_ID, x.Step_ID, x.Step_Number})
                .IsUnique(true);

            cfg.HasKey(x => x.ID);
            cfg.Property(x => x.ID)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            cfg.Property(x => x.Recipe_ID)
                .HasColumnType("uuid")
                .IsRequired(true);

            cfg.Property(x => x.Step_ID)
                .HasColumnType("uuid")
                .IsRequired(true);

            cfg.Property(x => x.Step_Number)
                .IsRequired(true);

            cfg.HasOne(x => x.Recipe)
                .WithMany(y => y.StepRel)
                .HasForeignKey(x => x.Recipe_ID);

            cfg.HasOne(x => x.Step)
                .WithOne(y => y.Cooking)
                .HasForeignKey<Cooking>(x => x.Step_ID);
        }
    }
}
