namespace Atelier.Logic.Repositories.Interface
{
    public interface IWorkerRepository
    {
        List<string> GetWorkerNames();

        void UpdateWorkerNumberOrders(int workerId);
    }
}
