using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace BusinessObjects
{
    public partial class PRN211_OnlyFunds_CopyContext : DbContext
    {
        public PRN211_OnlyFunds_CopyContext()
        {
        }

        public PRN211_OnlyFunds_CopyContext(DbContextOptions<PRN211_OnlyFunds_CopyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Bookmark> Bookmarks { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CommentLike> CommentLikes { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostCategoryMap> PostCategoryMaps { get; set; }
        public virtual DbSet<PostLike> PostLikes { get; set; }
        public virtual DbSet<PostReport> PostReports { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserCategoryMap> UserCategoryMaps { get; set; }

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
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Admin__536C85E535961341");

                entity.ToTable("Admin");

                entity.HasIndex(e => e.Email, "UQ__Admin__A9D10534A200B853")
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
                    .HasName("PK__Bookmark__EF24A8464A39E61F");

                entity.ToTable("Bookmark");

                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Bookmarks)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Bookmark__PostId__2739D489");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Bookmarks)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK__Bookmark__Userna__282DF8C2");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasIndex(e => e.CategoryName, "UQ__Category__8517B2E01ED4AB3C")
                    .IsUnique();

                entity.Property(e => e.CategoryId).ValueGeneratedNever();

                entity.Property(e => e.CategoryName).HasMaxLength(100);
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
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Comment__PostId__236943A5");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Comment__Usernam__245D67DE");
            });

            modelBuilder.Entity<CommentLike>(entity =>
            {
                entity.HasKey(e => new { e.CommentId, e.Username })
                    .HasName("PK__CommentL__86821794294302F6");

                entity.ToTable("CommentLike");

                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentLikes)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CommentLi__Comme__3C34F16F");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.CommentLikes)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CommentLi__Usern__3B40CD36");
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
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Post__UploaderUs__2645B050");
            });

            modelBuilder.Entity<PostCategoryMap>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.CategoryId })
                    .HasName("PK__PostCate__0B82F3B89E8A2B51");

                entity.ToTable("PostCategoryMap");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.PostCategoryMaps)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__PostCateg__Categ__22751F6C");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostCategoryMaps)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__PostCateg__PostI__2180FB33");
            });

            modelBuilder.Entity<PostLike>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.Username })
                    .HasName("PK__PostLike__EF24A8469F10202F");

                entity.ToTable("PostLike");

                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostLikes)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__PostLike__PostId__2CF2ADDF");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.PostLikes)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK__PostLike__Userna__2BFE89A6");
            });

            modelBuilder.Entity<PostReport>(entity =>
            {
                entity.HasKey(e => e.ReportId)
                    .HasName("PK__PostRepo__D5BD4805C1D489C4");

                entity.ToTable("PostReport");

                entity.Property(e => e.ReportId).ValueGeneratedNever();

                entity.Property(e => e.ReportDate).HasColumnType("date");

                entity.Property(e => e.ReportDescription).HasMaxLength(1000);

                entity.Property(e => e.ReporterUsername)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(e => e.IsSolved)
                    .HasDefaultValue(false)
                    .IsRequired();

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostReports)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__PostRepor__PostI__1EA48E88");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__User__536C85E538E27FA6");

                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "UQ__User__A9D10534F82B0870")
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

            modelBuilder.Entity<UserCategoryMap>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.CategoryId })
                    .HasName("PK__UserCate__F2FC164505101EB2");

                entity.ToTable("UserCategoryMap");

                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.UserCategoryMaps)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__UserCateg__Categ__1DB06A4F");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.UserCategoryMaps)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK__UserCateg__Usern__1CBC4616");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
