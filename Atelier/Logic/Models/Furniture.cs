namespace Atelier.Logic.Models
{
    public class Furniture
    {
        public int FurnitureId { get; set; }

        public int SupplierId { get; set; }

        public string Name { get; set; }

        public string Material { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }

        public string ImageSrc { get; set; }
    }
}
