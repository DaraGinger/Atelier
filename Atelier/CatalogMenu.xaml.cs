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
            dataContext = new DataContext();
        }

        DataContext dataContext;

        private void ClothCatalogButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ClothCatalog clothCatalog = new ClothCatalog();
            clothCatalog.Show();

            string query = "SELECT [СlotheId],[Name],[Width],[Price],[Amount],[ImageSrc] FROM [dbo].[Сlothes] WHERE [СlotheId] = 1";
            var result = dataContext.GetSingleRow(query);
            Cloth cloth = Cloth.ToModel(result);
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
    }
}
