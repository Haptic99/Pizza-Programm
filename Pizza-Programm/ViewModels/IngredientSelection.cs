// /ViewModels/IngredientSelection.cs
using CommunityToolkit.Mvvm.ComponentModel;
using Pizza_Programm.Models;


.ViewModels
{
    // Diese Klasse "verpackt" eine Zutat und fügt eine IsSelected-Eigenschaft
    // für die Checkboxen in der GUI hinzu.
    public partial class IngredientSelection : ObservableObject
    {
        [ObservableProperty]
        private bool _isSelected;

        [ObservableProperty]
        private Ingredient _ingredient;

        public IngredientSelection(Ingredient ingredient, bool isSelected = false)
        {
            _ingredient = ingredient;
            _isSelected = isSelected;
        }
    }
}