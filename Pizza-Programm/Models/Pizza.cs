// /Models/Pizza.cs
using CommunityToolkit.Mvvm.ComponentModel;
using Pizza_Programm.Models;
using System.Collections.Generic;

namespace Pizza_Programm.Models
{
    public partial class Pizza : ObservableObject
    {
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private decimal _price;

        // Jede Pizza hat eine Liste von Zutaten
        [ObservableProperty]
        private ICollection<PizzaIngredient> _pizzaIngredients = new List<PizzaIngredient>();
    }
}