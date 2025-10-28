// /Views/OrderView.xaml.cs
using Pizza_Programm.ViewModels;
using System.Windows;
using System.Windows.Controls;

// CORRECTED: Namespace added
namespace Pizza_Programm.Views
{
    public partial class OrderView : UserControl
    {
        public OrderView()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is OrderViewModel viewModel)
            {
                if (viewModel.LoadMenuCommand.CanExecute(null))
                {
                    await viewModel.LoadMenuCommand.ExecuteAsync(null);
                }
            }
        }
    }
}