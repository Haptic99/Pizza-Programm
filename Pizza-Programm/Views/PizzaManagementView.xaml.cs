// /Views/PizzaManagementView.xaml.cs
using DeinPizzaShopProjekt.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DeinPizzaShopProjekt.Views
{
    public partial class PizzaManagementView : UserControl
    {
        public PizzaManagementView()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Wir müssen das Laden der Daten manuell anstoßen,
            // da der 'Loaded'-Event selbst nicht async-fähig ist.
            if (DataContext is PizzaManagementViewModel viewModel)
            {
                if (viewModel.LoadDataCommand.CanExecute(null))
                {
                    await viewModel.LoadDataCommand.ExecuteAsync(null);
                }
            }
        }
    }
}