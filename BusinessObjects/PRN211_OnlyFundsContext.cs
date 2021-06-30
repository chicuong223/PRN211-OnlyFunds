using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace BusinessObjects
{
    public partial class PRN211_OnlyFundsContext : DbContext
    {
        public PRN211_OnlyFundsContext()
        {
        }

        public PRN211_OnlyFundsContext(DbContextOptions<PRN211_OnlyFundsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Bookmark> Bookmarks { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryMap> CategoryMaps { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostReport> PostReports { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true);
                IConfigurationRoot config = builder.Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("OnlyFundsDB"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Vietnamese_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Admin__536C85E5A8A7ADBE");

                entity.ToTable("Admin");

                entity.HasIndex(e => e.Email, "UQ__Admin__A9D10534EB2850EA")
                    .IsUnique();

                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bookmark>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.Username })
                    .HasName("PK__Bookmark__EF24A846749ED7D7");

                entity.ToTable("Bookmark");

                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Bookmarks)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bookmark__PostId__3A81B327");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Bookmarks)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bookmark__Userna__3B75D760");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasIndex(e => e.CategoryName, "UQ__Category__8517B2E05885F515")
                    .IsUnique();

                entity.Property(e => e.CategoryId).ValueGeneratedNever();

                entity.Property(e => e.CategoryName).HasMaxLength(100);
            });

            modelBuilder.Entity<CategoryMap>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.CategoryId })
                    .HasName("PK__Category__0B82F3B8914426F2");

                entity.ToTable("CategoryMap");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryMaps)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CategoryM__Categ__31EC6D26");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.CategoryMaps)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CategoryM__PostI__30F848ED");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentId).ValueGeneratedNever();

                entity.Property(e => e.CommentDate).HasColumnType("date");

                entity.Property(e => e.Content).HasMaxLength(1000);

                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Comment__PostId__2A4B4B5E");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK__Comment__Usernam__2B3F6F97");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId).ValueGeneratedNever();

                entity.Property(e => e.FileUrl)
                    .HasMaxLength(256)
                    .HasColumnName("FileURL");

                entity.Property(e => e.PostDescription).HasMaxLength(1000);

                entity.Property(e => e.PostTitle).HasMaxLength(100);

                entity.Property(e => e.UploadDate).HasColumnType("date");

                entity.Property(e => e.UploaderUsername)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.UploaderUsernameNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UploaderUsername)
                    .HasConstraintName("FK__Post__UploaderUs__276EDEB3");
            });

            modelBuilder.Entity<PostReport>(entity =>
            {
                entity.HasKey(e => e.ReportId)
                    .HasName("PK__PostRepo__D5BD4805E9AE9255");

                entity.ToTable("PostReport");

                entity.Property(e => e.ReportId).ValueGeneratedNever();

                entity.Property(e => e.ReportDate).HasColumnType("date");

                entity.Property(e => e.ReportDescription).HasMaxLength(1000);

                entity.Property(e => e.ReporterUsername)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.ReporterUsernameNavigation)
                    .WithMany(p => p.PostReports)
                    .HasForeignKey(d => d.ReporterUsername)
                    .HasConstraintName("FK__PostRepor__Repor__37A5467C");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__User__536C85E56F7024A8");

                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "UQ__User__A9D10534EC238622")
                    .IsUnique();

                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.AvatarUrl)
                    .HasMaxLength(256)
                    .HasColumnName("AvatarURL");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
