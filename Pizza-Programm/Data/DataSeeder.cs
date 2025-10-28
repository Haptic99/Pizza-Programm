// /Data/DataSeeder.cs
using Microsoft.EntityFrameworkCore;
using Pizza_Programm.Models;

namespace Pizza_Programm.Data
{
    public static class DataSeeder
    {
        // Haupt-Seed-Methode, die vom DbContext aufgerufen wird
        public static void Seed(ModelBuilder modelBuilder)
        {
            // 1. Zuerst die Zutaten erstellen
            SeedIngredients(modelBuilder);

            // 2. Dann die Pizzen erstellen
            SeedPizzas(modelBuilder);

            // 3. Zuletzt die Pizzen mit den Zutaten verknüpfen
            SeedPizzaIngredients(modelBuilder);
        }

        private static void SeedIngredients(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Anchovy", StockLevel = 100 },
                new Ingredient { Id = 2, Name = "Banana", StockLevel = 100 },
                new Ingredient { Id = 3, Name = "Basil", StockLevel = 100 },
                new Ingredient { Id = 4, Name = "Black Olives", StockLevel = 100 },
                new Ingredient { Id = 5, Name = "Capers", StockLevel = 100 },
                new Ingredient { Id = 6, Name = "Champignon", StockLevel = 100 },
                new Ingredient { Id = 7, Name = "Cherry Tomato", StockLevel = 100 },
                new Ingredient { Id = 8, Name = "Garlic", StockLevel = 100 },
                new Ingredient { Id = 9, Name = "German Cooked ham", StockLevel = 100 },
                new Ingredient { Id = 10, Name = "Gorgonzola", StockLevel = 100 },
                new Ingredient { Id = 11, Name = "Jalapeno", StockLevel = 100 },
                new Ingredient { Id = 12, Name = "Lemon", StockLevel = 100 },
                new Ingredient { Id = 13, Name = "Mature Cheedar", StockLevel = 100 },
                new Ingredient { Id = 14, Name = "Mozzarella", StockLevel = 100 },
                new Ingredient { Id = 15, Name = "Nutella", StockLevel = 100 },
                new Ingredient { Id = 16, Name = "Parma Ham", StockLevel = 100 },
                new Ingredient { Id = 17, Name = "Pepperoni", StockLevel = 100 },
                new Ingredient { Id = 18, Name = "Pineapple", StockLevel = 100 },
                new Ingredient { Id = 19, Name = "Red Onion", StockLevel = 100 },
                new Ingredient { Id = 20, Name = "Salad", StockLevel = 100 },
                new Ingredient { Id = 21, Name = "Seafood", StockLevel = 100 },
                new Ingredient { Id = 22, Name = "Smoked Cheese", StockLevel = 100 },
                new Ingredient { Id = 23, Name = "Smoked Ham", StockLevel = 100 },
                new Ingredient { Id = 24, Name = "Smoked Salmon", StockLevel = 100 },
                new Ingredient { Id = 25, Name = "Spicy Chorizo", StockLevel = 100 },
                new Ingredient { Id = 26, Name = "Spinach", StockLevel = 100 },
                new Ingredient { Id = 27, Name = "Strawberry", StockLevel = 100 },
                new Ingredient { Id = 28, Name = "Sweet Corn", StockLevel = 100 },
                new Ingredient { Id = 29, Name = "Tomato Sauce", StockLevel = 100 },
                new Ingredient { Id = 30, Name = "Truffle Sauce", StockLevel = 100 },
                new Ingredient { Id = 31, Name = "Tuna", StockLevel = 100 },
                new Ingredient { Id = 32, Name = "White Sauce", StockLevel = 100 }
            );
        }

