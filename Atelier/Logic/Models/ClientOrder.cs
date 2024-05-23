using System.Data.SqlClient;

namespace Atelier.Logic.Models
{
    public class ClientOrder
    {
        public int ClientOrderId { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int ProductId { get; set; }

        public int WorkerId { get; set; }

        public double Price { get; set; }

        public DateTime DateReceivingOrder { get; set; }

        public DateTime DateFitting { get; set; }

        public DateTime ExecutionDate { get; set; }

        public bool Payment {  get; set; }

        public static ClientOrder ToModel(SqlDataReader reader)
        {
            reader.Read();
            return new ClientOrder
            {
                ClientOrderId = Convert.ToInt32(reader["ClientOrderId"]),
                Name = Convert.ToString(reader["Name"]),
                Surname = Convert.ToString(reader["Surname"]),
                LastName = Convert.ToString(reader["LastName"]),
                ProductId = Convert.ToInt32(reader["ProductId"]),
                WorkerId = Convert.ToInt32(reader["WorkerId"]),
                Price = Convert.ToDouble(reader["Price"]),
                DateReceivingOrder = Convert.ToDateTime(reader["DateReceivingOrder"]),
                DateFitting = Convert.ToDateTime(reader["DateFitting"]),
                ExecutionDate = Convert.ToDateTime(reader["ExecutionDate"]),
                Payment = Convert.ToBoolean(reader["Payment"]),
            };
        }

        public static List<ClientOrder> ToDebtersList(SqlDataReader reader)
        {
            List<ClientOrder> list = new List<ClientOrder>();

            while (reader.Read())
            {
                ClientOrder clientOrder = new ClientOrder();
                clientOrder.ClientOrderId = Convert.ToInt32(reader["ClientOrderId"]);
                clientOrder.Name = Convert.ToString(reader["Name"]);
                clientOrder.Surname = Convert.ToString(reader["Surname"]);
                clientOrder.LastName = Convert.ToString(reader["LastName"]);
                clientOrder.Price = Convert.ToDouble(reader["Price"]);
                clientOrder.Payment = Convert.ToBoolean(reader["Payment"]);
                list.Add(clientOrder);
            }

            return list;
        }

        public static List<ClientOrder> ToModelList(SqlDataReader reader)
        {
            List<ClientOrder> list = new List<ClientOrder>();

            while (reader.Read())
            {
                ClientOrder clientOrder = new ClientOrder();
                clientOrder.ClientOrderId = Convert.ToInt32(reader["ClientOrderId"]);
                clientOrder.ProductId = Convert.ToInt32(reader["ProductId"]);
                clientOrder.WorkerId = Convert.ToInt32(reader["WorkerId"]);
                clientOrder.Price = Convert.ToDouble(reader["Price"]);
                clientOrder.DateReceivingOrder = Convert.ToDateTime(reader["DateReceivingOrder"]);
                clientOrder.DateFitting = Convert.ToDateTime(reader["DateFitting"]);
                clientOrder.ExecutionDate = Convert.ToDateTime(reader["ExecutionDate"]);
                clientOrder.Payment = Convert.ToBoolean(reader["Payment"]);
                list.Add(clientOrder);
            }

            return list;
        }
    }
}
