namespace Atelier
{
    using Atelier.Logic;
    using Atelier.Logic.Entities;
    using Atelier.Logic.Models;
    using System.Windows;

    public partial class Order : Window
    {
        private readonly Database dataContext;

        public Order()
        {
            InitializeComponent();
            dataContext = new Database();
            FillComboBoxes();
        }

        private void FillComboBoxes()
        {
            var fabricQuery = $"SELECT [FabricId],[Name] FROM [dbo].[Fabrics]";
            var result = dataContext.GetListDataQuery(fabricQuery);
            while (result.Read())
            {
                int id = Convert.ToInt32(result["FabricId"]);
                string name = Convert.ToString(result["Name"]);
               
                ClothesComboBox.Items.Add(id+" "+name);
            }
            result.Close();

            var furnitureQuery = $"SELECT [FurnitureId],[Name] FROM [dbo].[Furnitures] WHERE [FurnitureId]>0";

            result = dataContext.GetListDataQuery(furnitureQuery);
            while (result.Read())
            {
                int id = Convert.ToInt32(result["FurnitureId"]);
                string name = Convert.ToString(result["Name"]);


                FurnitureComboBox.Items.Add(id + " " + name);
            }
            result.Close();

            var modelQuery = $"SELECT [ModelId],[Name] FROM [dbo].[Models]";

            result = dataContext.GetListDataQuery(modelQuery);
            while (result.Read())
            {
                int id = Convert.ToInt32(result["ModelId"]);
                string name = Convert.ToString(result["Name"]);


                ModelComboBox.Items.Add(id + " " + name);
            }

            var workerQuery = $"SELECT [WorkerId],[Name],[LastName],[Surname] FROM [dbo].[Workers] WHERE [NumberOrders] <= 3 AND [WorkerId]>0";

            result = dataContext.GetListDataQuery(workerQuery);
            while (result.Read())
            {
                int id = Convert.ToInt32(result["WorkerId"]);
                string lastName = Convert.ToString(result["LastName"]);
                string name = Convert.ToString(result["Name"]);
                string surname = Convert.ToString(result["Surname"]);


                WorkerComboBox.Items.Add($"{id} {lastName} {name} {surname}");
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
            Product product = new Product
            {
                FabricId = Convert.ToInt32(ClothesComboBox.Text.Substring(0, 1)),
                FurnitureId = FurnitureComboBox.Text == "" ? 0 : Convert.ToInt32(FurnitureComboBox.Text.Substring(0, 1)),
                ModelId = Convert.ToInt32(ModelComboBox.Text.Substring(0, 1)),
                ProductCount = Convert.ToInt32(CountTextBox.Text)
            };

            int workerId = Convert.ToInt32(WorkerComboBox.Text.Substring(0, 1));
            product.Price = CalculatePrice(product.FabricId, product.FurnitureId, product.ModelId, product.ProductCount, workerId);

            var productQuery = $"INSERT INTO [dbo].[Products]([ModelId],[ClothId],[FurnitureId],[NumberProducts],[Price]) OUTPUT inserted.[ProductId] VALUES({product.ModelId}, {product.FabricId},{product.FurnitureId},{product.ProductCount},{product.Price})";
            var reader = dataContext.GetSingleRow(productQuery);
            if (reader.Read())
            {
                product.ProductId = Convert.ToInt32(reader["ProductId"]);
            }
            reader.Close();

            DateTime dateTime = DateTime.Now;
            string date = dateTime.ToString("yyyy-MM-dd");
            var clientOrderQuery = $"INSERT INTO [dbo].[ClientOrders] ([LastName],[Name],[Surname],[ProductId],[WorkerId],[Price],[DateReceivingOrder],[DateFitting],[ExecutionDate],[Payment]) VALUES ({LastNameTextBox.Text},{NameTextBox},{SurnameTextBox},{product.ProductId},{workerId},{product.Price},'{date}',null,null,{0})";
            dataContext.ExecuteQuery(clientOrderQuery);

            PriceLabel.Content = $"{product.Price} грн";

            MessageBox.Show("Success!");

        }

        private double CalculatePrice(int fabricId, int furnitureId, int modelId, int productCount, int workerId)
        {
            var modelQuery = $"SELECT [Price],[CostWork],[WasteFabric],[NumberFurniture] FROM [dbo].[Models] WHERE [FurnitureId]={modelId}";

            var reader = dataContext.GetSingleRow(modelQuery);

            Model model = new Model();

            if (reader.Read())
            {
                model.Price = Convert.ToDouble(reader["Price"]);
                model.CostOfWork = Convert.ToDouble(reader["CostWork"]);
                model.WasteFabric = Convert.ToDouble(reader["WasteFabric"]);
                model.NumberFurniture = Convert.ToInt32(reader["NumberFurniture"]);
            }

            reader.Close();

            var fabricQuery = $"SELECT [Price],[Name],[Amount],[SupplierId] FROM [dbo].[Fabrics] WHERE [FabricId]={fabricId}";

            reader = dataContext.GetSingleRow(fabricQuery);

            Fabric fabric = new Fabric();

            if (reader.Read())
            {
                fabric.FabricId = fabricId;
                fabric.Price = Convert.ToDouble(reader["Price"]);
                fabric.Name = Convert.ToString(reader["Name"]);
                fabric.Amount = Convert.ToDouble(reader["Amount"]);
                fabric.SupplierId = Convert.ToInt32(reader["SupplierId"]);
            }

            reader.Close();

            UpdateFabric(fabric, model.WasteFabric, workerId);

            var furnitureQuery = $"SELECT [Price],[Name],[SupplierId],[Amount] FROM [dbo].[Furnitures] WHERE [FurnitureId]={furnitureId}";

            reader = dataContext.GetSingleRow(furnitureQuery);

            Furniture furniture = new Furniture();

            if (reader.Read())
            {
                furniture.FurnitureId = furnitureId;
                furniture.Price = Convert.ToDouble(reader["Price"]);
                furniture.Name = Convert.ToString(reader["Name"]);
                furniture.Amount = Convert.ToInt32(reader["Amount"]);
                fabric.SupplierId = Convert.ToInt32(reader["SupplierId"]);
            }

            reader.Close();

            UpdateFurniture(furniture, model.NumberFurniture, workerId);

            double priceOrder = (fabric.Price * model.WasteFabric + furniture.Price * model.NumberFurniture + model.Price + model.CostOfWork) * productCount;

            return priceOrder;
        }

        private void UpdateFabric(Fabric fabric, double wasteFabric, int workerId)
        {
            int fabricCount = 20;
            double price = fabric.Price * fabricCount;
            DateTime dateTime = DateTime.Now;
            string date = dateTime.ToString("yyyy-MM-dd");
            string orderSupplierQuery = "";

            if (fabric.Amount == 0 && fabric.Amount < wasteFabric)
            {
                orderSupplierQuery = $"INSERT INTO [dbo].[SuplierOrders] ([SupplierId],[WorkerId],[TypeProduct],[ProductName],[Amount],[Price],[IsCompleted],[OrderDate],[ExecutionDate]) VALUES ({fabric.SupplierId},{workerId},{ProductType.Cloth},'{fabric.Name}',{fabricCount},{price},1,'{date}',null)";
            }
            else
            {
                double rest = fabric.Amount - wasteFabric;
                orderSupplierQuery = $"UPDATE [dbo].[Fabrics] SET [Amount]={rest.ToString().Replace(',','.')} WHERE [FabricId]={fabric.FabricId}";
            }

            dataContext.ExecuteQuery(orderSupplierQuery);

            UpdateWorker(workerId);
        }

        private void UpdateFurniture(Furniture furniture, int numberFurniture, int workerId)
        {
            int furnitureCount = 30;
            double price = furniture.Price * numberFurniture;
            DateTime dateTime = DateTime.Now;
            string date = dateTime.ToString("yyyy-MM-dd");
            string orderSupplierQuery = "";

            if (furniture.Amount == 0 && furniture.Amount < numberFurniture)
            {
                orderSupplierQuery = $"INSERT INTO [dbo].[SuplierOrders] ([SupplierId],[WorkerId],[TypeProduct],[ProductName],[Amount],[Price],[IsCompleted],[OrderDate],[ExecutionDate]) VALUES ({furniture.SupplierId},{workerId},{ProductType.Furniture},'{furniture.Name}',{numberFurniture},{price},1,'{date}',null)";
            }
            else
            {
                orderSupplierQuery = $"UPDATED [dbo].[Furniture] SET [Amount]={furniture.Amount - numberFurniture} WHERE [FurnitureId]={furniture.FurnitureId}";
            }

            dataContext.ExecuteQuery(orderSupplierQuery);
        }

        private void UpdateWorker(int workerId)
        {
            string workerQuery = $"UPDATE [dbo].[Workers] SET [NumberOrders]+=1 WHERE [WorkerId]={workerId} AND [WorkerId]>0";
            dataContext.ExecuteQuery(workerQuery);
        }
    }
}
