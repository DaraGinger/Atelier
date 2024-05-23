namespace Atelier.Logic.Models
{
    public class Debtor
    {
        public string Name { get; set; }

        public int ClientOrderId { get; set; }

        public double Price { get; set; }

        public bool Payment { get; set; }

        public static Debtor ToModel(ClientOrder clientOrder)
        {
            Debtor debtor = new Debtor
            {
                ClientOrderId = clientOrder.ClientOrderId,
                Name = $"{clientOrder.Surname} {clientOrder.Name} {clientOrder.LastName}",
                Price = clientOrder.Price,
                Payment = clientOrder.Payment,
            };

            return debtor;
        }
    }
}
