namespace Atelier.Logic.Repositories.Interface
{
    using Atelier.Logic.Models;

    public interface IModelRepository
    {
        Model GetModelById(int id);

        int GetMaxModelId();

        List<string> GetModelNames();
    }
}
