using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class DBFirstDbContext : DbContext
    {
        public DBFirstDbContext()
        {
        }

        public DBFirstDbContext(DbContextOptions<DBFirstDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Auth> Auths { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("name=MySQLConnection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Auth>(entity =>
            {
                entity.ToTable("auth");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccessToken)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("access_token");

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("client_id");

                entity.Property(e => e.ClientSecret)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("client_secret");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("date_created");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("last_updated");

                entity.Property(e => e.TokenType)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("token_type");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
