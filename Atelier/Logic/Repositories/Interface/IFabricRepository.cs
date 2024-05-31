using Atelier.Logic.Models;

namespace Atelier.Logic.Repositories.Interface
{
    public interface IFabricRepository
    {
        Fabric GetFabricById(int id);

        int GetMaxFabricId();

        List<string> GetFabricNames();

        void UpdateFabricAmount(int product);

        void UpdateFabricAmountBySupplierOrder(int supplierOrderId);

        Fabric GetFabricByName(string fabricName);
    }
}
