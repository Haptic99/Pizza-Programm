// /ViewModels/KitchenViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pizza_Programm.Data;
using Pizza_Programm.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Pizza_Programm.ViewModels
{
    public partial class KitchenViewModel : ObservableObject
    {
        public ObservableCollection<Order> PendingOrders { get; } = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CompleteOrderCommand))]
        [NotifyCanExecuteChangedFor(nameof(CancelOrderCommand))]
        private Order _selectedOrder;

        [RelayCommand]
        private async Task LoadPendingOrdersAsync()
        {
            PendingOrders.Clear();

            using (var context = new PizzaDbContext())
            {
                await context.Database.EnsureCreatedAsync();

                // Load only pending orders
                var pendingOrders = await context.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.Status == OrderStatus.Pending)
                    .OrderBy(o => o.OrderTime)
                    .ToListAsync();

                foreach (var order in pendingOrders)
                {
                    PendingOrders.Add(order);
                }
            }
        }

        [RelayCommand(CanExecute = nameof(CanCompleteOrder))]
        private async Task CompleteOrderAsync()
        {
            if (SelectedOrder == null) return;

            using (var context = new PizzaDbContext())
            {
                var orderFromDb = await context.Orders.FindAsync(SelectedOrder.Id);
                if (orderFromDb != null)
                {
                    orderFromDb.Status = OrderStatus.Completed;
                    await context.SaveChangesAsync();
                }
            }

            PendingOrders.Remove(SelectedOrder);
            SelectedOrder = null;

            MessageBox.Show("Order has been marked as completed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private bool CanCompleteOrder() => SelectedOrder != null;

        [RelayCommand(CanExecute = nameof(CanCancelOrder))]
        private async Task CancelOrderAsync()
        {
            if (SelectedOrder == null) return;

            var result = MessageBox.Show(
                $"Should order #{SelectedOrder.Id} really be cancelled?",
                "Confirm Cancellation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.No) return;

            using (var context = new PizzaDbContext())
            {
                var orderFromDb = await context.Orders.FindAsync(SelectedOrder.Id);
                if (orderFromDb != null)
                {
                    orderFromDb.Status = OrderStatus.Cancelled;
                    await context.SaveChangesAsync();
                }
            }

            PendingOrders.Remove(SelectedOrder);
            SelectedOrder = null;

            MessageBox.Show("Order was cancelled.", "Cancelled", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private bool CanCancelOrder() => SelectedOrder != null;
    }
}