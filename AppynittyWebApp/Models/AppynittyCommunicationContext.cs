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

        public virtual DbSet<LoginInfo> LoginInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=202.65.157.254;Initial Catalog=AppynittyCommunication;Persist Security Info=False;User ID=appynitty;Password=BigV$Telecom;MultipleActiveResultSets=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<LoginInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LoginInfo");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("userId");

                entity.Property(e => e.UserLoginId)
                    .HasMaxLength(200)
                    .HasColumnName("userLoginId");

                entity.Property(e => e.UserName)
                    .HasMaxLength(500)
                    .HasColumnName("userName");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(100)
                    .HasColumnName("userPassword");

                entity.Property(e => e.UserType)
                    .HasMaxLength(50)
                    .HasColumnName("userType");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
