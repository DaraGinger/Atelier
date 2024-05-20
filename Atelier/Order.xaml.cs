using Atelier.Logic;
using Atelier.Logic.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Atelier
{
    public partial class Order : Window
    {
        private List<ComboxModel> Clothes;
        private readonly Database dataContext;

        public Order()
        {
            InitializeComponent();
            Clothes = new List<ComboxModel>();
            dataContext = new Database();
            FillComboBoxes();
        }

        private void FillComboBoxes()
        {
            var clothQuery = $"SELECT [СlotheId],[Name] FROM [dbo].[Сlothes]";
            var result = dataContext.GetListDataQuery(clothQuery);
            while (result.Read())
            {
                int id = Convert.ToInt32(result["СlotheId"]);
                string name = Convert.ToString(result["Name"]);
               
                ClothesComboBox.Items.Add(id+" "+name);
            }
            result.Close();

            var furnitureQuery = $"SELECT [FurnitureId],[Name] FROM [dbo].[Furnitures] WHERE [FurnitureId]>0";

            result = dataContext.GetListDataQuery(furnitureQuery);
            while (result.Read())
            {
                int id = Convert.ToInt32(result["FurnitureId"]);
                string name = Convert.ToString(result["Name"]);


                FurnitureComboBox.Items.Add(id + " " + name);
            }
            result.Close();

            var modelQuery = $"SELECT [ModelId],[Name] FROM [dbo].[Models]";

            result = dataContext.GetListDataQuery(modelQuery);
            while (result.Read())
            {
                int id = Convert.ToInt32(result["ModelId"]);
                string name = Convert.ToString(result["Name"]);


                ModelComboBox.Items.Add(id + " " + name);
            }

            var workerQuery = $"SELECT [WorkerId],[Name],[LastName],[Surname] FROM [dbo].[Workers] WHERE [NumberOrders] <= 3";

            result = dataContext.GetListDataQuery(workerQuery);
            while (result.Read())
            {
                int id = Convert.ToInt32(result["WorkerId"]);
                string lastName = Convert.ToString(result["LastName"]);
                string name = Convert.ToString(result["Name"]);
                string surname = Convert.ToString(result["Surname"]);


                WorkerComboBox.Items.Add($"{id} {lastName} {name} {surname}");
            }
        }

        private void OrderButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
