namespace Atelier.Logic.Models
{
    public class SupplierOrderHelper
    {
        public int SupplierOrderId { get; set; }

        public string SupplierName { get; set; }

        public string WorkerName { get; set; }

        public string ProductName { get; set; }

        public ProductType ProductType { get; set; }

        public double Amount { get; set; }

        public double Price { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime OrderDate { get; set; }

        public string ExecutionDate { get; set; }
    }
}
