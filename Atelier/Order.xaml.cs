using Atelier.Logic;
using Atelier.Logic.Models;
using Microsoft.VisualBasic;
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
            Client client = new Client()
            {
                LastName = LastNameTextBox.Text,
                Name = NameTextBox.Text,
                Surname = SurnameTextBox.Text,
                PhoneNumber = PhoneNumberTextBox.Text,
                Email = EmailTextBox.Text,
                Country = CountryTextBox.Text,
                City = CityTextBox.Text,
                Street = StreetTextBox.Text,
                HouseNumber = HouseTextBox.Text,
                FlatNumber = Convert.ToInt32(FlatNumberTextBox.Text)
            };

            var clientQuery = $"INSERT INTO [dbo].[Clients] ([LastName],[Name],[Surname],[Country],[City],[Street],[HouseNumber],[FlatNumber],[PhoneNumber],[Email]) OUTPUT inserted.[ClientId] VALUES ('{client.LastName}','{client.Name}','{client.Surname}','{client.Country}','{client.City}','{client.Street}','{client.HouseNumber}',{client.FlatNumber},'{client.PhoneNumber}','{client.Email}')";
            var reader = dataContext.GetSingleRow(clientQuery);
            if (reader.Read())
            {
                client.ClientID = Convert.ToInt32(reader["ClientId"]);
            }
            reader.Close();

            Product product = new Product
            {
                Clothd = Convert.ToInt32(ClothesComboBox.Text.Substring(0, 1)),
                FurnitureId = Convert.ToInt32(FurnitureComboBox.Text.Substring(0, 1)),
                ModelId = Convert.ToInt32(ModelComboBox.Text.Substring(0, 1)),
                ProductCount = Convert.ToInt32(CountTextBox.Text)
            };

            product.Price = CalculatePrice(product.Clothd, product.FurnitureId, product.ModelId, product.ProductCount);

            var productQuery = $"INSERT INTO [dbo].[Products]([ModelId],[ClothId],[FurnitureId],[Кількість_виробів],[Вартість]) OUTPUT inserted.[ProductId] VALUES({product.ModelId}, {product.Clothd},{product.FurnitureId},{product.ProductCount},{product.Price})";
            reader = dataContext.GetSingleRow(productQuery);
            if (reader.Read())
            {
                product.ProductId = Convert.ToInt32(reader["ProductId"]);
            }
            reader.Close();

            int workerId = Convert.ToInt32(WorkerComboBox.Text.Substring(0,1));
            DateTime dateTime = DateTime.Now;
            string date = dateTime.ToString("yyyy-MM-dd");
            var clientOrderQuery = $"INSERT INTO [dbo].[ClientOrders] ([ClientId],[ProductId],[WorkerId],[Price],[DateReceivingOrder],[DateFitting],[ExecutionDate],[Payment]) VALUES ({client.ClientID},{product.ProductId},{workerId},{product.Price},'{date}',null,null,{0})";
            dataContext.ExecuteQuery(clientOrderQuery);

            PriceLabel.Content = $"{product.Price} грн";

            MessageBox.Show("Success!");

        }

        public double CalculatePrice(int clothId, int furnitureId, int modelId, int productCount)
        {
            var clothQuery = $"SELECT [Price] FROM [dbo].[Сlothes] WHERE [СlotheId]={clothId}";
            var reader = dataContext.GetSingleRow(clothQuery);
            double clothPrice = 0;

            if (reader.Read())
            {
                clothPrice = Convert.ToDouble(reader["Price"]);
            }

            reader.Close();

            var furnitureQuery = $"SELECT [Price] FROM [dbo].[Furnitures] WHERE [FurnitureId]={furnitureId}";
            reader = dataContext.GetSingleRow(furnitureQuery);
            double furniturePrice = 0;

            if (reader.Read())
            {
                furniturePrice = Convert.ToDouble(reader["Price"]);
            }

            reader.Close();

            var modelQuery = $"SELECT [Price],[CostWork],[WasteFabric],[NumberFurniture] FROM [dbo].[Models] WHERE [FurnitureId]={furnitureId}";

            reader = dataContext.GetSingleRow(modelQuery);
            double modelPrice = 0;
            double costWork = 0;
            double wasteFabric = 0;
            double numberFurniture = 0;

            if (reader.Read())
            {
                modelPrice = Convert.ToDouble(reader["Price"]);
                costWork = Convert.ToDouble(reader["CostWork"]);
                wasteFabric = Convert.ToDouble(reader["WasteFabric"]);
                numberFurniture = Convert.ToDouble(reader["NumberFurniture"]);
            }

            reader.Close();

            double priceOrder = (clothPrice * wasteFabric + furniturePrice * numberFurniture + modelPrice + costWork) * productCount;

            return priceOrder;
        }
    }
}
