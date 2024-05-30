using Atelier.Logic.Models;
using Atelier.Logic.Repositories.Interface;

namespace Atelier.Logic.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Database context;

        public OrderRepository()
        {
            context = new Database();
        }

        public ClientOrder AddOrder(Product product, string lastname, string name, string surname, int workerId)
        {
            DateTime dateTime = DateTime.Now;
            string date = dateTime.ToString("yyyy-MM-dd");

            var productQuery = $"INSERT INTO [dbo].[Products]([ModelId],[FabricId],[FurnitureId],[NumberProducts],[Price]) " +
                $"OUTPUT inserted.[ProductId],inserted.[Price]" +
                $"SELECT {product.ModelId}, {product.FabricId},{product.FurnitureId},{product.NumberProducts},(Fabric.Price*Model.WasteFabric+Furniture.Price*Model.NumberFurniture+Model.Price+Model.CostWork)*1 " +
                $"FROM [dbo].[Fabrics] AS Fabric Join [dbo].[Furnitures] " +
                $"AS Furniture ON Furniture.FurnitureId = {product.FurnitureId} " +
                $"JOIN [dbo].[Models] AS Model ON Model.ModelId = {product.ModelId} " +
                $"WHERE Fabric.[FabricId] = {product.FabricId}";

            var reader = context.GetSingleRow(productQuery);

            if (reader.Read())
            {
                product.ProductId = Convert.ToInt32(reader["ProductId"]);
                product.Price = Convert.ToDouble(reader["Price"]);
            }

            reader.Close();

            var clientOrderQuery = $"INSERT INTO [dbo].[ClientOrders] ([LastName],[Name],[Surname],[ProductId],[WorkerId],[Price],[DateReceivingOrder],[DateFitting],[ExecutionDate],[Payment]) OUTPUT inserted.[ClientOrderId] VALUES ('{lastname}','{name}','{surname}',{product.ProductId},{workerId},{product.Price.ToString().Replace(',','.')},'{date}',null,null,{0})";
            reader = context.GetSingleRow(clientOrderQuery);
            int clentOrderId = 0;

            if (reader.Read())
            {
                clentOrderId = Convert.ToInt32(reader["ClientOrderId"]);
            }
            reader.Close();

            return new ClientOrder
            {
                ClientOrderId = clentOrderId,
                Price = product.Price,
            };
        }

        public void AddSupplierOrder(SupplierOrder supplierOrder)
        {
            string date = supplierOrder.OrderDate.ToString("yyyy-MM-dd");
            string price = supplierOrder.Price.ToString().Replace(',', '.');

            string orderSupplierQuery = $"INSERT INTO [dbo].[SupplierOrders] " +
                $"([SupplierId],[WorkerId],[TypeProduct],[ProductName],[Amount],[Price],[IsCompleted],[OrderDate],[ExecutionDate],[IsPaid]) " +
                $"VALUES ({supplierOrder.SupplierId},{supplierOrder.WorkerId},{(int)supplierOrder.ProductType},'{supplierOrder.ProductName}',{supplierOrder.Amount},{price},0,'{date}',null,0)";
        
            context.ExecuteQuery(orderSupplierQuery);
        }

        public List<ClientOrderHelper> GetOrders()
        {
            string clientOrdersQuery = "SELECT [ClientOrderId],[LastName],[Name],[Surname],[ProductId],[WorkerId],[Price],[DateReceivingOrder],[DateFitting],[ExecutionDate],[Payment] FROM [dbo].[ClientOrders]";

            var reader = context.GetListDataQuery(clientOrdersQuery);

            List<ClientOrder> orders = ClientOrder.ToModelList(reader);

            List<ClientOrderHelper> helper = new List<ClientOrderHelper>();

            foreach (var order in orders)
            {
                string workerQuery = $"SELECT [WorkerId],[LastName],[Name],[Surname],[NumberOrders] FROM [dbo].[Workers] WHERE [WorkerId]={order.WorkerId}";
                Worker worker = Worker.ToModel(workerQuery, context);
                DateTime dateFitting = order.DateFitting != null ? (DateTime)order.DateFitting : DateTime.MinValue;
                DateTime executionDate = order.ExecutionDate != null ? (DateTime)order.ExecutionDate : DateTime.MinValue;

                ClientOrderHelper clientOrder = new ClientOrderHelper
                {
                    ClientOrderId = order.ClientOrderId,
                    ProductId = order.ProductId,
                    ClientName = order.LastName + " " + order.Name + " " + order.Surname,
                    WorkerName = worker.LastName + " " + worker.Name + " " + worker.Surname,
                    DateReceivingOrder = order.DateReceivingOrder,
                    DateFitting = dateFitting.ToString("yyyy-MM-dd") != DateTime.MinValue.ToString("yyyy-MM-dd") ? dateFitting.ToString("yyyy-MM-dd") : null,
                    ExecutionDate = executionDate.ToString("yyyy-MM-dd") != DateTime.MinValue.ToString("yyyy-MM-dd") ? executionDate.ToString("yyyy-MM-dd") : null,
                    Price = order.Price,
                    Payment = order.Payment
                };

                helper.Add(clientOrder);
            }

            return helper;
        }

        public void UpdateFittingDate(int clientOrderId, string date)
        {
            string clientOrdersQuery = $"UPDATE [dbo].[ClientOrders] SET [DateFitting]='{date}' WHERE [ClientOrderId]={clientOrderId}";

            context.ExecuteQuery(clientOrdersQuery);
        }

        public void UpdateExecutionDate(int clientOrderId, string date)
        {
            string clientOrdersQuery = $"UPDATE [dbo].[ClientOrders] SET [ExecutionDate]='{date}' WHERE [ClientOrderId]={clientOrderId}";

            context.ExecuteQuery(clientOrdersQuery);
        }

        public void UpdatePayment(int clientOrderId, int payment)
        {
            var orderQuery = $"UPDATE [dbo].[ClientOrders] SET [Payment]={payment} WHERE [ClientOrderId]={clientOrderId}";

            context.ExecuteQuery(orderQuery);
        }

        public List<ClientOrder> GetDebtors()
        {
            var orderQuery = "SELECT [ClientOrderId],[Payment],[LastName],[Name],[Surname],[Price] FROM [Atelier].[dbo].[ClientOrders] WHERE [Payment] = 0";

            var clientOrdersReader = context.GetListDataQuery(orderQuery);
            List<ClientOrder> clientOrders = ClientOrder.ToDebtersList(clientOrdersReader);
            clientOrdersReader.Close();

            return clientOrders;
        }

        public List<SupplierOrderHelper> GetSupplierOrders()
        {
            string supplierOrdersQuery = "SELECT [SupplierOrderId],[SupplierId],[WorkerId],[TypeProduct],[ProductName],[Amount],[Price],[IsCompleted],[IsPaid],[OrderDate],[ExecutionDate] FROM [dbo].[SupplierOrders]";

            var reader = context.GetListDataQuery(supplierOrdersQuery);

            List<SupplierOrder> orders = SupplierOrder.ToModelList(supplierOrdersQuery, context);

            List<SupplierOrderHelper> helper = new List<SupplierOrderHelper>();

            foreach (var order in orders)
            {
                string workerQuery = $"SELECT [WorkerId],[LastName],[Name],[Surname],[NumberOrders] FROM [dbo].[Workers] WHERE [WorkerId]={order.WorkerId}";
                Worker worker = Worker.ToModel(workerQuery, context);

                string supplierQuery = $"SELECT [SupplierId],[CompanyName] FROM [dbo].[Suppliers] WHERE [SupplierId]={order.SupplierId}";
                Supplier supplier = Supplier.ToModel(supplierQuery, context);

                DateTime executionDate = order.ExecutionDate != null ? (DateTime)order.ExecutionDate : DateTime.MinValue;

                SupplierOrderHelper supplierOrder = new SupplierOrderHelper
                {
                    SupplierOrderId = order.SupplierOrderId,
                    SupplierName = supplier.CompanyName,
                    ProductName = order.ProductName,
                    WorkerName = worker.LastName + " " + worker.Name + " " + worker.Surname,
                    OrderDate = order.OrderDate,
                    ExecutionDate = executionDate.ToString("yyyy-MM-dd") != DateTime.MinValue.ToString("yyyy-MM-dd") ? executionDate.ToString("yyyy-MM-dd") : null,
                    Price = order.Price,
                    Amount = order.Amount,
                    ProductType = order.ProductType,
                    IsCompleted = order.IsCompleted,
                    IsPaid = order.IsPaid
                };

                helper.Add(supplierOrder);
            }

            return helper;
        }

        public void UpdateSupplierExecutionDate(int supplierOrderId, string date)
        {
            string clientOrdersQuery = $"UPDATE [dbo].[SupplierOrders] SET [ExecutionDate]='{date}' WHERE [SupplierOrderId]={supplierOrderId}";

            context.ExecuteQuery(clientOrdersQuery);
        }

        public void UpdateSupplierPayment(int supplierOrderId, int payment)
        {
            var orderQuery = $"UPDATE [dbo].[SupplierOrders] SET [IsPaid]={payment} WHERE [SupplierOrderId]={supplierOrderId}";

            context.ExecuteQuery(orderQuery);
        }

        public void UpdateSupplierIsComplete(int supplierOrderId, int isCompleted)
        {
            var orderQuery = $"UPDATE [dbo].[SupplierOrders] SET [IsCompleted]={isCompleted} WHERE [SupplierOrderId]={supplierOrderId}";

            context.ExecuteQuery(orderQuery);
        }

        public ClientOrder GetOrderById(int id)
        {
            string query = $"SELECT [ClientOrderId],[Payment],[LastName],[Name],[Surname],[Price],[ProductId] FROM [Atelier].[dbo].[ClientOrders] WHERE [ClientOrderId] = {id}";
            
            return ClientOrder.ToModel(query, context);
        }

        public Product GetProductById(int id)
        {
            string query = $"SELECT [ProductId],[ModelId],[FabricId],[FurnitureId],[NumberProducts],[Price] FROM [dbo].[Products] WHERE [ProductId]={id}";

            var reader = context.GetSingleRow(query);
            Product product = new Product()

;           if (reader.Read())
            {
                product.FurnitureId = Convert.ToInt32(reader["FurnitureId"]);
                product.FabricId = Convert.ToInt32(reader["FabricId"]);
                product.ModelId = Convert.ToInt32(reader["ModelId"]);
                product.NumberProducts = Convert.ToInt32(reader["NumberProducts"]);
                product.Price = Convert.ToDouble(reader["Price"]);
            }

            return product;
        }
    }
}
