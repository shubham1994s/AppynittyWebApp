﻿using System;
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

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<LoginInfo> LoginInfos { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Test> Tests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=202.65.157.254;Initial Catalog=AppynittyCommunication;PersistSecurityInfo=False;User ID=appynitty;Password=BigV$Telecom;MultipleActiveResultSets=False;Connection Timeout=30;");
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

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.PasswordString).HasMaxLength(100);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.Property(e => e.BlogsDate).HasColumnType("datetime");

                entity.Property(e => e.BlogsTitle).HasMaxLength(500);

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Urlink)
                    .HasMaxLength(500)
                    .HasColumnName("URLink");
            });

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

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.NewsDate).HasColumnType("datetime");

                entity.Property(e => e.NewsTitle).HasMaxLength(500);
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Test");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.TestDate).HasColumnType("datetime");

                entity.Property(e => e.TestTitle).HasMaxLength(500);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
