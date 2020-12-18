using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreOrdering.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreOrdering.Data
{
    public sealed class StoreContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<MockUserIdentity> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItem { get; set; }


        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MockUserIdentity>().HasData(new List<MockUserIdentity>
            {
                new MockUserIdentity
                {
                    Id = 1,
                    Phone = 89245678996
                }
            });

            modelBuilder.Entity<Item>().HasData(new List<Item>
            {
                new Item
                {
                    Id = 1,
                    Description = "Товар для тела",
                    Name = "Lousvit 12mg",
                    Price = 12500
                }
            });

            modelBuilder.Entity<Cart>().HasData(new List<Cart>
            {
                new Cart
                {
                    Id = 1,
                    CreatorId = 1,
                }
            });

            modelBuilder.Entity<Order>().HasData(new List<Order>
            {
                new Order
                {
                    Id = 1,
                    CreatorId = 1,
                    CartId = 1
                }
            });

            modelBuilder.Entity<CartItem>().HasData(new List<CartItem>
            {
                new CartItem
                {
                    Id = 1,
                    CartId = 1,
                    ItemId = 1
                }
            });

        }
    }
}
