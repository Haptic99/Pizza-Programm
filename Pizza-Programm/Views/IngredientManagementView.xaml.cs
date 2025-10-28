// /Views/IngredientManagementView.xaml.cs
using Pizza_Programm.ViewModels;
using System.Windows;
using System.Windows.Controls;

// CORRECTED: Namespace added
namespace Pizza_Programm.Views
{
    public partial class IngredientManagementView : UserControl
    {
        public IngredientManagementView()
        {
            InitializeComponent();

            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is IngredientManagementViewModel viewModel)
            {
                if (viewModel.LoadIngredientsCommand.CanExecute(null))
                {
                    await viewModel.LoadIngredientsCommand.ExecuteAsync(null);
                }
            }
        }
    }
}