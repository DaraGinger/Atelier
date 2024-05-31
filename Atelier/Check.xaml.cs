using Atelier.Logic.Repositories;
using Atelier.Logic.Repositories.Interface;
using System.Windows;

namespace Atelier
{
    /// <summary>
    /// Interaction logic for Check.xaml
    /// </summary>
    public partial class Check : Window
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IModelRepository _modelRepository;

        private readonly IFabricRepository _fabricRepository;

        private readonly IFurnitureRepository _furnitureRepository;
        public Check()
        {
            InitializeComponent();
            _orderRepository = new OrderRepository();
            _modelRepository = new ModelRepository();
            _fabricRepository = new FabricRepository();
            _furnitureRepository = new FurnitureRepository();
        }

        public void FillCheck(int orderId)
        {
            var check = _orderRepository.GetCheck(orderId);

            ModelTextBlock.Text = check.ModelName;
            ModelPriceLabel.Content = $"{check.ModelPrice} * {check.NumberProducts} = {check.SumModel}";

            FabricTextBlock.Text = check.FabricName;
            FabricPriceLabel.Content = $"{check.FabricPrice} * {check.WasteFabric} * {check.NumberProducts} = {check.SumFabric}";

            FurnitureTextBlock.Text = check.FurnitureName;
            FurniturePriceLabel.Content = $"{check.FurniturePrice} * {check.NumberFurniture} * {check.NumberProducts} = {check.SumFurniture}";

            WorkTextBlock.Text = "Ціна роботи";
            WorkPriceLabel.Content = $"{check.CostOfWork} * {check.NumberProducts}";

            PriceLabel.Content = $"{check.Price}";
        }
    }
}
