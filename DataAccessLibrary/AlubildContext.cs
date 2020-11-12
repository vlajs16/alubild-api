using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccessLibrary
{
    public class AlubildContext : IdentityDbContext<User, Role, long,
        IdentityUserClaim<long>, UserRole, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public DbSet<UserLog> UserLogs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPhoto> OrderPhotos { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Typology> Typologies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TypologyCategory> TypologyCategories { get; set; }
        public DbSet<Quality> Qualities { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<GlassQuality> GlassQualities { get; set; }
        public DbSet<GlassPackage> GlassPackages { get; set; }
        public DbSet<GlassPackageTypology> GlassPackageTypologies { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Tabakera> Tabakeras { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Series> Series { get; set; }

        public AlubildContext(DbContextOptions<AlubildContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(b =>
            {
                b.HasKey(ur => new { ur.UserId, ur.RoleId });

                b.HasOne(ur => ur.Role)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                b.HasOne(ur => ur.User)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });

            builder.Entity<UserLog>(x =>
            {
                x.HasKey(p => new { p.LogDateTime, p.UserId });
            });

            builder.Entity<Order>(x =>
            {
                x.HasKey(p => new { p.Id, p.UserId });
                x.Property(p => p.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<OrderPhoto>(x =>
            {
                x.HasKey(p => new { p.Id, p.OrderId, p.OrderUserId });
                x.Property(p => p.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<OrderItem>(x =>
            {
                x.HasKey(p => new { p.Id, p.OrderId, p.OrderUserId });
                x.Property(p => p.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<TypologyCategory>(x =>
            {
                x.HasKey(p => new { p.TypologyId, p.CategoryId });

                x.HasOne(p => p.Category)
                .WithMany(p => p.TypologyCategories)
                .HasForeignKey(p => p.CategoryId)
                .IsRequired();

                x.HasOne(p => p.Typology)
                .WithMany(p => p.TypologyCategories)
                .HasForeignKey(p => p.TypologyId)
                .IsRequired();
            });

            builder.Entity<GlassPackageTypology>(x =>
            {
                x.HasKey(p => new { p.TypologyId, p.GlassPackageId });

                x.HasOne(p => p.GlassPackage)
                .WithMany(p => p.GlassPackageTypologies)
                .HasForeignKey(p => p.GlassPackageId)
                .IsRequired();

                x.HasOne(p => p.Typology)
                .WithMany(p => p.GlassPackageTypologies)
                .HasForeignKey(p => p.TypologyId)
                .IsRequired();
            });

            builder.Entity<Series>(x =>
            {
                x.HasKey(p => new { p.Id, p.ManufacturerId });
                x.Property(p => p.Id).ValueGeneratedOnAdd();
            });

        }
    }
}
