namespace Atelier
{
    using Atelier.Logic;
    using Atelier.Logic.Models;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for Debtors.xaml
    /// </summary>
    public partial class DebtorsForm : Window
    {
        public ObservableCollection<Debtor> Debtors { get; set; }

        public DebtorsForm()
        {
            InitializeComponent();
            dataContext = new Database();
            Debtors = new ObservableCollection<Debtor>();
            FillDataGrid();
        }

        Database dataContext;

        private void FillDataGrid()
        {
            var orderQuery = "SELECT [ClientOrderId],[Payment],[ClientId],[Price] FROM [Atelier].[dbo].[ClientOrders] WHERE [Payment] = 0";

            var clientOrdersReader = dataContext.GetListDataQuery(orderQuery);
            List<ClientOrder> clientOrders = ClientOrder.ToDebtersList(clientOrdersReader);
            clientOrdersReader.Close();

            foreach(var clientOrder in  clientOrders)
            {
                var clientQuery = $"SELECT [Name],[Surname],[LastName] FROM [Atelier].[dbo].[Clients] WHERE [ClientId]={clientOrder.ClientId}";
                var clientOrderReader = dataContext.GetSingleRow(clientQuery);

                if (clientOrderReader.Read())
                {
                    clientOrder.Client.Name = Convert.ToString(clientOrderReader["Name"]);
                    clientOrder.Client.Surname = Convert.ToString(clientOrderReader["Surname"]);
                    clientOrder.Client.LastName = Convert.ToString(clientOrderReader["LastName"]);
                }

                clientOrderReader.Close();

                Debtor debtor = Debtor.ToModel(clientOrder);
                Debtors.Add(debtor);
            }

            DataContext = this;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var debtor = DebtorsDataGrid.SelectedItem as Debtor;

            var orderQuery = $"UPDATE [Atelier].[dbo].[ClientOrders] SET [Payment]=1 WHERE [ClientOrderId] = {debtor.ClientOrderId}";

            dataContext.ExecuteQuery(orderQuery);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var debtor = DebtorsDataGrid.SelectedItem as Debtor;

            var orderQuery = $"UPDATE [Atelier].[dbo].[ClientOrders] SET [Payment]=0 WHERE [ClientOrderId] = {debtor.ClientOrderId}";

            dataContext.ExecuteQuery(orderQuery);
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
