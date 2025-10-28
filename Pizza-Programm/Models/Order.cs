// /Models/Order.cs
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeinPizzaShopProjekt.Models
{
    public partial class Order : ObservableObject
    {
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private DateTime _orderTime; // Wann wurde bestellt?

        [ObservableProperty]
        private string _customerNote; // Bemerkung für die ganze Bestellung

        [ObservableProperty]
        private decimal _totalPrice;

        [ObservableProperty]
        private OrderStatus _status;

        [ObservableProperty]
        private ICollection<OrderItem> _orderItems = new List<OrderItem>();
    }
}