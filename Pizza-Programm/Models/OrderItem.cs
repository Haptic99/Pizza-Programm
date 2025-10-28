// /Models/OrderItem.cs
using CommunityToolkit.Mvvm.ComponentModel;

// CORRECTED: Namespace added
namespace Pizza_Programm.Models
{
    // This is a snapshot of a pizza at the time of ordering.
    public partial class OrderItem : ObservableObject
    {
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private int _orderId;

        [ObservableProperty]
        private Order _order;

        [ObservableProperty]
        private string _pizzaName; // Name as a copy

        [ObservableProperty]
        private decimal _priceAtOrder; // Price as a copy

        // Here we save customer wishes, e.g., "Without Olives, Extra Cheese"
        [ObservableProperty]
        private string _customizations;
    }
}