using System.Windows;
using System.Windows.Media;
using Atelier.Logic.Models;
using Atelier.Logic.Repositories.Interface;
using Atelier.Logic.Repositories;

namespace Atelier
{
    /// <summary>
    /// Interaction logic for FurnitureCatalog.xaml
    /// </summary>
    public partial class FurnitureCatalog : Window
    {
        private readonly IFurnitureRepository furnitureRepository;
        public FurnitureCatalog()
        {
            InitializeComponent();
            furnitureRepository = new FurnitureRepository();
            CountMaxId();
        }

        private int MaxId { get; set; }

        private void CountMaxId()
        {
            MaxId = furnitureRepository.GetMaxFurnitureId();
        }

        public Furniture GetFurniture(int id)
        {
            Furniture furniture = new Furniture();

            if (id <= MaxId && id > 0)
            {
                furniture = furnitureRepository.GetFurnitureById(id);
            }

            return furniture;
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
