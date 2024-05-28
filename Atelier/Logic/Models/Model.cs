namespace Atelier.Logic.Models
{
    public class Model
    {
        public int ModelId { get; set; }

        public string Name { get; set; }

        public int FabricId { get; set; }

        public string FabricName { get; set; }

        public int FurnitureId { get; set; }

        public string FurnitureName { get; set; }

        public double Price { get; set; }

        public double WasteFabric { get; set; }

        public int NumberFurniture { get; set; }

        public double CostOfWork { get; set; }

        public string ImageSrc { get; set; }

        public static Model ToModel(string query, Database database)
        {
            Model model = new Model();

            var reader = database.GetSingleRow(query);

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