        private static void SeedPizzas(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>().HasData(
                new Pizza { Id = 1, Name = "Diavola", Price = 9.50m },
                new Pizza { Id = 2, Name = "Hawaii", Price = 9.50m },
                new Pizza { Id = 3, Name = "Margherita", Price = 8.00m },
                new Pizza { Id = 4, Name = "Mexican Hawaii", Price = 10.50m },
                new Pizza { Id = 5, Name = "Mexican Pepperoni", Price = 10.50m },
                new Pizza { Id = 6, Name = "Napoli", Price = 9.00m },
                new Pizza { Id = 7, Name = "Nutella Banana", Price = 7.50m },
                new Pizza { Id = 8, Name = "Nutella Strawberry", Price = 7.50m },
                new Pizza { Id = 9, Name = "Parma Ham & Rocket", Price = 11.50m },
                new Pizza { Id = 10, Name = "Pepperoni", Price = 9.50m },
                new Pizza { Id = 11, Name = "Quattro Formaggi", Price = 10.50m },
                new Pizza { Id = 12, Name = "Salmone", Price = 12.00m },
                new Pizza { Id = 13, Name = "Seafood TomatoSauce/Pesto Sauce", Price = 12.50m },
                new Pizza { Id = 14, Name = "Spinach /Ham/Mushroom", Price = 10.00m },
                new Pizza { Id = 15, Name = "Sweet corn", Price = 9.50m },
                new Pizza { Id = 16, Name = "Tonno", Price = 9.50m },
                new Pizza { Id = 17, Name = "Truffle & Mushroom", Price = 8.00m }
            );
        }

