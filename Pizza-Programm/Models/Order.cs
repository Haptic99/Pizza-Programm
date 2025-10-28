// /Models/Order.cs
using CommunityToolkit.Mvvm.ComponentModel;
using Pizza_Programm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


.Models
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