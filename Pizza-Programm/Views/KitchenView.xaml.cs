// /Views/KitchenView.xaml.cs
using Pizza_Programm.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Pizza_Programm.Views
{
    public partial class KitchenView : UserControl
    {
        public KitchenView()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is KitchenViewModel viewModel)
            {
                if (viewModel.LoadPendingOrdersCommand.CanExecute(null))
                {
                    await viewModel.LoadPendingOrdersCommand.ExecuteAsync(null);
                }
            }
        }
    }
}