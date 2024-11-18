using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Models.Entities
{
    public class Comment
    {
        public Guid ID { get; set; }
        public Guid Recipe_ID { get; set; }
        public Guid User_ID { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public User User { get; set; }
        public Recipe Recipe { get; set; }

        public static void ConfigureEntity(EntityTypeBuilder<Comment> cfg)
        {
            cfg.ToTable("Comments");

            cfg.HasKey(x => x.ID);
            cfg.Property(x => x.ID)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            cfg.Property(x => x.Recipe_ID)
                .HasColumnType("uuid")
                .IsRequired(true);

            cfg.Property(x => x.User_ID)
               .HasColumnType("uuid")
               .IsRequired(true);

            cfg.Property(x => x.Text)
                .HasColumnType("varchar")
                .HasMaxLength(500)
                .IsRequired(true);

            cfg.Property(x => x.TimeStamp)
                .HasColumnType("timestamp")
                .IsRequired(true);

            cfg.HasOne(x => x.User)
                .WithMany(y => y.Comments)
                .HasForeignKey(x => x.User_ID);

            cfg.HasOne(x => x.Recipe)
                .WithMany(y => y.Comments)
                .HasForeignKey(x => x.Recipe_ID);
        }
    }
}
