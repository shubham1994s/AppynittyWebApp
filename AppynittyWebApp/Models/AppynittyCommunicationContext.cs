using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AppynittyWebApp.Models
{
    public partial class AppynittyCommunicationContext : DbContext
    {
        //public AppynittyCommunicationContext()
        //{
        //}

        public AppynittyCommunicationContext(DbContextOptions<AppynittyCommunicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppliedEmp> AppliedEmps { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<BlogRply> BlogRplies { get; set; }
        public virtual DbSet<Career> Careers { get; set; }
        public virtual DbSet<ContactU> ContactUs { get; set; }
        public virtual DbSet<LoginInfo> LoginInfos { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsRply> NewsRplies { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<NewsReplyDetails> NewsReplyDetails { get; set; }
        public virtual DbSet<AppEmpCVDetails> AppEmpCVDetails { get; set; }
        public virtual DbSet<CarrersDetails> CarrersDetails { get; set; }
        public virtual DbSet<BlogReplyDetails> BlogReplyDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=202.65.157.254;Initial Catalog=AppynittyCommunication;Persist Security Info=False;User ID=appynitty;Password=BigV$Telecom;MultipleActiveResultSets=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<LoginInfo>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
