using Atelier.Logic.Entities;
using Atelier.Logic;
using System;
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
using Atelier.Logic.Models;

namespace Atelier
{
    /// <summary>
    /// Interaction logic for CatalogMenu.xaml
    /// </summary>
    public partial class CatalogMenu : Window
    {
        public CatalogMenu()
        {
            InitializeComponent();
            dataContext = new Database();
        }

        Database dataContext;

        private void ClothCatalogButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ClothCatalog clothCatalog = new ClothCatalog();
            clothCatalog.Show();

            string query = "SELECT [FabricId],[Name],[Width],[Price],[Amount],[ImageSrc] FROM [dbo].[Fabrics] WHERE [FabricId] = 1";
            var result = dataContext.GetSingleRow(query);
            Fabric cloth = Fabric.ToModel(result);
            result.Close();

            clothCatalog.FillForm(cloth);
        }

        private void FurnitureButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            FurnitureCatalog furnitureCatalog = new FurnitureCatalog();
            furnitureCatalog.Show();

            string query = $"SELECT [FurnitureId],[Name],[Material],[Amount],[Price],[ImageSrc] FROM [Atelier].[dbo].[Furnitures] WHERE ([FurnitureId]) = 1";
            var result = dataContext.GetSingleRow(query);
            Furniture furniture = Furniture.ToModel(result);
            result.Close();

            furnitureCatalog.FillForm(furniture);
        }

        private void ModelCatalogButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ModelCatalog modelCatalog= new ModelCatalog();
            modelCatalog.Show();

            Model model = new Model();
            string query = $" SELECT [ModelId],[Name],[FabricId],[FurnitureId],[Price],[WasteFabric],[NumberFurniture],[CostWork],[ImageSrc] FROM [Atelier].[dbo].[Models] WHERE [ModelId] = 1";
            var result = dataContext.GetSingleRow(query);
            model = Model.ToModel(result);
            result.Close();

            string furnitureQuery = $"SELECT [Name] FROM [Atelier].[dbo].[Furnitures] WHERE [FurnitureId] = {model.FurnitureId}";
            var furniture = dataContext.GetSingleRow(furnitureQuery);

            if (furniture.Read())
            {
                model.FurnitureName = Convert.ToString(furniture["Name"]);
            }

            string clothQuery = $"SELECT [Name] FROM [Atelier].[dbo].[Fabrics] WHERE [FabricId] = {model.FabricId}";
            var cloth = dataContext.GetSingleRow(clothQuery);

            if (cloth.Read())
            {
                model.FabricName = Convert.ToString(cloth["Name"]);
            }

            modelCatalog.FillForm(model);
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
