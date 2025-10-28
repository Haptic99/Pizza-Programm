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
                // IMPORTANT: Create database if not present
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
            // IMMEDIATE confirmation that the method is called
            MessageBox.Show("SubmitOrderAsync was called!", "Debug", MessageBoxButton.OK, MessageBoxImage.Information);

            try
            {
                // Debug output
                System.Diagnostics.Debug.WriteLine("SubmitOrderAsync was called");

                if (!Cart.Any())
                {
                    MessageBox.Show("The cart is empty.", "Note", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Create order
                var order = new Order
                {
                    OrderTime = DateTime.Now,
                    Status = OrderStatus.Pending,
                    CustomerNote = this.CustomerNote ?? string.Empty,
                    TotalPrice = this.TotalPrice
                };

                // Add OrderItems
                foreach (var cartItem in Cart)
                {
                    var orderItem = cartItem.CreateOrderItemModel();
                    order.OrderItems.Add(orderItem);
                }

                // Save to database
                using (var context = new PizzaDbContext())
                {
                    // Ensure DB exists
                    await context.Database.EnsureCreatedAsync();

                    context.Orders.Add(order);

                    System.Diagnostics.Debug.WriteLine($"Saving order with {order.OrderItems.Count} items");

                    await context.SaveChangesAsync();

                    System.Diagnostics.Debug.WriteLine("Order saved successfully");
                }

                // Clear cart
                Cart.Clear();
                CustomerNote = string.Empty;

                MessageBox.Show(
                    $"Order #{order.Id} has been submitted!\n\n" +
                    $"Number of pizzas: {order.OrderItems.Count}\n" +
                    $"Total price: {order.TotalPrice:C}\n\n" +
                    "The kitchen has been notified.",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (DbUpdateException dbEx)
            {
                var innerMessage = dbEx.InnerException?.Message ?? "No details available";
                MessageBox.Show(
                    $"Database error while saving:\n\n{dbEx.Message}\n\nDetails:\n{innerMessage}",
                    "Database Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                System.Diagnostics.Debug.WriteLine($"DbUpdateException: {dbEx}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unexpected error:\n\n{ex.Message}\n\nType: {ex.GetType().Name}\n\nStackTrace:\n{ex.StackTrace}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                System.Diagnostics.Debug.WriteLine($"Exception: {ex}");
            }
        }
    }
}