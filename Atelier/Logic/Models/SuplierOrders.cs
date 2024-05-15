namespace Atelier.Logic.Models
{
    public class SuplierOrders
    {
        public int SuplierOrderId { get; set; }

        public int SuplierId { get; set; }

        public int WorkerId { get; set; }

        public ProductType ProductType { get; set; }

        public double Amount { get; set; }

        public double PaymentAmount { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ExecutionDate { get; set; }
    }
}
