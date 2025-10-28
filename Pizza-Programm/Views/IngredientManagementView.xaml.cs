// /Views/IngredientManagementView.xaml.cs
using DeinPizzaShopProjekt.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DeinPizzaShopProjekt.Views
{
    public partial class IngredientManagementView : UserControl
    {
        public IngredientManagementView()
        {
            InitializeComponent();

            // Wir müssen dem Loaded-Event manuell sagen, 
            // dass es asynchron ausgeführt werden soll.
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Sicherstellen, dass der DataContext vorhanden ist
            if (DataContext is IngredientManagementViewModel viewModel)
            {
                // Den Lade-Befehl im ViewModel asynchron aufrufen
                if (viewModel.LoadIngredientsCommand.CanExecute(null))
                {
                    await viewModel.LoadIngredientsCommand.ExecuteAsync(null);
                }
            }
        }
    }
}