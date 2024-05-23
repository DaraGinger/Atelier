namespace Atelier
{
    using Atelier.Logic.Models;
    using System.Collections.ObjectModel;
    using System.Windows;
    /// <summary>
    /// Interaction logic for ListOrders.xaml
    /// </summary>
    public partial class ListOrders : Window
    {
        public ObservableCollection<ClientOrderHelper> Orders { get; set; }

        public ListOrders()
        {
            InitializeComponent();
            Orders = new ObservableCollection<ClientOrderHelper>();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
