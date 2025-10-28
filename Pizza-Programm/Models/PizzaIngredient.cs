// /Models/PizzaIngredient.cs
using Pizza_Programm.Models;

// CORRECTED: Namespace added
namespace Pizza_Programm.Models
{
    // This is a "many-to-many" join table.
    // It specifies which ingredient belongs to which pizza.
    public class PizzaIngredient
    {
        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}