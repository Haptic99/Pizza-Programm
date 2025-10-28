// /Views/OrderView.xaml.cs
using DeinPizzaShopProjekt.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DeinPizzaShopProjekt.Views
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