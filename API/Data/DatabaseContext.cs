using BroGarage.API.Data.Entities;
using BroGarage.API.Data.Seeds;
using Microsoft.EntityFrameworkCore;

namespace BroGarage.API.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<CustomerTypeEntity> CustomerTypes => Set<CustomerTypeEntity>();

        public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();

        public DbSet<CarTypeEntity> CarTypes => Set<CarTypeEntity>();

        public DbSet<ManufacturerEntity> Manufacturers => Set<ManufacturerEntity>();

        public DbSet<CarEntity> Cars => Set<CarEntity>();

        public DbSet<ProductEntity> Products => Set<ProductEntity>();

        public DbSet<UserEntity> Users => Set<UserEntity>();

        public DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();

        public DbSet<OrderStatusEntity> OrderStatuses => Set<OrderStatusEntity>();

        public DbSet<OrderTypeEntity> OrderTypes => Set<OrderTypeEntity>();

        public DbSet<OrderEntity> Orders => Set<OrderEntity>();

        public DbSet<OrderDetailEntity> OrderDetails => Set<OrderDetailEntity>();

        public DbSet<TemplateEntity> Templates => Set<TemplateEntity>();

        public DbSet<TemplateDetailEntity> TemplateDetails => Set<TemplateDetailEntity>();

        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerTypeEntity>().HasData(CustomerTypeSeed.Data);

            modelBuilder.Entity<UserEntity>().HasData(UserSeed.Data);

            modelBuilder.Entity<OrderTypeEntity>().HasData(OrderTypeSeed.Data);

            modelBuilder.Entity<OrderStatusEntity>().HasData(OrderStatusSeed.Data);

            modelBuilder.Entity<ManufacturerEntity>().HasData(ManufacturerSeed.Data);

            modelBuilder.Entity<CarTypeEntity>().HasData(CarTypeSeed.Data);

            modelBuilder.Entity<CustomerEntity>().HasData(CustomerSeed.Data);

            modelBuilder.Entity<CarEntity>().HasData(CarSeed.Data);

            modelBuilder.Entity<ProductEntity>().HasData(ProductSeed.Data);

            modelBuilder.Entity<TemplateEntity>().HasData(TemplateSeed.Data);

            modelBuilder.Entity<TemplateDetailEntity>().HasData(TemplateDetailSeed.Data);

            modelBuilder.Entity<OrderEntity>().HasData(OrderSeed.Data);

            modelBuilder.Entity<OrderDetailEntity>().HasData(OrderDetailSeed.Data);
        }
    }
}
