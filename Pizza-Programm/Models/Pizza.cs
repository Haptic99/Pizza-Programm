// /Models/Pizza.cs
using CommunityToolkit.Mvvm.ComponentModel;
using Pizza_Programm.Models;
using System.Collections.Generic;

// CORRECTED: Namespace added
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

        // Every pizza has a list of ingredients
        [ObservableProperty]
        private ICollection<PizzaIngredient> _pizzaIngredients = new List<PizzaIngredient>();
    }
}