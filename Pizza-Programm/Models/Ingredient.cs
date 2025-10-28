// /Models/Ingredient.cs
using CommunityToolkit.Mvvm.ComponentModel;



.Models
{
    // Wir erben von ObservableObject, damit die GUI auf Änderungen reagieren kann
    // (z.B. wenn sich der Lagerbestand ändert)
    public partial class Ingredient : ObservableObject
    {
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private int _stockLevel; // Bonus: Lagerbestand
    }
}