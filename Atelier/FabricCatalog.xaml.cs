namespace Atelier
{
    using Atelier.Logic.Models;
    using Atelier.Logic.Repositories;
    using Atelier.Logic.Repositories.Interface;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for ClothCatalog.xaml
    /// </summary>
    public partial class ClothCatalog : Window
    {
        public ClothCatalog()
        {
            InitializeComponent();
            fabricRepository = new FabricRepository();
            CountMaxId();
        }

        private readonly IFabricRepository fabricRepository;
        
        private int MaxId {  get; set; }

        private void CountMaxId()
        {
            MaxId = fabricRepository.GetMaxFabricId();
        }

        public Fabric GetFabric(int id)
        {
            Fabric fabric = new Fabric();

            if (id <= MaxId && id > 0)
            {
                fabric = fabricRepository.GetFabricById(id);
            }

            return fabric;
        }
        
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(ClothIdLabel.Content) + 1;

            var fabric = GetFabric(id);

            FillForm(fabric);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(ClothIdLabel.Content) - 1;

            var fabric = GetFabric(id);

            FillForm(fabric);
        }

        public void FillForm(Fabric fabric)
        {
            if (fabric.FabricId > 0)
            {
                NameTextBlock.Text = fabric.Name;
                ClothIdLabel.Content = fabric.FabricId;
                AmountLabel.Content = fabric.Amount+" м";
                WidthLabel.Content = fabric.Width+" м";
                PriceLabel.Content = fabric.Price+" грн/м";
                ClothImage.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(fabric.ImageSrc);
            }
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CatalogMenu catalogMenu = new CatalogMenu();
            catalogMenu.Show();
        }
    }
}
