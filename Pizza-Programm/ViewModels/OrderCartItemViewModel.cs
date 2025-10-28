// /ViewModels/OrderCartItemViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using DeinPizzaShopProjekt.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace DeinPizzaShopProjekt.ViewModels
{
    // Dies ist ein "Wrapper" für eine Pizza, die im Warenkorb liegt.
    // Es ist noch *nicht* der finale Datenbank-Eintrag.
    public partial class OrderCartItemViewModel : ObservableObject
    {
        // Die Pizza aus dem Menü, auf der dieser Artikel basiert
        public Pizza BasePizza { get; }

        // Eine *eigene* Liste von Zutaten, nur für diesen Artikel,
        // damit der Benutzer sie abwählen kann.
        public ObservableCollection<IngredientSelection> CustomizableIngredients { get; } = new();

        [ObservableProperty]
        private decimal _finalPrice;

        // Wird im UI angezeigt, z.B. "Pizza Salami (ohne Oliven)"
        [ObservableProperty]
        private string _displayName;

        public OrderCartItemViewModel(Pizza basePizza)
        {
            BasePizza = basePizza;
            _finalPrice = basePizza.Price;
            _displayName = basePizza.Name;

            // Fülle die Zutaten-Liste nur mit den Zutaten,
            // die auf der Standard-Pizza drauf sind.
            foreach (var pizzaIngredient in basePizza.PizzaIngredients)
            {
                var selection = new IngredientSelection(pizzaIngredient.Ingredient, isSelected: true);

                // Wir beobachten, ob der Benutzer eine Checkbox ändert
                selection.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(IngredientSelection.IsSelected))
                    {
                        UpdateDisplayName();
                    }
                };
                CustomizableIngredients.Add(selection);
            }
        }

        // Aktualisiert den Namen im Warenkorb, um Kundenwünsche anzuzeigen
        public void UpdateDisplayName()
        {
            var removed = CustomizableIngredients
                .Where(i => !i.IsSelected)
                .Select(i => i.Ingredient.Name)
                .ToList();

            if (removed.Any())
            {
                DisplayName = $"{BasePizza.Name} (ohne {string.Join(", ", removed)})";
            }
            else
            {
                DisplayName = BasePizza.Name;
            }
        }

        // Diese Methode erstellt den finalen Datenbank-Eintrag
        public OrderItem CreateOrderItemModel()
        {
            UpdateDisplayName(); // Sicherstellen, dass der Name aktuell ist

            // Finde die abgewählten Zutaten
            var customizations = CustomizableIngredients
                .Where(i => !i.IsSelected)
                .Select(i => $"Ohne {i.Ingredient.Name}");

            return new OrderItem
            {
                PizzaName = this.BasePizza.Name, // Speichere den *Original-Namen*
                PriceAtOrder = this.FinalPrice,
                Customizations = string.Join(", ", customizations) // Speichere die Wünsche
            };
        }
    }
}