using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStore.Core.Entities;

namespace SimpleStore.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCharacteristic> ItemCharacteristics { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>(ConfigureCategory);
            builder.Entity<Characteristic>(ConfigureCharacteristic);
            builder.Entity<Item>(ConfigureItem);
            builder.Entity<ItemCharacteristic>(ConfigureItemCharacteristic);
            builder.Entity<Order>(ConfigureOrder);
            builder.Entity<OrderDetail>(ConfigureOrderDetail);
        }

        private void ConfigureCategory(EntityTypeBuilder<Category> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired();

            builder.HasOne(e => e.ParentCategory)
                .WithMany()
                .HasForeignKey(e => e.ParentCategoryId);

            builder.HasMany(e => e.Items)
                .WithOne(e => e.Category);
        }

        private void ConfigureCharacteristic(EntityTypeBuilder<Characteristic> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.Value)
                .IsRequired();

            builder.HasMany(e => e.ItemCharacteristics);
        }

        private void ConfigureItem(EntityTypeBuilder<Item> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.Price)
                .IsRequired();

            builder.HasOne(e => e.Category)
                .WithMany(e => e.Items)
                .HasForeignKey(e => e.CategoryId);

            builder.HasMany(e => e.ItemCharacteristics);
        }

        private void ConfigureItemCharacteristic(EntityTypeBuilder<ItemCharacteristic> builder)
        {
            builder.HasKey(e => new { e.ItemId, e.CharacteristicId });

            builder.HasOne(e => e.Item)
                .WithMany(e => e.ItemCharacteristics)
                .HasForeignKey(e => e.ItemId);

            builder.HasOne(e => e.Characteristic)
                .WithMany(e => e.ItemCharacteristics)
                .HasForeignKey(e => e.CharacteristicId);
        }

        private void ConfigureOrder(EntityTypeBuilder<Order> builder)
        {
            builder.Property(e => e.Timestamp)
                .IsRequired();

            builder.HasMany(e => e.Details)
                .WithOne(e => e.Order);
        }

        private void ConfigureOrderDetail(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Property(e => e.Quantity)
                .IsRequired();
        }
    }
}
