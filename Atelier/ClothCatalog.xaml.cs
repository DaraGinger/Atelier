using Atelier.Logic;
using Atelier.Logic.Entities;
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

namespace Atelier
{
    /// <summary>
    /// Interaction logic for ClothCatalog.xaml
    /// </summary>
    public partial class ClothCatalog : Window
    {
        public ClothCatalog()
        {
            InitializeComponent();
            dataContext = new DataContext();
            CountMaxId();
        }

        private readonly DataContext dataContext;
        
        private int MaxId {  get; set; }

        private void CountMaxId()
        {
            string query = "SELECT MAX ([СlotheId]) FROM [dbo].[Сlothes]";
            var result = dataContext.GetSingleRow(query);
            if (result.Read())
            {
                MaxId = Convert.ToInt32(result[0]);
            }
        }

        private Cloth GetCloth(int id)
        {
            Cloth cloth = new Cloth();

            if (id <= MaxId && id > 0)
            {
                string query = $"SELECT [СlotheId],[Name],[Width],[Price],[Amount],[ImageSrc] FROM [dbo].[Сlothes] WHERE [СlotheId] = {id}";
                var result = dataContext.GetSingleRow(query);
                cloth = Cloth.FromDataReader(result);
                result.Close();
            }

            return cloth;
        }

        public void FillForm(Cloth cloth)
        {
            if (cloth.ClothId > 0)
            {
                NameTextBlock.Text = cloth.Name;
                ClothIdLabel.Content = cloth.ClothId;
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
    }
}
