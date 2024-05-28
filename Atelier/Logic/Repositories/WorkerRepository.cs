using Atelier.Logic.Models;
using Atelier.Logic.Repositories.Interface;

namespace Atelier.Logic.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly Database context;

        public WorkerRepository()
        {
            context = new Database();
        }

        public List<string> GetWorkerNames()
        {
            List<string> workerNames = new List<string>();

            var workerQuery = $"SELECT [WorkerId],[Name],[LastName],[Surname] FROM [dbo].[Workers] WHERE [NumberOrders] <= 3 AND [WorkerId]>0";

            var result = context.GetListDataQuery(workerQuery);
            while (result.Read())
            {
                int id = Convert.ToInt32(result["WorkerId"]);
                string lastName = Convert.ToString(result["LastName"]);
                string name = Convert.ToString(result["Name"]);
                string surname = Convert.ToString(result["Surname"]);

                workerNames.Add($"{id} {lastName} {name} {surname}");
            }

            result.Close();

            return workerNames;
        }

        public void UpdateWorkerNumberOrders(int workerId)
        {
            string workerQuery = $"UPDATE [dbo].[Workers] SET [NumberOrders]+=1 WHERE [WorkerId]={workerId} AND [WorkerId]>0";
            context.ExecuteQuery(workerQuery);
        }
    }
}
