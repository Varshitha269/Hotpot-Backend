using HotpotLibrary.DTO;
using HotpotLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

       

        public virtual DbSet<Cart> Carts { get; set; }

        public virtual DbSet<FeedbackRating> FeedbackRatings { get; set; }

        public virtual DbSet<Menu> Menus { get; set; }

        public virtual DbSet<MenuItem> MenuItems { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }

        public virtual DbSet<Restaurant> Restaurants { get; set; }

        public virtual DbSet<User> Users { get; set; }
        public DbSet<RestaurantMenuItem> RestaurantsMenuItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

            => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=HotpotFinal;Integrated Security=True;TrustServerCertificate=true");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RestaurantMenuItem>()
            .HasNoKey();
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.CartID).HasName("PK__Cart__51BCD797E2EC6018");

                entity.ToTable("Cart");

                entity.Property(e => e.CartID).HasColumnName("CartID");
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.MenuItemID).HasColumnName("MenuItemID");
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.UserID).HasColumnName("UserId");

                entity.HasOne(d => d.MenuItem).WithMany(p => p.Carts)
                    .HasForeignKey(d => d.MenuItemID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_MenuItem");

                entity.HasOne(d => d.User).WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_User");
            });

            modelBuilder.Entity<FeedbackRating>(entity =>
            {
                entity.HasKey(e => e.FeedbackRatingID).HasName("PK__Feedback__82BC7D967C09241A");

                entity.Property(e => e.FeedbackRatingID).HasColumnName("FeedbackRatingID");
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Message).HasColumnType("text");
                entity.Property(e => e.RestaurantID).HasColumnName("RestaurantID");
                entity.Property(e => e.UserID).HasColumnName("UserId");

                entity.HasOne(d => d.Restaurant).WithMany(p => p.FeedbackRatings)
                    .HasForeignKey(d => d.RestaurantID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeedbackRatings_Restaurant");

                entity.HasOne(d => d.User).WithMany(p => p.FeedbackRatings)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FeedbackRatings_User");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.MenuID).HasName("PK__Menu__C99ED25019012F8A");

                entity.ToTable("Menu");

                entity.Property(e => e.MenuID).HasColumnName("MenuID");
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.MenuName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.RestaurantID).HasColumnName("RestaurantID");

                entity.HasOne(d => d.Restaurant).WithMany(p => p.Menus)
                    .HasForeignKey(d => d.RestaurantID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_Restaurant");
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(e => e.MenuItemID).HasName("PK__MenuItem__8943F7020A857266");

                entity.Property(e => e.MenuItemID).HasColumnName("MenuItemID");
                entity.Property(e => e.AvailabilityTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.Discounts)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(5, 2)");
                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ImageUrl");
                entity.Property(e => e.IsAvailable).HasDefaultValue(true);
                entity.Property(e => e.IsInStock).HasDefaultValue(true);
                entity.Property(e => e.ItemName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.MenuID).HasColumnName("MenuID");
                entity.Property(e => e.NutritionalInfo)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.SpecialDietaryInfo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.TasteInfo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Menu).WithMany(p => p.MenuItems)
                    .HasForeignKey(d => d.MenuID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuItems_Menu");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderID).HasName("PK__Orders__C3905BAFC48C277F");

                entity.Property(e => e.OrderID).HasColumnName("OrderID");
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.DeliveryAddress)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
                entity.Property(e => e.OrderDate).HasColumnType("datetime");
                entity.Property(e => e.OrderStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.PaymentStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.RestaurantID).HasColumnName("RestaurantID");
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.UserID).HasColumnName("UserId");

                entity.HasOne(d => d.Restaurant).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.RestaurantID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Restaurant");

                entity.HasOne(d => d.User).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_User");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailID).HasName("PK__OrderDet__D3B9D30C76FAB422");

                entity.Property(e => e.OrderDetailID).HasColumnName("OrderDetailID");
                entity.Property(e => e.MenuItemID).HasColumnName("MenuItemID");
                entity.Property(e => e.OrderID).HasColumnName("OrderID");
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Subtotal)
                    .HasComputedColumnSql("([Quantity]*[Price])", true)
                    .HasColumnType("decimal(21, 2)");

                entity.HasOne(d => d.MenuItem).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.MenuItemID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_MenuItem");

                entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Order");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PaymentID).HasName("PK__Payments__9B556A58A2B413D1");

                entity.Property(e => e.PaymentID).HasColumnName("PaymentID");
                entity.Property(e => e.AmountPaid).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.OrderID).HasColumnName("OrderID");
                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.TransactionDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.TransactionStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payments_Order");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.HasKey(e => e.RestaurantID).HasName("PK__Restaura__87454CB5E6FCA662");

                entity.HasIndex(e => e.Email, "UQ__Restaura__A9D10534AB7192F4").IsUnique();

                entity.Property(e => e.RestaurantID).HasColumnName("RestaurantID");
                entity.Property(e => e.AddressLine)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Country)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.OperatingHours)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.PhNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.PostalCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserID).HasName("PK__Users__1788CCAC0473EA0F");

                entity.HasIndex(e => e.Email, "UQ__Users__A9D105343CD5B57B").IsUnique();

                entity.Property(e => e.UserID).HasColumnName("UserId");
                entity.Property(e => e.AddressLine)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Country)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.PhNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.PostalCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });


        }
    }
}
