using Atelier.Logic.Models;
using System.Data;
using System.Data.SqlClient;

namespace Atelier.Logic.Entities
{
    public class Fabric
    {
        public int FabricId { get; set; }

        public int SupplierId { get; set; }

        public string Name { get; set; }

        public double Width { get; set; }

        public double Price { get; set; }

        public double Amount { get; set; }

        public string ImageSrc { get; set; }

        public static Fabric ToModel(SqlDataReader reader)
        {
            reader.Read();
            return new Fabric
            {
                FabricId = Convert.ToInt32(reader["FabricId"]),
                SupplierId = Convert.ToInt32(reader["SupplierId"]),
                Name = Convert.ToString(reader["Name"]),
                Width = Convert.ToDouble(reader["Width"]),
                Price = Convert.ToDouble(reader["Price"]),
                Amount = Convert.ToDouble(reader["Amount"]),
                ImageSrc = Convert.ToString(reader["ImageSrc"])
            };
        }

        public static List<Fabric> ToModelList(SqlDataReader sqlDataReader)
        {
            List<Fabric> list = new List<Fabric>();

            while (sqlDataReader.Read())
            {
                Fabric cloth = new Fabric();
                cloth.FabricId = Convert.ToInt32(sqlDataReader["FabricId"]);
                cloth.SupplierId = Convert.ToInt32(sqlDataReader["SupplierId"]);
                cloth.Name = Convert.ToString(sqlDataReader["Name"]);
                cloth.Width = Convert.ToDouble(sqlDataReader["Width"]);
                cloth.Width = Convert.ToDouble(sqlDataReader["Width"]);
                cloth.Price = Convert.ToDouble(sqlDataReader["Price"]);
                cloth.Amount = Convert.ToDouble(sqlDataReader["Amount"]);
                cloth.ImageSrc = Convert.ToString(sqlDataReader["ImageSrc"]);

                list.Add(cloth);
            }

            return list;
        }
    }
}
