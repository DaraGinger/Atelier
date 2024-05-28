using Atelier.Logic;
using System.Windows;

namespace Atelier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Database dataContext = new Database();

        private void CatalogButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CatalogMenu catalogMenu = new CatalogMenu();
            catalogMenu.Show();
        }

        private void DebtorsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            DebtorsForm debtors = new DebtorsForm();
            debtors.Show();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Order order = new Order();
            order.Show();
        }

        private void ClientOrderButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ClientOrders clientOrders = new ClientOrders();
            clientOrders.Show();
        }

        private void SupplierOrderButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            //SupplierOrderList supplierOrderList = new SupplierOrderList();
            //supplierOrderList.Show();
        }
    }
}