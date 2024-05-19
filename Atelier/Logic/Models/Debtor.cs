namespace Atelier.Logic.Models
{
    public class Debtor
    {
        public string Name { get; set; }

        public int ClientId { get; set; }

        public int ClientOrderId { get; set; }

        public double Price { get; set; }

        public bool Payment { get; set; }

        public static Debtor ToModel(ClientOrder clientOrder)
        {
            Debtor debtor = new Debtor
            {
                ClientId = clientOrder.ClientId,
                ClientOrderId = clientOrder.ClientOrderId,
                Name = $"{clientOrder.Client.Surname} {clientOrder.Client.Name} {clientOrder.Client.LastName}",
                Price = clientOrder.Price,
                Payment = clientOrder.Payment,
            };

            return debtor;
        }
    }
}
