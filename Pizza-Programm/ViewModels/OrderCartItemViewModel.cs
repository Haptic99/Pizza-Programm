// /ViewModels/OrderCartItemViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using Pizza_Programm.Models;
using System.Collections.ObjectModel;
using System.Linq;

// KORRIGIERT: Namespace hinzugefügt
namespace Pizza_Programm.ViewModels
{
    public partial class OrderCartItemViewModel : ObservableObject
    {
        public Pizza BasePizza { get; }

        public ObservableCollection<IngredientSelection> CustomizableIngredients { get; } = new();

        [ObservableProperty]
        private decimal _finalPrice;

        [ObservableProperty]
        private string _displayName;

        public OrderCartItemViewModel(Pizza basePizza)
        {
            BasePizza = basePizza;
            _finalPrice = basePizza.Price;
            _displayName = basePizza.Name;

            foreach (var pizzaIngredient in basePizza.PizzaIngredients)
            {
                var selection = new IngredientSelection(pizzaIngredient.Ingredient, isSelected: true);

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

        public OrderItem CreateOrderItemModel()
        {
            UpdateDisplayName();

            var customizations = CustomizableIngredients
                .Where(i => !i.IsSelected)
                .Select(i => $"Ohne {i.Ingredient.Name}");

            return new OrderItem
            {
                PizzaName = this.BasePizza.Name,
                PriceAtOrder = this.FinalPrice,
                Customizations = string.Join(", ", customizations)
            };
        }
    }
}