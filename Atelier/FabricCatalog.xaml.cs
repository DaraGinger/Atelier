namespace Atelier
{
    using Atelier.Logic;
    using Atelier.Logic.Entities;
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
            dataContext = new Database();
            CountMaxId();
        }

        private readonly Database dataContext;
        
        private int MaxId {  get; set; }

        private void CountMaxId()
        {
            string query = "SELECT MAX ([FabricId]) FROM [dbo].[Fabrics]";
            var result = dataContext.GetSingleRow(query);
            if (result.Read())
            {
                MaxId = Convert.ToInt32(result[0]);
            }
        }

        private Fabric GetCloth(int id)
        {
            Fabric cloth = new Fabric();

            if (id <= MaxId && id > 0)
            {
                string query = $"SELECT [FabricId],[Name],[Width],[Price],[Amount],[ImageSrc] FROM [dbo].[Fabrics] WHERE [FabricId] = {id}";
                var result = dataContext.GetSingleRow(query);
                cloth = Fabric.ToModel(result);
                result.Close();
            }

            return cloth;
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

            var cloth = GetCloth(id);

            FillForm(cloth);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(ClothIdLabel.Content) - 1;

            var cloth = GetCloth(id);

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
