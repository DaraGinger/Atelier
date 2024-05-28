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

        public void FillForm(Fabric cloth)
        {
            if (cloth.FabricId > 0)
            {
                NameTextBlock.Text = cloth.Name;
                ClothIdLabel.Content = cloth.FabricId;
                AmountLabel.Content = cloth.Amount+" м";
                WidthLabel.Content = cloth.Width+" м";
                PriceLabel.Content = cloth.Price+" грн/м";
                ClothImage.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(cloth.ImageSrc);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(ClothIdLabel.Content) + 1;

            var cloth = GetFabric(id);

            FillForm(cloth);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(ClothIdLabel.Content) - 1;

            var cloth = GetFabric(id);

            FillForm(cloth);
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CatalogMenu catalogMenu = new CatalogMenu();
            catalogMenu.Show();
        }
    }
}
