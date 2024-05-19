namespace Atelier
{
    using Atelier.Logic.Models;
    using Atelier.Logic;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for ModelCatalog.xaml
    /// </summary>
    public partial class ModelCatalog : Window
    {
        public ModelCatalog()
        {
            InitializeComponent();
            dataContext = new Database();
            CountMaxId();
        }

        private readonly Database dataContext;

        private int MaxId { get; set; }

        private void CountMaxId()
        {
            string query = "SELECT MAX ([ModelId]) FROM [dbo].[Models]";
            var result = dataContext.GetSingleRow(query);
            if (result.Read())
            {
                MaxId = Convert.ToInt32(result[0]);
            }
        }

        private Model GetModel(int id)
        {
            Model model = new Model();

            if (id <= MaxId && id > 0)
            {
                string query = $"SELECT [ModelId],[Name],[ClothId],[FurnitureId],[Price],[WasteFabric],[NumberFurniture],[CostWork],[ImageSrc] FROM [Atelier].[dbo].[Models] WHERE [ModelId] = {id}";
                var result = dataContext.GetSingleRow(query);
                model = Model.ToModel(result);
                result.Close();

                string furnitureQuery = $"SELECT [Name] FROM [Atelier].[dbo].[Furnitures] WHERE [FurnitureId] = {model.FurnitureId}";
                var furniture = dataContext.GetSingleRow(furnitureQuery);

                if (furniture.Read())
                {
                    model.FurnitureName = Convert.ToString(furniture["Name"]);
                }

                string clothQuery = $"SELECT [Name] FROM [Atelier].[dbo].[Сlothes] WHERE [СlotheId] = {model.ClothId}";
                var cloth = dataContext.GetSingleRow(clothQuery);

                if (cloth.Read())
                {
                    model.ClothName = Convert.ToString(cloth["Name"]);
                }

                FillForm(model);
            }

            return model;
        }

        public void FillForm(Model model)
        {
            if (model.ModelId > 0)
            {
                NameTextBlock.Text = model.Name;
                ModelIdLabel.Content = model.ModelId;
                ClotheNameTextBlock.Text = model.ClothName;
                WasteClothLabel.Content = model.WasteFabric;
                FurnitureNameTextBlock.Text = model.FurnitureName;
                PriceLabel.Content = model.Price + " грн/шт";
                ClothImage.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(model.ImageSrc);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(ModelIdLabel.Content) + 1;

            var furniture = GetModel(id);

            FillForm(furniture);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(ModelIdLabel.Content) - 1;

            var furniture = GetModel(id);

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
