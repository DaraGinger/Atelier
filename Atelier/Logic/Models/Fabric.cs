using Atelier.Logic.Models;
using System.Data;
using System.Data.SqlClient;

namespace Atelier.Logic.Models
{
    public class Fabric
    {
        public int FabricId { get; set; }

        public int SupplierId { get; set; }

        public string Name { get; set; }

        public double Width { get; set; }

        public double Price { get; set; }

        public double Amount { get; set; }

        public string ImageSrc { get; set; }
    }
}
