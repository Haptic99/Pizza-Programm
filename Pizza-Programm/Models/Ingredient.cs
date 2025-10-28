// /Models/Ingredient.cs
using CommunityToolkit.Mvvm.ComponentModel;

// CORRECTED: Namespace added
namespace Pizza_Programm.Models
{
    // We inherit from ObservableObject so the GUI can react to changes
    // (e.g., if the stock level changes)
    public partial class Ingredient : ObservableObject
    {
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private int _stockLevel; // Bonus: Stock level
    }
}