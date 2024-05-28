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

        public void AddOrder(Product product, string lastname, string name, string surname, int workerId)
        {
            DateTime dateTime = DateTime.Now;
            string date = dateTime.ToString("yyyy-MM-dd");

            var productQuery = $"INSERT INTO [dbo].[Products]([ModelId],[ClothId],[FurnitureId],[NumberProducts],[Price]) OUTPUT inserted.[ProductId] VALUES({product.ModelId}, {product.FabricId},{product.FurnitureId},{product.ProductCount},{product.Price})";
            var reader = context.GetSingleRow(productQuery);
            if (reader.Read())
            {
                product.ProductId = Convert.ToInt32(reader["ProductId"]);
            }
            reader.Close();

            var clientOrderQuery = $"INSERT INTO [dbo].[ClientOrders] ([LastName],[Name],[Surname],[ProductId],[WorkerId],[Price],[DateReceivingOrder],[DateFitting],[ExecutionDate],[Payment]) VALUES ({lastname},{name},{surname},{product.ProductId},{workerId},{product.Price},'{date}',null,null,{0})";
            context.ExecuteQuery(clientOrderQuery);
            reader.Close();
        }

        public void AddSupplierOrder(SupplierOrder supplierOrder)
        {
            string date = supplierOrder.OrderDate.ToString("yyyy-MM-dd");
            string price = supplierOrder.Price.ToString().Replace(',', '.');

            string orderSupplierQuery = $"INSERT INTO [dbo].[SuplierOrders] ([SupplierId],[WorkerId],[TypeProduct],[ProductName],[Amount],[Price],[IsCompleted],[OrderDate],[ExecutionDate]) VALUES ({supplierOrder.SupplierId},{supplierOrder.WorkerId},{supplierOrder.ProductType},'{supplierOrder.ProductName}',{supplierOrder.Amount},{price},1,'{date}',null)";
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

                ClientOrderHelper clientOrder = new ClientOrderHelper
                {
                    ClientOrderId = order.ClientOrderId,
                    ClientName = order.LastName + " " + order.Name + " " + order.Surname,
                    WorkerName = worker.LastName + " " + worker.Name + " " + worker.Surname,
                    DateReceivingOrder = order.DateReceivingOrder,
                    DateFitting = order.DateFitting.ToString(),
                    ExecutionDate = order.ExecutionDate.ToString(),
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
            string supplierOrdersQuery = "[SupplierOrderId],[SupplierId],[WorkerId],[TypeProduct],[ProductName],[Amount],[Price],[IsCompleted],[OrderDate],[ExecutionDate] FROM [dbo].[SupplierOrders]";

            var reader = context.GetListDataQuery(supplierOrdersQuery);

            List<SupplierOrder> orders = SupplierOrder.ToModelList(supplierOrdersQuery, context);

            List<SupplierOrderHelper> helper = new List<SupplierOrderHelper>();

            foreach (var order in orders)
            {
                string workerQuery = $"SELECT [WorkerId],[LastName],[Name],[Surname],[NumberOrders] FROM [dbo].[Workers] WHERE [WorkerId]={order.WorkerId}";
                Worker worker = Worker.ToModel(workerQuery, context);

                string supplierQuery = $"SELECT [SupplierId],[CompanyName] FROM [dbo].[Suppliers] WHERE [SupplierId]={order.SupplierId}";
                Supplier supplier = Supplier.ToModel(supplierQuery, context);

                SupplierOrderHelper supplierOrder = new SupplierOrderHelper
                {
                    SupplierOrderId = order.SupplierOrderId,
                    SupplierName = supplier.CompanyName,
                    WorkerName = worker.LastName + " " + worker.Name + " " + worker.Surname,
                    OrderDate = order.OrderDate,
                    ExecutionDate = order.ExecutionDate.ToString(),
                    Price = order.Price,
                    Amount = order.Amount,
                    ProductType = order.ProductType,
                    IsCompleted = order.IsCompleted,
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
            var orderQuery = $"UPDATE [dbo].[SupplierOrders] SET [IsCompleted]={payment} WHERE [SupplierOrderId]={supplierOrderId}";

            context.ExecuteQuery(orderQuery);
        }
    }
}
