using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Models.Entities
{
    public class User
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string First_name { get; set; }
        public string Last_Name { get; set; }
        public IEnumerable<Comment> Comments { get; set; }


        public static void ConfigureEntity(EntityTypeBuilder<User> cfg)
        {
            cfg.ToTable("Users");

            cfg.HasKey(x => x.ID);
            cfg.Property(x => x.ID)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            cfg.HasIndex(x => x.Email)
                .IsUnique(true);

            cfg.Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired(true);

            cfg.Property(x => x.Password)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired(true);

            cfg.Property(x => x.Role)
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsRequired(true);

            cfg.Property(x => x.First_name)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired(true);

            cfg.Property(x => x.Last_Name)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired(true);
        }
    }
}
