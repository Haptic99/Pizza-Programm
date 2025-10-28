// /ViewModels/OrderViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DeinPizzaShopProjekt.Data;
using DeinPizzaShopProjekt.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DeinPizzaShopProjekt.ViewModels
{
    public partial class OrderViewModel : ObservableObject
    {
        // Spalte 1: Das Menü
        public ObservableCollection<Pizza> Menu { get; } = new();

        // Spalte 2: Der Warenkorb
        public ObservableCollection<OrderCartItemViewModel> Cart { get; } = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddToCartCommand))]
        private Pizza _selectedPizzaFromMenu;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RemoveFromCartCommand))]
        private OrderCartItemViewModel _selectedCartItem;

        [ObservableProperty]
        private string _customerNote;

        [ObservableProperty]
        private decimal _totalPrice;

        public OrderViewModel()
        {
            // Wir beobachten den Warenkorb. Wenn Pizzen hinzugefügt oder
            // entfernt werden, berechnen wir den Gesamtpreis neu.
            Cart.CollectionChanged += (s, e) => UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            TotalPrice = Cart.Sum(item => item.FinalPrice);
        }

        [RelayCommand]
        private async Task LoadMenuAsync()
        {
            Menu.Clear();
            Cart.Clear();

            using (var context = new PizzaDbContext())
            {
                await context.Database.EnsureCreatedAsync();

                // Lade alle Pizzen, UND ihre Zutaten-Beziehungen,
                // UND die Zutat-Details selbst.
                // Das ist wichtig für die Anpassungs-Checkliste!
                var pizzas = await context.Pizzas
                    .Include(p => p.PizzaIngredients)
                        .ThenInclude(pi => pi.Ingredient)
                    .ToListAsync();

                foreach (var pizza in pizzas)
                {
                    Menu.Add(pizza);
                }
            }
        }

        [RelayCommand(CanExecute = nameof(CanAddToCart))]
        private void AddToCart()
        {
            // Wir erstellen unseren "Wrapper" für den Warenkorb
            var cartItem = new OrderCartItemViewModel(SelectedPizzaFromMenu);
            Cart.Add(cartItem);
        }
        private bool CanAddToCart() => SelectedPizzaFromMenu != null;

        [RelayCommand(CanExecute = nameof(CanRemoveFromCart))]
        private void RemoveFromCart()
        {
            Cart.Remove(SelectedCartItem);
        }
        private bool CanRemoveFromCart() => SelectedCartItem != null;


        [RelayCommand]
        private async Task SubmitOrderAsync()
        {
            if (!Cart.Any())
            {
                MessageBox.Show("Der Warenkorb ist leer.", "Hinweis");
                return;
            }

            // 1. Erstelle das Haupt-Bestellobjekt
            var order = new Order
            {
                OrderTime = DateTime.Now,
                Status = OrderStatus.Pending, // "Offen" für die Küche
                CustomerNote = this.CustomerNote,
                TotalPrice = this.TotalPrice
            };

            // 2. Wandle jeden "Warenkorb-Wrapper" in einen
            //    echten "OrderItem" (Datenbank-Objekt) um.
            foreach (var cartItem in Cart)
            {
                order.OrderItems.Add(cartItem.CreateOrderItemModel());
            }

            // 3. Speichere alles in der Datenbank
            using (var context = new PizzaDbContext())
            {
                context.Orders.Add(order);
                await context.SaveChangesAsync();
            }

            // 4. GUI aufräumen
            Cart.Clear();
            CustomerNote = string.Empty;

            MessageBox.Show("Bestellung wurde aufgenommen und an die Küche gesendet!", "Erfolg");
        }
    }
}