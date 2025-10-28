// /ViewModels/IngredientManagementViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pizza_Programm.Data;
using Pizza_Programm.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

// CORRECTED: Namespace was already correct
namespace Pizza_Programm.ViewModels
{
    // partial, because the CommunityToolkit generates code (e.g., Commands)
    public partial class IngredientManagementViewModel : ObservableObject
    {
        // A special list that automatically informs the GUI about changes
        public ObservableCollection<Ingredient> Ingredients { get; } = new();

        [ObservableProperty]
        private string _newIngredientName;

        [ObservableProperty]
        private Ingredient _selectedIngredient;

        [RelayCommand]
        private async Task LoadIngredientsAsync()
        {
            Ingredients.Clear();
            using (var context = new PizzaDbContext())
            {
                // Create database if it doesn't exist
                await context.Database.EnsureCreatedAsync();

                var ingredientsFromDb = await context.Ingredients.ToListAsync();
                foreach (var ingredient in ingredientsFromDb)
                {
                    Ingredients.Add(ingredient);
                }
            }
        }

        [RelayCommand]
        private async Task AddIngredientAsync()
        {
            if (string.IsNullOrWhiteSpace(NewIngredientName))
                return;

            var newIngredient = new Ingredient { Name = NewIngredientName, StockLevel = 0 };

            using (var context = new PizzaDbContext())
            {
                context.Ingredients.Add(newIngredient);
                await context.SaveChangesAsync();
            }

            Ingredients.Add(newIngredient); // Add to the list in the GUI
            NewIngredientName = string.Empty; // Clear textbox
        }

        [RelayCommand]
        private async Task DeleteIngredientAsync()
        {
            if (SelectedIngredient == null)
                return;

            using (var context = new PizzaDbContext())
            {
                context.Ingredients.Attach(SelectedIngredient);
                context.Ingredients.Remove(SelectedIngredient);
                await context.SaveChangesAsync();
            }

            Ingredients.Remove(SelectedIngredient); // Remove from the GUI list
        }
    }
}