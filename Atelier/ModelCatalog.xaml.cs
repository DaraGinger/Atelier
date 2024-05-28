namespace Atelier
{
    using Atelier.Logic.Models;
    using System.Windows;
    using System.Windows.Media;
    using Atelier.Logic.Repositories.Interface;
    using Atelier.Logic.Repositories;

    /// <summary>
    /// Interaction logic for ModelCatalog.xaml
    /// </summary>
    public partial class ModelCatalog : Window
    {
        private readonly IFabricRepository fabricRepository;

        private readonly IFurnitureRepository furnitureRepository;

        private readonly IModelRepository modelRepository;

        public ModelCatalog()
        {
            InitializeComponent();
            fabricRepository = new FabricRepository();
            furnitureRepository = new FurnitureRepository();
            modelRepository = new ModelRepository();
            CountMaxId();
        }

        private int MaxId { get; set; }

        private void CountMaxId()
        {
            MaxId = modelRepository.GetMaxModelId();
        }

        public Model GetModel(int id)
        {
            Model model = new Model();

            if (id <= MaxId && id > 0)
            {
                model = modelRepository.GetModelById(id);
      
                Furniture furniture = furnitureRepository.GetFurnitureById(model.FurnitureId);

                Fabric fabric = fabricRepository.GetFabricById(model.FabricId);

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
                ClotheNameTextBlock.Text = model.FabricName;
                WasteClothLabel.Content = model.WasteFabric;
                FurnitureNameTextBlock.Text = model.FurnitureName;
                PriceLabel.Content = model.Price + " грн";
                ClothImage.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(model.ImageSrc);
                NumberFurnitureLabel.Content = model.NumberFurniture;
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
