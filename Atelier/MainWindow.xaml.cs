using Atelier.Logic;
using Atelier.Logic.Entities;
using Atelier.Logic.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT [СlotheId],[Name],[Width],[Price],[Amount],[ImageSrc] FROM [dbo].[Сlothes] WHERE [СlotheId] = 1";
            var result = dataContext.GetSingleRow(query);
            Fabric cloth = Fabric.ToModel(result);
            result.Close();

            string query2 = "SELECT [СlotheId],[Name],[Width],[Price],[Amount],[ImageSrc] FROM [dbo].[Сlothes]";
            var result2 = dataContext.GetListDataQuery(query2);
            List<Fabric> cloths = Fabric.ToModelList(result2);
            result.Close();
        }

        private void CatalogButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CatalogMenu catalogMenu = new CatalogMenu();
            catalogMenu.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string query = "SELECT [СlotheId],[Name],[Width],[Price],[Amount],[ImageSrc] FROM [dbo].[Сlothes] WHERE [СlotheId] = 1";
            var result = dataContext.GetSingleRow(query);
            Fabric cloth = Fabric.ToModel(result);
            result.Close();
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
    }
}