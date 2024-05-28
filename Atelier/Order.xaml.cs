namespace Atelier
{
    using Atelier.Logic;
    using Atelier.Logic.Models;
    using Atelier.Logic.Repositories;
    using Atelier.Logic.Repositories.Interface;
    using System.Windows;

    public partial class Order : Window
    {

        private readonly IFabricRepository fabricRepository;

        private readonly IFurnitureRepository furnitureRepository;

        private readonly IModelRepository modelRepository;

        private readonly IWorkerRepository workerRepository;

        private readonly IOrderRepository orderRepository;

        public Order()
        {
            InitializeComponent();

            fabricRepository = new FabricRepository();
            furnitureRepository = new FurnitureRepository();
            modelRepository = new ModelRepository();
            workerRepository = new WorkerRepository();
            orderRepository = new OrderRepository();

            FillComboBoxes();
        }

        private int ClientOrderId { get; set; }

        private void FillComboBoxes()
        {
            var fabricNames = fabricRepository.GetFabricNames();
            foreach (var fabricName in fabricNames)
            {
                ClothesComboBox.Items.Add(fabricName);
            }

            var furnitureNames = furnitureRepository.GetFurnitureNames();
            foreach (var furnName in furnitureNames)
            {
                FurnitureComboBox.Items.Add(furnName);
            }

            var modelNames = modelRepository.GetModelNames();
            foreach (var modelName in modelNames)
            {
                ModelComboBox.Items.Add(modelName);
            }

            var workerNames = workerRepository.GetWorkerNames();
            foreach (var workerName in workerNames)
            {
                WorkerComboBox.Items.Add(workerName);
            }
        }

        private void OrderButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckFields())
            {
                return;
            }

            Product product = new Product
            {
                FabricId = Convert.ToInt32(ClothesComboBox.Text.Substring(0, 2).Trim()),
                FurnitureId = FurnitureComboBox.Text == "" ? 0 : Convert.ToInt32(FurnitureComboBox.Text.Substring(0, 2).Trim()),
                ModelId = Convert.ToInt32(ModelComboBox.Text.Substring(0, 2).Trim()),
                NumberProducts = Convert.ToInt32(CountTextBox.Text)
            };

            Model model = modelRepository.GetModelById(product.ModelId);

            Fabric fabric = fabricRepository.GetFabricById(product.FabricId);

            Furniture furniture = furnitureRepository.GetFurnitureById(product.FurnitureId);

            product.Price = (fabric.Price * model.WasteFabric + furniture.Price * model.NumberFurniture + model.Price + model.CostOfWork) * product.NumberProducts;

            int workerId = WorkerComboBox.Text == "" ? 0 : Convert.ToInt32(WorkerComboBox.Text.Substring(0, 1));

            ClientOrderId = orderRepository.AddOrder(product, LastNameTextBox.Text, NameTextBox.Text, SurnameTextBox.Text, workerId);

            UpdateFabric(fabric, model.WasteFabric, workerId);

            UpdateFurniture(furniture, model.NumberFurniture, workerId);

            workerRepository.UpdateWorkerNumberOrders(workerId);

            PriceLabel.Content = $"{product.Price} грн";

            MessageBox.Show("Success!");
        }

        private void UpdateFabric(Fabric fabric, double wasteFabric, int workerId)
        {
            if (fabric.Amount == 0 || fabric.Amount < wasteFabric)
            {
                int fabricCount = 20;

                SupplierOrder supplierOrder = new SupplierOrder
                {
                    SupplierId = fabric.SupplierId,
                    ProductName = fabric.Name,
                    ProductType = ProductType.Fabric,
                    Amount = fabricCount,
                    Price = fabricCount * fabric.Price,
                    WorkerId = workerId,
                    OrderDate = DateTime.Now
                };

                orderRepository.AddSupplierOrder(supplierOrder);
            }
            else
            {
                double rest = fabric.Amount - wasteFabric;
                fabricRepository.UpdateFabricAmount(rest, fabric.FabricId);
            }
        }

        private void UpdateFurniture(Furniture furniture, int numberFurniture, int workerId)
        {
            if (furniture.Amount == 0 || furniture.Amount < numberFurniture)
            {
                int fabricCount = 30;

                SupplierOrder supplierOrder = new SupplierOrder
                {
                    SupplierId = furniture.SupplierId,
                    ProductName = furniture.Name,
                    ProductType = ProductType.Furniture,
                    Amount = fabricCount,
                    Price = fabricCount * furniture.Price,
                    WorkerId = workerId,
                    OrderDate = DateTime.Now
                };

                orderRepository.AddSupplierOrder(supplierOrder);
            }
            else
            {
                double rest = furniture.Amount - numberFurniture;
                furnitureRepository.UpdateFurnitureAmount(rest, furniture.FurnitureId);
            }
        }

        private bool CheckFields()
        {
            if (LastNameTextBox.Text == "" || NameTextBox.Text == "" || SurnameTextBox.Text == "")
            {
                MessageBox.Show("Введіть повне ім'я!");
                return false;
            }
            
            if (ClothesComboBox.Text == "")
            {
                MessageBox.Show("Оберіть тканину!");
                return false;
            }

            if (ModelComboBox.Text == "")
            {
                MessageBox.Show("Оберіть модель!");
                return false;
            }

            if (WorkerComboBox.Text == "")
            {
                MessageBox.Show("Оберіть робітника!");
                return false;
            }

            if (CountTextBox.Text == "")
            {
                MessageBox.Show("Введіть кількість!");
                return false;
            }

            return true;
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientOrderId == 0)
            {
                MessageBox.Show("Ви ще не зробили замовлення!");
                return;
            }

            Check check = new Check();
            check.FillCheck(ClientOrderId);
            check.ShowDialog();
        }
    }
}