        private static void SeedPizzaIngredients(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PizzaIngredient>().HasData(
                new PizzaIngredient { PizzaId = 1, IngredientId = 11 },
                new PizzaIngredient { PizzaId = 1, IngredientId = 14 },
                new PizzaIngredient { PizzaId = 1, IngredientId = 19 },
                new PizzaIngredient { PizzaId = 1, IngredientId = 25 },
                new PizzaIngredient { PizzaId = 1, IngredientId = 29 },
                new PizzaIngredient { PizzaId = 2, IngredientId = 14 },
                new PizzaIngredient { PizzaId = 2, IngredientId = 18 },
                new PizzaIngredient { PizzaId = 2, IngredientId = 23 },
                new PizzaIngredient { PizzaId = 2, IngredientId = 29 },
                new PizzaIngredient { PizzaId = 3, IngredientId = 3 },
                new PizzaIngredient { PizzaId = 3, IngredientId = 14 },
                new PizzaIngredient { PizzaId = 3, IngredientId = 29 },
                new PizzaIngredient { PizzaId = 4, IngredientId = 11 },
                new PizzaIngredient { PizzaId = 4, IngredientId = 14 },
                new PizzaIngredient { PizzaId = 4, IngredientId = 18 },
                new PizzaIngredient { PizzaId = 4, IngredientId = 23 },
                new PizzaIngredient { PizzaId = 4, IngredientId = 29 },
                new PizzaIngredient { PizzaId = 5, IngredientId = 3 },
                new PizzaIngredient { PizzaId = 5, IngredientId = 4 },
                new PizzaIngredient { PizzaId = 5, IngredientId = 8 },
                new PizzaIngredient { PizzaId = 5, IngredientId = 11 },
                new PizzaIngredient { PizzaId = 5, IngredientId = 14 },
                new PizzaIngredient { PizzaId = 5, IngredientId = 17 },
                new PizzaIngredient { PizzaId = 5, IngredientId = 18 },
                new PizzaIngredient { PizzaId = 5, IngredientId = 19 },
                new PizzaIngredient { PizzaId = 5, IngredientId = 29 },
                new PizzaIngredient { PizzaId = 6, IngredientId = 1 },
                new PizzaIngredient { PizzaId = 6, IngredientId = 4 },
                new PizzaIngredient { PizzaId = 6, IngredientId = 5 },
                new PizzaIngredient { PizzaId = 6, IngredientId = 8 },
                new PizzaIngredient { PizzaId = 6, IngredientId = 14 },
                new PizzaIngredient { PizzaId = 6, IngredientId = 29 },
                new PizzaIngredient { PizzaId = 7, IngredientId = 2 },
                new PizzaIngredient { PizzaId = 7, IngredientId = 15 },
                new PizzaIngredient { PizzaId = 8, IngredientId = 15 },
                new PizzaIngredient { PizzaId = 8, IngredientId = 27 },
                new PizzaIngredient { PizzaId = 9, IngredientId = 3 },
                new PizzaIngredient { PizzaId = 9, IngredientId = 4 },
                new PizzaIngredient { PizzaId = 9, IngredientId = 7 },
                new PizzaIngredient { PizzaId = 9, IngredientId = 8 },
                new PizzaIngredient { PizzaId = 9, IngredientId = 14 },
                new PizzaIngredient { PizzaId = 9, IngredientId = 16 },
                new PizzaIngredient { PizzaId = 9, IngredientId = 20 },
                new PizzaIngredient { PizzaId = 9, IngredientId = 29 },
                new PizzaIngredient { PizzaId = 10, IngredientId = 3 },
                new PizzaIngredient { PizzaId = 10, IngredientId = 14 },
                new PizzaIngredient { PizzaId = 10, IngredientId = 17 },
                new PizzaIngredient { PizzaId = 10, IngredientId = 29 },
                new PizzaIngredient { PizzaId = 11, IngredientId = 10 },
                new PizzaIngredient { PizzaId = 11, IngredientId = 13 },
                new PizzaIngredient { PizzaId = 11, IngredientId = 14 },
                new PizzaIngredient { PizzaId = 11, IngredientId = 22 },
                new PizzaIngredient { PizzaId = 11, IngredientId = 29 },
                new PizzaIngredient { PizzaId = 12, IngredientId = 5 },
                new PizzaIngredient { PizzaId = 12, IngredientId = 7 },
                new PizzaIngredient { PizzaId = 12, IngredientId = 12 },
                new PizzaIngredient { PizzaId = 12, IngredientId = 14 },
                new PizzaIngredient { PizzaId = 12, IngredientId = 20 },
                new PizzaIngredient { PizzaId = 12, IngredientId = 24 },
                new PizzaIngredient { PizzaId = 12, IngredientId = 29 },
                new PizzaIngredient { PizzaId = 13, IngredientId = 3 },
                new PizzaIngredient { PizzaId = 13, IngredientId = 4 },
                new PizzaIngredient { PizzaId = 13, IngredientId = 5 },
                new PizzaIngredient { PizzaId = 13, IngredientId = 6 },
                new PizzaIngredient { PizzaId = 13, IngredientId = 7 },
                new PizzaIngredient { PizzaId = 13, IngredientId = 8 },
                new PizzaIngredient { PizzaId = 13, IngredientId = 11 },
                new PizzaIngredient { PizzaId = 13, IngredientId = 14 },
                new PizzaIngredient { PizzaId = 13, IngredientId = 18 },
                new PizzaIngredient { PizzaId = 13, IngredientId = 19 },
                new PizzaIngredient { PizzaId = 13, IngredientId = 21 },
                new PizzaIngredient { PizzaId = 13, IngredientId = 29 },
                new PizzaIngredient { PizzaId = 14, IngredientId = 6 },
                new PizzaIngredient { PizzaId = 14, IngredientId = 14 },
                new PizzaIngredient { PizzaId = 14, IngredientId = 26 },
                new PizzaIngredient { PizzaId = 14, IngredientId = 32 },
                new PizzaIngredient { PizzaId = 15, IngredientId = 14 },
                new PizzaIngredient { PizzaId = 15, IngredientId = 28 },
                new PizzaIngredient { PizzaId = 15, IngredientId = 32 },
                new PizzaIngredient { PizzaId = 16, IngredientId = 4 },
                new PizzaIngredient { PizzaId = 16, IngredientId = 14 },
                new PizzaIngredient { PizzaId = 16, IngredientId = 19 },
                new PizzaIngredient { PizzaId = 16, IngredientId = 29 },
                new PizzaIngredient { PizzaId = 16, IngredientId = 31 },
                new PizzaIngredient { PizzaId = 17, IngredientId = 6 },
                new PizzaIngredient { PizzaId = 17, IngredientId = 9 },
                new PizzaIngredient { PizzaId = 17, IngredientId = 14 },
                new PizzaIngredient { PizzaId = 17, IngredientId = 30 }
            );
        }
    }
}