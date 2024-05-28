using Atelier.Logic.Models;
using Atelier.Logic.Repositories.Interface;

namespace Atelier.Logic.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly Database context;

        public ModelRepository()
        {
            context = new Database();
        }

        public Model GetModelById(int id)
        {
            string query = $"SELECT [ModelId],[Name],[FabricId],[FurnitureId],[Price],[WasteFabric],[NumberFurniture],[CostWork],[ImageSrc] FROM [Atelier].[dbo].[Models] WHERE [ModelId] = {id}";
            return ToModel(query);
        }

        public int GetMaxModelId()
        {
            string query = "SELECT MAX ([ModelId]) FROM [dbo].[Models]";
            var result = context.GetSingleRow(query);

            int maxId = 0;
            if (result.Read())
            {
                maxId = Convert.ToInt32(result[0]);
            }

            return maxId;
        }

        public List<string> GetModelNames()
        {
            List<string> names = new List<string>();

            var modelQuery = $"SELECT [ModelId],[Name] FROM [dbo].[Models]";
            var result = context.GetListDataQuery(modelQuery);

            while (result.Read())
            {
                int id = Convert.ToInt32(result["ModelId"]);
                string name = Convert.ToString(result["Name"]);

                names.Add(id + " " + name);
            }
            result.Close();

            return names;
        }

        public Model ToModel(string query)
        {
            Model model = new Model();

            var reader = context.GetSingleRow(query);

            if (reader.Read())
            {
                model.ModelId = Convert.ToInt32(reader["ModelId"]);
                model.Name = Convert.ToString(reader["Name"]);
                model.FabricId = Convert.ToInt32(reader["FabricId"]);
                model.FurnitureId = Convert.ToInt32(reader["FurnitureId"]);
                model.Price = Convert.ToDouble(reader["Price"]);
                model.WasteFabric = Convert.ToDouble(reader["WasteFabric"]);
                model.NumberFurniture = Convert.ToInt32(reader["NumberFurniture"]);
                model.CostOfWork = Convert.ToDouble(reader["CostWork"]);
                model.ImageSrc = Convert.ToString(reader["ImageSrc"]);
            }

            reader.Close();

            return model;
        }
    }
}
