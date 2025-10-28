// /ViewModels/PizzaManagementViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pizza_Programm.Data;
using Pizza_Programm.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Pizza_Programm.ViewModels
{
    public partial class PizzaManagementViewModel : ObservableObject
    {
        // Listen für die GUI
        public ObservableCollection<Pizza> Pizzas { get; } = new();
        public ObservableCollection<IngredientSelection> IngredientSelections { get; } = new();

        // Eigenschaften für neue Pizzen
        [ObservableProperty]
        private string _newPizzaName;

        [ObservableProperty]
        private decimal _newPizzaPrice;

        // Die aktuell im UI ausgewählte Pizza
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DeletePizzaCommand))]
        [NotifyCanExecuteChangedFor(nameof(SavePizzaIngredientsCommand))]
        private Pizza _selectedPizza;

        partial void OnSelectedPizzaChanged(Pizza value)
        {
            UpdateIngredientSelection();
        }

        private void UpdateIngredientSelection()
        {
            if (SelectedPizza == null)
            {
                foreach (var selection in IngredientSelections)
                {
                    selection.IsSelected = false;
                }
            }
            else
            {
                var pizzaIngredientIds = new HashSet<int>(
                    SelectedPizza.PizzaIngredients.Select(pi => pi.IngredientId)
                );

                foreach (var selection in IngredientSelections)
                {
                    selection.IsSelected = pizzaIngredientIds.Contains(selection.Ingredient.Id);
                }
            }
        }

        [RelayCommand]
        private async Task LoadDataAsync()
        {
            Pizzas.Clear();
            IngredientSelections.Clear();

            using (var context = new PizzaDbContext())
            {
                await context.Database.EnsureCreatedAsync();

                var pizzasFromDb = await context.Pizzas
                    .Include(p => p.PizzaIngredients)
                    .ToListAsync();

                foreach (var pizza in pizzasFromDb)
                {
                    Pizzas.Add(pizza);
                }

                var allIngredients = await context.Ingredients.ToListAsync();
                foreach (var ingredient in allIngredients)
                {
                    IngredientSelections.Add(new IngredientSelection(ingredient));
                }
            }
        }

        [RelayCommand]
        private async Task AddPizzaAsync()
        {
            if (string.IsNullOrWhiteSpace(NewPizzaName) || NewPizzaPrice <= 0)
            {
                MessageBox.Show("Bitte einen gültigen Namen und Preis eingeben.", "Fehler");
                return;
            }

            var newPizza = new Pizza { Name = NewPizzaName, Price = NewPizzaPrice };

            using (var context = new PizzaDbContext())
            {
                context.Pizzas.Add(newPizza);
                await context.SaveChangesAsync();
            }

            Pizzas.Add(newPizza);
            NewPizzaName = string.Empty;
            NewPizzaPrice = 0;
        }

        [RelayCommand(CanExecute = nameof(CanDeletePizza))]
        private async Task DeletePizzaAsync()
        {
            if (SelectedPizza == null) return;

            var result = MessageBox.Show($"Soll die Pizza '{SelectedPizza.Name}' wirklich gelöscht werden?",
                "Löschen bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.No) return;

            using (var context = new PizzaDbContext())
            {
                context.Pizzas.Attach(SelectedPizza);
                context.Pizzas.Remove(SelectedPizza);
                await context.SaveChangesAsync();
            }

            Pizzas.Remove(SelectedPizza);
            SelectedPizza = null;
        }
        private bool CanDeletePizza() => SelectedPizza != null;


        [RelayCommand(CanExecute = nameof(CanSavePizzaIngredients))]
        private async Task SavePizzaIngredientsAsync()
        {
            if (SelectedPizza == null) return;

            using (var context = new PizzaDbContext())
            {
                var pizzaFromDb = await context.Pizzas
                    .Include(p => p.PizzaIngredients)
                    .FirstOrDefaultAsync(p => p.Id == SelectedPizza.Id);

                if (pizzaFromDb == null) return;

                pizzaFromDb.PizzaIngredients.Clear();

                foreach (var selection in IngredientSelections)
                {
                    if (selection.IsSelected)
                    {
                        pizzaFromDb.PizzaIngredients.Add(new PizzaIngredient
                        {
                            PizzaId = pizzaFromDb.Id,
                            IngredientId = selection.Ingredient.Id
                        });
                    }
                }

                await context.SaveChangesAsync();
                SelectedPizza.PizzaIngredients = pizzaFromDb.PizzaIngredients;
                OnSelectedPizzaChanged(SelectedPizza);
            }

            MessageBox.Show("Zutaten gespeichert!", "Erfolg");
        }
        private bool CanSavePizzaIngredients() => SelectedPizza != null;
    }
}