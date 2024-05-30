using Atelier.Logic.Models;
using Atelier.Logic.Repositories.Interface;

namespace Atelier.Logic.Repositories
{
    public class FabricRepository : IFabricRepository
    {
        private readonly Database context;
        public FabricRepository() 
        {
            context = new Database();
        }

        public Fabric GetFabricById(int id)
        {
            string query = $"SELECT [FabricId],[SupplierId],[Name],[Width],[Price],[Amount],[ImageSrc] FROM [dbo].[Fabrics] WHERE [FabricId] = {id}";
            return ToModel(query);
        }

        public int GetMaxFabricId()
        {
            string query = "SELECT MAX ([FabricId]) FROM [dbo].[Fabrics]";

            var reader = context.GetSingleRow(query);

            int maxFabricId = 0;

            if (reader.Read())
            {
                maxFabricId = Convert.ToInt32(reader[0]);
            }

            reader.Close();

            return maxFabricId;
        }


        public List<string> GetFabricNames()
        {
            List<string> names = new List<string>();

            var fabricQuery = $"SELECT [FabricId],[Name] FROM [dbo].[Fabrics]";
            var result = context.GetListDataQuery(fabricQuery);
            while (result.Read())
            {
                int id = Convert.ToInt32(result["FabricId"]);
                string name = Convert.ToString(result["Name"]);

                names.Add(id + " " + name);
            }
            result.Close();

            return names;
        }

        public void UpdateFabricAmount(double amount, int productId)
        {
            string orderSupplierQuery = $"UPDATE [dbo].[Fabrics] " +
                $"SET [Amount] = Fabric.Amount-Product.NumberProducts*Model.WasteFabric " +
                $"FROM [dbo].[Fabrics] AS Fabric " +
                $"JOIN [dbo].[Products] AS Product ON Product.ProductId = {} " +
                $"JOIN [dbo].[Models] AS Model ON Model.ModelId = Product.ModelId " +
                $"WHERE Fabric.FabricId = 1";

            context.ExecuteQuery(orderSupplierQuery);
        }

        public void RemoveFabricAmount(double amount, int fabricId)
        {
            string orderSupplierQuery = $"UPDATE [dbo].[Fabrics] SET [Amount]-={amount.ToString().Replace(',', '.')} WHERE [FabricId]={fabricId}";

            context.ExecuteQuery(orderSupplierQuery);
        }

        public Fabric GetFabricByName(string fabricName)
        {
            string query = $"SELECT [FabricId],[SupplierId],[Name],[Width],[Price],[Amount],[ImageSrc] FROM [dbo].[Fabrics] WHERE [Name]='{fabricName}'";
            return ToModel(query);
        }

        public Fabric ToModel(string query)
        {
            Fabric fabric = new Fabric();
            var reader = context.GetSingleRow(query);

            if (reader.Read())
            {
                fabric.FabricId = Convert.ToInt32(reader["FabricId"]);
                fabric.SupplierId = Convert.ToInt32(reader["SupplierId"]);
                fabric.Name = Convert.ToString(reader["Name"]);
                fabric.Width = Convert.ToDouble(reader["Width"]);
                fabric.Price = Convert.ToDouble(reader["Price"]);
                fabric.Amount = Convert.ToDouble(reader["Amount"]);
                fabric.ImageSrc = Convert.ToString(reader["ImageSrc"]);
            }

            reader.Close();

            return fabric;
        }
    }
}
