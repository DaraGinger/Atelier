using Atelier.Logic.Models;

namespace Atelier.Logic.Repositories.Interface
{
    public interface IFabricRepository
    {
        Fabric GetFabricById(int id);

        int GetMaxFabricId();

        List<string> GetFabricNames();

        void UpdateFabricAmount(double amount, int fabricId);

        void RemoveFabricAmount(double amount, int fabricId);

        Fabric GetFabricByName(string fabricName);
    }
}
