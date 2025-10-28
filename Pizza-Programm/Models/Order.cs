// /Models/Order.cs
using CommunityToolkit.Mvvm.ComponentModel;
using Pizza_Programm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// CORRECTED: Namespace added
namespace Pizza_Programm.Models
{
    public partial class Order : ObservableObject
    {
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private DateTime _orderTime; // When was it ordered?

        [ObservableProperty]
        private string _customerNote; // Note for the entire order

        [ObservableProperty]
        private decimal _totalPrice;

        [ObservableProperty]
        private OrderStatus _status;

        [ObservableProperty]
        private ICollection<OrderItem> _orderItems = new List<OrderItem>();
    }
}