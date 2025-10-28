// /Data/PizzaDbContext.cs
using Pizza_Programm.Models;
using Microsoft.EntityFrameworkCore;

// KORRIGIERT: Namespace hinzugefügt
namespace Pizza_Programm.Data
{
    public class PizzaDbContext : DbContext
    {
        // Tabellen-Definitionen
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<PizzaIngredient> PizzaIngredients { get; set; }

        // --- NEU ---
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        // -----------

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=pizzashop.db");

            // (Optional, aber gut für die Entwicklung, um die DB-Abfragen zu sehen)
            // .EnableSensitiveDataLogging()
            // .LogTo(System.Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- Bestehender Code (Relationen) ---
            modelBuilder.Entity<PizzaIngredient>()
                .HasKey(pi => new { pi.PizzaId, pi.IngredientId });

            modelBuilder.Entity<PizzaIngredient>()
                .HasOne(pi => pi.Pizza)
                .WithMany(p => p.PizzaIngredients)
                .HasForeignKey(pi => pi.PizzaId);

            modelBuilder.Entity<PizzaIngredient>()
                .HasOne(pi => pi.Ingredient)
                .WithMany()
                .HasForeignKey(pi => pi.IngredientId);

            // --- NEU (Relationen) ---
            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            // --- AUFGERÄUMT: Seed-Logik wird ausgelagert ---
            DataSeeder.Seed(modelBuilder);
        }
    }
}