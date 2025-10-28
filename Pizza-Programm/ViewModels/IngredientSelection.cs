// /ViewModels/IngredientSelection.cs
using CommunityToolkit.Mvvm.ComponentModel;
using Pizza_Programm.Models;

// CORRECTED: Namespace added
namespace Pizza_Programm.ViewModels
{
    // This class "wraps" an ingredient and adds an IsSelected property
    // for the checkboxes in the GUI.
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