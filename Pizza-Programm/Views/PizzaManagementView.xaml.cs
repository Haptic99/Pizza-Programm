// /Views/PizzaManagementView.xaml.cs
using Pizza_Programm.ViewModels;
using System.Windows;
using System.Windows.Controls;

// CORRECTED: Namespace added
namespace Pizza_Programm.Views
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