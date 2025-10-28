// /Data/PizzaDbContext.cs
using DeinPizzaShopProjekt.Models;
using Microsoft.EntityFrameworkCore;

namespace DeinPizzaShopProjekt.Data
{
    public class PizzaDbContext : DbContext
    {
        // Tabellen-Definitionen
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<PizzaIngredient> PizzaIngredients { get; set; }
        // Hier kämen später noch z.B. DbSet<Order> hinzu

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Wir verwenden SQLite, das die Datenbank in einer einzigen Datei speichert.
            optionsBuilder.UseSqlite("Data Source=pizzashop.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Wir definieren hier den zusammengesetzten Primärschlüssel
            // für die "many-to-many" Beziehung.
            modelBuilder.Entity<PizzaIngredient>()
                .HasKey(pi => new { pi.PizzaId, pi.IngredientId });

            // Beziehung: Pizza -> PizzaIngredient
            modelBuilder.Entity<PizzaIngredient>()
                .HasOne(pi => pi.Pizza)
                .WithMany(p => p.PizzaIngredients)
                .HasForeignKey(pi => pi.PizzaId);

            // Beziehung: Ingredient -> PizzaIngredient
            modelBuilder.Entity<PizzaIngredient>()
                .HasOne(pi => pi.Ingredient)
                .WithMany() // Eine Zutat kann auf vielen Pizzen sein
                .HasForeignKey(pi => pi.IngredientId);
        }
    }
}