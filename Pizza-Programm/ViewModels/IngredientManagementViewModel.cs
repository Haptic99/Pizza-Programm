// /ViewModels/IngredientManagementViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DeinPizzaShopProjekt.Data;
using DeinPizzaShopProjekt.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DeinPizzaShopProjekt.ViewModels
{
    // partial, da der CommunityToolkit Code (z.B. Commands) generiert
    public partial class IngredientManagementViewModel : ObservableObject
    {
        // Eine spezielle Liste, die die GUI automatisch über Änderungen informiert
        public ObservableCollection<Ingredient> Ingredients { get; } = new();

        // [ObservableProperty] erstellt automatisch den Property-Code für _newIngredientName
        // und benachrichtigt die GUI bei Änderung (INotifyPropertyChanged).
        [ObservableProperty]
        private string _newIngredientName;

        [ObservableProperty]
        private Ingredient _selectedIngredient;

        // [RelayCommand] erstellt automatisch einen ICommand (z.B. AddIngredientCommand),
        // den wir im XAML an einen Button binden können.
        [RelayCommand]
        private async Task LoadIngredientsAsync()
        {
            Ingredients.Clear();
            using (var context = new PizzaDbContext())
            {
                // Datenbank erstellen, falls sie nicht existiert
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

            Ingredients.Add(newIngredient); // Zur Liste in der GUI hinzufügen
            NewIngredientName = string.Empty; // Textbox leeren
        }

        [RelayCommand]
        private async Task DeleteIngredientAsync()
        {
            if (SelectedIngredient == null)
                return;

            using (var context = new PizzaDbContext())
            {
                // Wir müssen das Objekt zuerst "anfügen" (attach), damit EF Core weiß,
                // welches Objekt in der DB gelöscht werden soll.
                context.Ingredients.Attach(SelectedIngredient);
                context.Ingredients.Remove(SelectedIngredient);
                await context.SaveChangesAsync();
            }

            Ingredients.Remove(SelectedIngredient); // Aus der GUI-Liste entfernen
        }
    }
}