using Atelier.Logic.Models;

namespace Atelier.Logic.Repositories.Interface
{
    public interface IFurnitureRepository
    {
        Furniture GetFurnitureById(int id);

        Furniture GetFurnitureByName(string name);

        int GetMaxFurnitureId();

        List<string> GetFurnitureNames();

        void UpdateFurnitureAmount(int productId);

        void UpdateFurnitureAmountBySupplierOrder(int supplierOrderId);
    }
}
