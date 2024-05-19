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
    /// Interaction logic for FurnitureCatalog.xaml
    /// </summary>
    public partial class FurnitureCatalog : Window
    {
        public FurnitureCatalog()
        {
            InitializeComponent();
            dataContext = new Database();
            CountMaxId();
        }

        private readonly Database dataContext;

        private int MaxId { get; set; }

        private void CountMaxId()
        {
            string query = "SELECT MAX ([FurnitureId]) FROM [dbo].[Furnitures]";
            var result = dataContext.GetSingleRow(query);
            if (result.Read())
            {
                MaxId = Convert.ToInt32(result[0]);
            }
        }

        private Furniture GetFurniture(int id)
        {
            Furniture cloth = new Furniture();

            if (id <= MaxId && id > 0)
            {
                string query = $"SELECT [FurnitureId],[Name],[Material],[Amount],[Price],[ImageSrc] FROM [Atelier].[dbo].[Furnitures] WHERE [FurnitureId] = {id}";
                var result = dataContext.GetSingleRow(query);
                cloth = Furniture.ToModel(result);
                result.Close();
            }

            return cloth;
        }

        public void FillForm(Furniture furniture)
        {
            if (furniture.FurnitureId > 0)
            {
                NameTextBlock.Text = furniture.Name;
                ClothIdLabel.Content = furniture.FurnitureId;
                AmountLabel.Content = furniture.Amount + " шт";
                MaterialLabel.Content = furniture.Material;
                PriceLabel.Content = furniture.Price + " грн/шт";
                ClothImage.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(furniture.ImageSrc);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(ClothIdLabel.Content) + 1;

            var furniture = GetFurniture(id);

            FillForm(furniture);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(ClothIdLabel.Content) - 1;

            var furniture = GetFurniture(id);

            FillForm(furniture);
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CatalogMenu catalogMenu = new CatalogMenu();
            catalogMenu.Show();
        }
    }
}
