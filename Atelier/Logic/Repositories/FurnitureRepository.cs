using Atelier.Logic.Models;
using Atelier.Logic.Repositories.Interface;

namespace Atelier.Logic.Repositories
{
    public class FurnitureRepository : IFurnitureRepository
    {
        private readonly Database context;

        public FurnitureRepository()
        {
            context = new Database();
        }

        public Furniture GetFurnitureById(int id)
        {
            string query = $"SELECT [FurnitureId],[SupplierId],[Name],[Material],[Amount],[Price],[ImageSrc] FROM [Atelier].[dbo].[Furnitures] WHERE [FurnitureId] = {id}";

            return ToModel(query);

        }
        public Furniture GetFurnitureByName(string name)
        {
            string query = $"SELECT [FurnitureId],[SupplierId],[Name],[Material],[Amount],[Price],[ImageSrc] FROM [Atelier].[dbo].[Furnitures] WHERE [Name]='{name}'";

            return ToModel(query);
        }

        public int GetMaxFurnitureId()
        {
            string query = "SELECT MAX ([FurnitureId]) FROM [dbo].[Furnitures]";
            var result = context.GetSingleRow(query);

            int maxId = 0;

            if (result.Read())
            {
                maxId = Convert.ToInt32(result[0]);
            }

            result.Close();

            return maxId;
        }

        public List<string> GetFurnitureNames()
        {
            List<string> names = new List<string>();

            var fabricQuery = $"SELECT [FurnitureId],[Name] FROM [dbo].[Furnitures] WHERE [FurnitureId]>0";
            var result = context.GetListDataQuery(fabricQuery);

            while (result.Read())
            {
                int id = Convert.ToInt32(result["FurnitureId"]);
                string name = Convert.ToString(result["Name"]);

                names.Add(id + " " + name);
            }
            result.Close();

            return names;
        }

        public void UpdateFurnitureAmount(double amount, int furnitureId)
        {
            string orderSupplierQuery = $"UPDATE [dbo].[Furnitures] SET [Amount]={amount} WHERE [FurnitureId]={furnitureId}";

            context.ExecuteQuery(orderSupplierQuery);
        }

        public Furniture ToModel(string query)
        {
            Furniture furniture = new Furniture();

            var reader = context.GetSingleRow(query);

            if (reader.Read())
            {
                furniture.FurnitureId = Convert.ToInt32(reader["FurnitureId"]);
                furniture.SupplierId = Convert.ToInt32(reader["SupplierId"]);
                furniture.Name = Convert.ToString(reader["Name"]);
                furniture.Material = Convert.ToString(reader["Material"]);
                furniture.Price = Convert.ToDouble(reader["Price"]);
                furniture.Amount = Convert.ToInt32(reader["Amount"]);
                furniture.ImageSrc = Convert.ToString(reader["ImageSrc"]);
            }

            reader.Close();

            return furniture;
        }
    }
}
