using Atelier.Logic.Models;

namespace Atelier.Logic.Repositories.Interface
{
    public interface IFurnitureRepository
    {
        Furniture GetFurnitureById(int id);

        Furniture GetFurnitureByName(string name);

        int GetMaxFurnitureId();

        List<string> GetFurnitureNames();

        void UpdateFurnitureAmount(double amount, int furnitureId);
    }
}
