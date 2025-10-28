// /ViewModels/OrderViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pizza_Programm.Data;
using Pizza_Programm.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System;

namespace Pizza_Programm.ViewModels
{
    public partial class OrderViewModel : ObservableObject
    {
        public ObservableCollection<Pizza> Menu { get; } = new();
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
                // WICHTIG: Datenbank erstellen falls nicht vorhanden
                await context.Database.EnsureCreatedAsync();

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
            // SOFORTIGE Bestätigung, dass die Methode aufgerufen wird
            MessageBox.Show("SubmitOrderAsync wurde aufgerufen!", "Debug", MessageBoxButton.OK, MessageBoxImage.Information);

            try
            {
                // Debug-Ausgabe
                System.Diagnostics.Debug.WriteLine("SubmitOrderAsync wurde aufgerufen");

                if (!Cart.Any())
                {
                    MessageBox.Show("Der Warenkorb ist leer.", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Bestellung erstellen
                var order = new Order
                {
                    OrderTime = DateTime.Now,
                    Status = OrderStatus.Pending,
                    CustomerNote = this.CustomerNote ?? string.Empty,
                    TotalPrice = this.TotalPrice
                };

                // OrderItems hinzufügen
                foreach (var cartItem in Cart)
                {
                    var orderItem = cartItem.CreateOrderItemModel();
                    order.OrderItems.Add(orderItem);
                }

                // In Datenbank speichern
                using (var context = new PizzaDbContext())
                {
                    // Sicherstellen, dass DB existiert
                    await context.Database.EnsureCreatedAsync();

                    context.Orders.Add(order);

                    System.Diagnostics.Debug.WriteLine($"Speichere Bestellung mit {order.OrderItems.Count} Items");

                    await context.SaveChangesAsync();

                    System.Diagnostics.Debug.WriteLine("Bestellung erfolgreich gespeichert");
                }

                // Warenkorb leeren
                Cart.Clear();
                CustomerNote = string.Empty;

                MessageBox.Show(
                    $"Bestellung #{order.Id} wurde aufgenommen!\n\n" +
                    $"Anzahl Pizzen: {order.OrderItems.Count}\n" +
                    $"Gesamtpreis: {order.TotalPrice:C}\n\n" +
                    "Die Küche wurde benachrichtigt.",
                    "Erfolg",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (DbUpdateException dbEx)
            {
                var innerMessage = dbEx.InnerException?.Message ?? "Keine Details verfügbar";
                MessageBox.Show(
                    $"Datenbankfehler beim Speichern:\n\n{dbEx.Message}\n\nDetails:\n{innerMessage}",
                    "Datenbankfehler",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                System.Diagnostics.Debug.WriteLine($"DbUpdateException: {dbEx}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unerwarteter Fehler:\n\n{ex.Message}\n\nTyp: {ex.GetType().Name}\n\nStackTrace:\n{ex.StackTrace}",
                    "Fehler",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                System.Diagnostics.Debug.WriteLine($"Exception: {ex}");
            }
        }
    }
}