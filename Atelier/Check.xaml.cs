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
            var order = _orderRepository.GetOrderById(orderId);

            var product = _orderRepository.GetProductById(order.ProductId);

            var model = _modelRepository.GetModelById(product.ModelId);

            var fabric = _fabricRepository.GetFabricById(product.FabricId);

            var furniture = _furnitureRepository.GetFurnitureById(product.FurnitureId);

            ModelTextBlock.Text = model.Name;
            ModelPriceLabel.Content = $"{model.Price} * {product.NumberProducts} = {model.Price*product.NumberProducts}";

            FabricTextBlock.Text = fabric.Name;
            FabricPriceLabel.Content = $"{fabric.Price} * {model.WasteFabric} * {product.NumberProducts} = {fabric.Price* model.WasteFabric* product.NumberProducts}";

            FurnitureTextBlock.Text = furniture.Name;
            FurniturePriceLabel.Content = $"{furniture.Price} * {model.NumberFurniture} * {product.NumberProducts} = {furniture.Price* model.NumberFurniture* product.NumberProducts}";

            WorkTextBlock.Text = "Cost of work";
            WorkPriceLabel.Content = $"{model.CostOfWork} * {product.NumberProducts}";

            PriceLabel.Content = $"{product.Price}";
        }
    }
}
