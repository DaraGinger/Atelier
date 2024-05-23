namespace Atelier.Logic.Models
{
    using System.Data.SqlClient;

    public class Furniture
    {
        public int FurnitureId { get; set; }

        public int SupplierId { get; set; }

        public string Name { get; set; }

        public string Material { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }

        public string ImageSrc { get; set; }

        public static Furniture ToModel(SqlDataReader reader)
        {
            reader.Read();
            return new Furniture
            {
                FurnitureId = Convert.ToInt32(reader["FurnitureId"]),
                SupplierId = Convert.ToInt32(reader["SupplierId"]),
                Name = Convert.ToString(reader["Name"]),
                Material = Convert.ToString(reader["Material"]),
                Price = Convert.ToDouble(reader["Price"]),
                Amount = Convert.ToInt32(reader["Amount"]),
                ImageSrc = Convert.ToString(reader["ImageSrc"])
            };
        }

        public static List<Furniture> ToModelList(SqlDataReader sqlDataReader)
        {
            List<Furniture> list = new List<Furniture>();

            while (sqlDataReader.Read())
            {
                Furniture furnitureId = new Furniture();
                furnitureId.FurnitureId = Convert.ToInt32(sqlDataReader["FurnitureId"]);
                furnitureId.SupplierId = Convert.ToInt32(sqlDataReader["SupplierId"]);
                furnitureId.Name = Convert.ToString(sqlDataReader["Name"]);
                furnitureId.Material = Convert.ToString(sqlDataReader["Material"]);
                furnitureId.Price = Convert.ToDouble(sqlDataReader["Price"]);
                furnitureId.Amount = Convert.ToInt32(sqlDataReader["Amount"]);
                furnitureId.ImageSrc = Convert.ToString(sqlDataReader["ImageSrc"]);

                list.Add(furnitureId);
            }

            return list;
        }
    }
}
