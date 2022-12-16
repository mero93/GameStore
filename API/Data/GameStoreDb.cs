using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
   public class GameStoreDb : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole,
    IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
   {
        public GameStoreDb(DbContextOptions<GameStoreDb> options) : base(options)
        {
        }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<GameCategory> GameCategories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Discussion> Discussions { get; set; }

        public DbSet<Review> Reviews { get; set; }

        //Mapping
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RefreshToken>().HasKey(x => new { x.Id, x.UserId });

            builder.Entity<AppUser>().HasMany(au => au.UserRoles)
            .WithOne(r => r.User).HasForeignKey(x => x.UserId)
            .IsRequired();

            builder.Entity<AppRole>().HasMany(r => r.UserRoles)
            .WithOne(au => au.Role).HasForeignKey(x => x.RoleId)
            .IsRequired();

            builder.Entity<AppUser>().HasAlternateKey(au => au.UserName);

            builder.Entity<AppUser>().HasOne(au => au.RefreshToken)
                .WithOne(rt => rt.AppUser).HasForeignKey<RefreshToken>(au => au.UserId);

            builder.Entity<AppUser>().HasMany(au => au.Orders)
            .WithOne(o => o.AppUser).HasForeignKey(o => o.AppUserId);

            builder.Entity<Review>().HasKey(r => new { r.AppUserId, r.GameId });

            builder.Entity<Review>().HasOne(r => r.AppUser)
            .WithMany(au => au.Reviews).HasForeignKey(r => r.AppUserId);

            builder.Entity<Review>().HasOne(r => r.Game)
            .WithMany(g => g.Reviews).HasForeignKey(r => r.GameId);

            builder.Entity<OrderItem>().HasKey(x => new { x.OrderId, x.GameId });

            builder.Entity<Order>().HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order).HasForeignKey(oi => oi.OrderId);

            builder.Entity<Publisher>().HasMany(p => p.Games)
                .WithOne(g => g.Publisher).HasForeignKey(g => g.PublisherId);

            builder.Entity<GameCategory>().HasKey(gc => new { gc.GameId, gc.CategoryId });

            builder.Entity<GameCategory>().HasOne(gc => gc.Game)
                .WithMany(g => g.Categories).HasForeignKey(g => g.GameId);

            builder.Entity<GameCategory>().HasOne(gc => gc.Category)
                .WithMany(c => c.Games).HasForeignKey(g => g.CategoryId);

            builder.Entity<Game>().HasMany(g => g.Discussions)
                .WithOne(d => d.Game).HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>().HasMany(g => g.Discussions)
                .WithOne(d => d.AppUser).HasForeignKey(d => d.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Discussion>().HasMany(g => g.Comments)
                .WithOne(d => d.Discussion).HasForeignKey(d => d.DiscussionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<AppUser>().HasMany(g => g.Comments)
                .WithOne(d => d.AppUser).HasForeignKey(d => d.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Game>().Property(o => o.Rating).HasColumnType("decimal(3,2)");
            builder.Entity<Order>().Property(o => o.Subtotal).HasColumnType("decimal(18,2)");
            builder.Entity<OrderItem>().Property(o => o.Price).HasColumnType("decimal(18,2)");
            builder.Entity<Game>().Property(o => o.Price).HasColumnType("decimal(18,2)");
            builder.Entity<Review>().Property(o => o.Rating).HasColumnType("decimal(3,2)");
        }
   }
}