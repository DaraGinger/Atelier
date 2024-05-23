namespace Atelier.Logic.Models
{
    public class ClientOrderHelper
    {
        public int ClientOrderId { get; set; }

        public string ClientName { get; set; }

        public string FabricName { get; set; }

        public string FurnitureName { get; set; }

        public string ModelName { get; set; }

        public string WorkerName { get; set; }

        public double Price { get; set; }

        public DateTime DateReceivingOrder { get; set; }

        public DateTime DateFitting { get; set; }

        public DateTime ExecutionDate { get; set; }

        public bool Payment { get; set; }
    }
}
