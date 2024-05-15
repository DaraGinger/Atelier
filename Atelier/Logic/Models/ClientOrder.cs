namespace Atelier.Logic.Models
{
    public class ClientOrder
    {
        public int ClientOrderId { get; set; }

        public int ClientId { get; set; }

        public int ProductId { get; set; }

        public int WorkerId { get; set; }

        public double Price { get; set; }

        public DateTime DateReceivingOrder { get; set; }

        public DateTime DateFitting { get; set; }

        public DateTime ExecutionDate { get; set; }

        public bool Payment {  get; set; }
    }
}
