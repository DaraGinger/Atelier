using System.Data.SqlClient;

namespace Atelier.Logic.Models
{
    public class SupplierOrder
    {
        public int SupplierOrderId { get; set; }

        public int SupplierId { get; set; }

        public int WorkerId { get; set; }

        public string ProductName { get; set; }

        public ProductType ProductType { get; set; }

        public double Amount { get; set; }

        public double Price { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public static List<SupplierOrder> ToModelList(string query, Database database)
        {
            var reader = database.GetListDataQuery(query);

            List<SupplierOrder> list = new List<SupplierOrder>();

            while (reader.Read())
            {
                SupplierOrder supplierOrder = new SupplierOrder();
                supplierOrder.SupplierOrderId = Convert.ToInt32(reader["SupplierOrderId"]);
                supplierOrder.SupplierId = Convert.ToInt32(reader["SupplierId"]);
                supplierOrder.WorkerId = Convert.ToInt32(reader["WorkerId"]);
                supplierOrder.ProductName = Convert.ToString(reader["ProductName"]);
                supplierOrder.ProductType = (ProductType)Convert.ToInt32(reader["TypeProduct"]);
                supplierOrder.WorkerId = Convert.ToInt32(reader["WorkerId"]);
                supplierOrder.Price = Convert.ToDouble(reader["Price"]);
                supplierOrder.Amount = Convert.ToDouble(reader["Amount"]);
                supplierOrder.IsCompleted = Convert.ToBoolean(reader["IsCompleted"]);
                supplierOrder.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                supplierOrder.ExecutionDate = reader["ExecutionDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["ExecutionDate"]);
                list.Add(supplierOrder);
            }

            return list;
        }
    }
}
