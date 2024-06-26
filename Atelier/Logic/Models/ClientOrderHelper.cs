﻿namespace Atelier.Logic.Models
{
    public class ClientOrderHelper
    {
        public int ClientOrderId { get; set; }

        public int ProductId { get; set; }

        public string ClientName { get; set; }

        public string WorkerName { get; set; }

        public double Price { get; set; }

        public DateTime DateReceivingOrder { get; set; }

        public string DateFitting { get; set; }

        public string ExecutionDate { get; set; }

        public bool Payment { get; set; }
    }
}
