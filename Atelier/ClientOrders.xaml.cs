namespace Atelier
{
    using Atelier.Logic.Models;
    using Atelier.Logic.Repositories;
    using Atelier.Logic.Repositories.Interface;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    /// <summary>
    /// Interaction logic for ListOrders.xaml
    /// </summary>
    public partial class ClientOrders : Window
    {
        public ObservableCollection<ClientOrderHelper> ClientOrdersList { get; set; }

        private readonly IOrderRepository orderRepository;

        public ClientOrders()
        {
            InitializeComponent();
            ClientOrdersList = new ObservableCollection<ClientOrderHelper>();
            orderRepository = new OrderRepository();
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            var orders = orderRepository.GetOrders();

            foreach (var order in orders)
            {
                ClientOrdersList.Add(order);
            }

            DataContext = this;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var clientOrder = OrdersDataGrid.SelectedItem as ClientOrderHelper;

            if (clientOrder != null)
            {
                orderRepository.UpdatePayment(clientOrder.ClientOrderId, 1);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var clientOrder = OrdersDataGrid.SelectedItem as ClientOrderHelper;

            if (clientOrder != null)
            {
                orderRepository.UpdatePayment(clientOrder.ClientOrderId, 0);
            }
        }

        private void DateFitting_TextChanged(object sender, EventArgs e)
        {
            var clientOrder = OrdersDataGrid.SelectedItem as ClientOrderHelper;

            if (!DateTime.TryParse(clientOrder.DateFitting, out DateTime value))
            {
                MessageBox.Show("Введіть дату примірки в належному форматі!");
                return;
            }

            DateTime date = Convert.ToDateTime(clientOrder.DateFitting);

            orderRepository.UpdateFittingDate(clientOrder.ClientOrderId, date.ToString("yyyy-MM-dd"));
        }

        private void ExecutionDate_TextChanged(object sender, EventArgs e)
        {
            var clientOrder = OrdersDataGrid.SelectedItem as ClientOrderHelper;

            if (!DateTime.TryParse(clientOrder.ExecutionDate, out DateTime value))
            {
                MessageBox.Show("Введіть дату виконання в належному форматі!");
                return;
            }

            DateTime date = Convert.ToDateTime(clientOrder.ExecutionDate);

            orderRepository.UpdateExecutionDate(clientOrder.ClientOrderId, date.ToString("yyyy-MM-dd"));
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var clientOrder = OrdersDataGrid.SelectedItem as ClientOrderHelper;
            
            Check check = new Check();
            check.FillCheck(clientOrder.ClientOrderId);
            check.ShowDialog();
        }
    }
}
