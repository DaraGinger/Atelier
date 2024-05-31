namespace Atelier.Logic.Models
{
    public class CheckModel
    {
        public double FabricPrice { get; set; }

        public double Price { get; set; }

        public double FurniturePrice { get; set; }

        public double ModelPrice { get; set; }

        public double SumFabric { get; set; }

        public double SumFurniture { get; set; }

        public double SumModel { get; set; }

        public double WasteFabric { get; set; }

        public int NumberFurniture { get; set; }

        public int NumberProducts { get; set; }

        public double CostOfWork { get; set; }

        public string FabricName { get; set; }

        public string FurnitureName { get; set; }

        public string ModelName { get; set; }
    }
}
