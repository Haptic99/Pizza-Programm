// /ViewModels/OrderViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pizza_Programm.Data;
using Pizza_Programm.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

// KORRIGIERT: Namespace hinzugefügt
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
            if (!Cart.Any())
            {
                MessageBox.Show("Der Warenkorb ist leer.", "Hinweis");
                return;
            }

            var order = new Order
            {
                OrderTime = System.DateTime.Now,
                Status = OrderStatus.Pending,
                CustomerNote = this.CustomerNote,
                TotalPrice = this.TotalPrice
            };

            foreach (var cartItem in Cart)
            {
                order.OrderItems.Add(cartItem.CreateOrderItemModel());
            }

            using (var context = new PizzaDbContext())
            {
                context.Orders.Add(order);
                await context.SaveChangesAsync();
            }

            Cart.Clear();
            CustomerNote = string.Empty;

            MessageBox.Show("Bestellung wurde aufgenommen und an die Küche gesendet!", "Erfolg");
        }
    }
}