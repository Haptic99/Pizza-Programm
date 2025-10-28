// /Models/PizzaIngredient.cs
using Pizza_Programm.Models;

namespace DeinPizzaShopProjekt.Models
{
    // Das ist eine "many-to-many" Verbindungstabelle.
    // Sie sagt, welche Zutat zu welcher Pizza gehört.
    public class PizzaIngredient
    {
        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}