using Atelier.Logic.Entities;
using System.Data.SqlClient;

namespace Atelier.Logic.Models
{
    public class Model
    {
        public int ModelId { get; set; }

        public string Name { get; set; }

        public int ClothId { get; set; }

        public string ClothName { get; set; }

        public int FurnitureId { get; set; }

        public string FurnitureName { get; set; }

        public double Price { get; set; }

        public double WasteFabric { get; set; }

        public int NumberFurniture { get; set; }

        public double CostOfWork { get; set; }

        public string ImageSrc { get; set; }

        public static Model ToModel(SqlDataReader reader)
        {
            reader.Read();
            return new Model
            {
                ModelId = Convert.ToInt32(reader["ModelId"]),
                Name = Convert.ToString(reader["Name"]),
                ClothId = Convert.ToInt32(reader["ClothId"]),
                FurnitureId = Convert.ToInt32(reader["FurnitureId"]),
                Price = Convert.ToDouble(reader["Price"]),
                WasteFabric = Convert.ToDouble(reader["WasteFabric"]),
                NumberFurniture = Convert.ToInt32(reader["WasteFabric"]),
                CostOfWork = Convert.ToDouble(reader["CostWork"]),
                ImageSrc = Convert.ToString(reader["ImageSrc"])
            };
        }

        public static List<Model> ToModelList(SqlDataReader reader)
        {
            List<Model> list = new List<Model>();

            while (reader.Read())
            {
                Model model = new Model();
                model.ModelId = Convert.ToInt32(reader["ModelId"]);
                model.Name = Convert.ToString(reader["Name"]);
                model.ClothId = Convert.ToInt32(reader["СlotheId"]);
                model.FurnitureId = Convert.ToInt32(reader["FurnitureId"]);
                model.Price = Convert.ToDouble(reader["Price"]);
                model.WasteFabric = Convert.ToDouble(reader["WasteFabric"]);
                model.NumberFurniture = Convert.ToInt32(reader["WasteFabric"]);
                model.CostOfWork = Convert.ToDouble(reader["CostWorkOf"]);
                model.ImageSrc = Convert.ToString(reader["ImageSrc"]);
                list.Add(model);
            }

            return list;
        }
    }
}
