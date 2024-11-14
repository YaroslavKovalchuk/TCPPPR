using Microsoft.EntityFrameworkCore;
using TCPPR.Data.Entities;

namespace TCPPR.Data.DatabaseContext
{
    /// <summary>
    /// Контекст бази даних для доступу до таблиць, включаючи користувачів, обладнання, інвентар і заявки на обслуговування.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Таблиці для різних сутностей
        public DbSet<User> Users { get; set; } // Таблиця користувачів
        public DbSet<Equipment> Equipments { get; set; } // Таблиця обладнання
        public DbSet<SparePart> SpareParts { get; set; } // Таблиця запасних частин
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; } // Таблиця заявок на техобслуговування

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфігурація для таблиці Users
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.Role).IsRequired().HasMaxLength(50);
                entity.Property(u => u.AccessLevel).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Workshop).IsRequired().HasMaxLength(100); // Конфігурація для цеху
            });

            // Конфігурація для таблиці Equipments
            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.InventoryNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.QRCode).HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(250);
            });

            // Конфігурація для таблиці SpareParts
            modelBuilder.Entity<SparePart>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.PartNumber).HasMaxLength(50);
                entity.Property(s => s.Manufacturer).HasMaxLength(50);
                entity.Property(s => s.Quantity).IsRequired();
                entity.Property(s => s.Barcode).HasMaxLength(50);
                entity.Property(s => s.StorageLocation).HasMaxLength(100);
            });

            // Конфігурація для таблиці MaintenanceRequests
            modelBuilder.Entity<MaintenanceRequest>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Description).HasMaxLength(500);
                entity.Property(m => m.Status).IsRequired().HasMaxLength(50);
            });
        }
    }
}
