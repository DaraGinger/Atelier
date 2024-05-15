using Atelier.Logic.Models;
using System.Data;
using System.Data.SqlClient;

namespace Atelier.Logic.Entities
{
    public class Cloth
    {
        public int ClothId { get; set; }

        public string Name { get; set; }

        public double Width { get; set; }

        public double Price { get; set; }

        public double Amount { get; set; }

        public string ImageSrc { get; set; }

        public static Cloth FromDataReader(SqlDataReader reader)
        {
            reader.Read();
            return new Cloth
            {
                ClothId = Convert.ToInt32(reader["СlotheId"]),
                Name = Convert.ToString(reader["Name"]),
                Width = Convert.ToDouble(reader["Width"]),
                Price = Convert.ToDouble(reader["Price"]),
                Amount = Convert.ToDouble(reader["Amount"]),
                ImageSrc = Convert.ToString(reader["ImageSrc"])
            };
        }

        public static List<Cloth> ToModelList(SqlDataReader sqlDataReader)
        {
            List<Cloth> list = new List<Cloth>();

            while (sqlDataReader.Read())
            {
                Cloth cloth = new Cloth();
                cloth.ClothId = Convert.ToInt32(sqlDataReader["СlotheId"]);
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
