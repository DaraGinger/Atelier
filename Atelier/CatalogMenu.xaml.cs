using Atelier.Logic;
using System.Windows;
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

        private void FabricCatalogButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ClothCatalog clothCatalog = new ClothCatalog();
            clothCatalog.Show();

            var fabric = clothCatalog.GetFabric(1);

            clothCatalog.FillForm(fabric);
        }

        private void FurnitureButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            FurnitureCatalog furnitureCatalog = new FurnitureCatalog();
            furnitureCatalog.Show();

            var furniture = furnitureCatalog.GetFurniture(1);

            furnitureCatalog.FillForm(furniture);
        }

        private void ModelCatalogButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ModelCatalog modelCatalog= new ModelCatalog();
            modelCatalog.Show();

            Model model = new Model();
            string query = $" SELECT [ModelId],[Name],[FabricId],[FurnitureId],[Price],[WasteFabric],[NumberFurniture],[CostWork],[ImageSrc] FROM [Atelier].[dbo].[Models] WHERE [ModelId] = 1";
            model = Model.ToModel(query, dataContext);

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
