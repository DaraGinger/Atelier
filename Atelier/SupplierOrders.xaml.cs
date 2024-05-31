using Atelier.Logic.Models;
using Atelier.Logic.Repositories.Interface;
using Atelier.Logic.Repositories;
using System.Collections.ObjectModel;
using System.Windows;
using Atelier.Logic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Atelier
{
    /// <summary>
    /// Interaction logic for SupplierOrders.xaml
    /// </summary>
    public partial class SupplierOrders : Window
    {
        private readonly IOrderRepository orderRepository;
        private readonly IFabricRepository fabricRepository;
        private readonly IFurnitureRepository furnitureRepository;

        public ObservableCollection<SupplierOrderHelper> SupplierOrdersList { get; set; }

        public SupplierOrders()
        {
            InitializeComponent();
            SupplierOrdersList = new ObservableCollection<SupplierOrderHelper>();
            orderRepository = new OrderRepository();
            fabricRepository = new FabricRepository();
            furnitureRepository = new FurnitureRepository();
            FillDataGrid();
        }
        private void FillDataGrid()
        {
            var orders = orderRepository.GetSupplierOrders();

            foreach (var order in orders)
            {
                SupplierOrdersList.Add(order);
            }

            DataContext = this;
        }

        private void CompletedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var order = OrdersDataGrid.SelectedItem as SupplierOrderHelper;

            if (order != null)
            {
                orderRepository.UpdateSupplierIsComplete(order.SupplierOrderId, 1);

                switch (order.ProductType)
                {
                    case ProductType.Fabric:
                        {
                            fabricRepository.UpdateFabricAmountBySupplierOrder(order.SupplierOrderId);
                            break;
                        }
                    case ProductType.Furniture:
                        {
                            furnitureRepository.UpdateFurnitureAmountBySupplierOrder(order.SupplierOrderId);
                            break;
                        }
                }
            }
        }

        private void PaidCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var order = OrdersDataGrid.SelectedItem as SupplierOrderHelper;

            if (order != null)
            {
                orderRepository.UpdateSupplierPayment(order.SupplierOrderId, 1);
            }
        }

        private void PaidCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var order = OrdersDataGrid.SelectedItem as SupplierOrderHelper;

            if (order != null)
            {
                orderRepository.UpdateSupplierPayment(order.SupplierOrderId, 0);
            }
        }


        private void ExecutionDate_TextChanged(object sender, EventArgs e)
        {
            var order = OrdersDataGrid.SelectedItem as SupplierOrderHelper;

            if (!DateTime.TryParse(order.ExecutionDate, out DateTime value))
            {
                MessageBox.Show("Введіть дату виконання в належному форматі!");
                return;
            }

            DateTime date = Convert.ToDateTime(order.ExecutionDate);
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }

}
