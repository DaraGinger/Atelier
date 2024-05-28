namespace Atelier
{
    using Atelier.Logic;
    using Atelier.Logic.Models;
    using Atelier.Logic.Repositories;
    using Atelier.Logic.Repositories.Interface;
    using System.Collections.ObjectModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for Debtors.xaml
    /// </summary>
    public partial class DebtorsForm : Window
    {
        public ObservableCollection<Debtor> Debtors { get; set; }
        private readonly IOrderRepository orderRepository;

        public DebtorsForm()
        {
            InitializeComponent();
            dataContext = new Database();
            Debtors = new ObservableCollection<Debtor>();
            orderRepository = new OrderRepository();
            FillDataGrid();
        }

        Database dataContext;

        private void FillDataGrid()
        {
            var clientOrders = orderRepository.GetDebtors();

            foreach (var clientOrder in  clientOrders)
            {
                Debtor debtor = Debtor.ToModel(clientOrder);
                Debtors.Add(debtor);
            }

            DataContext = this;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var debtor = DebtorsDataGrid.SelectedItem as Debtor;

            if (debtor != null)
            {
                orderRepository.UpdatePayment(debtor.ClientOrderId, 1);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var debtor = DebtorsDataGrid.SelectedItem as Debtor;

            if (debtor != null)
            {
                orderRepository.UpdatePayment(debtor.ClientOrderId, 0);
            }
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
