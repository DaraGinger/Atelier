namespace Atelier.Logic.Models
{
    public class Worker
    {
        public int WorkerId { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int NumberOfOrders { get; set;}

        public static Worker ToModel(string query, Database database)
        {
            var reader = database.GetSingleRow(query);

            reader.Read();

            Worker worker = new Worker
            {
                WorkerId = Convert.ToInt32(reader["WorkerId"]),
                LastName = Convert.ToString(reader["LastName"]),
                Name = Convert.ToString(reader["Name"]),
                Surname = Convert.ToString(reader["SurName"]),
                NumberOfOrders = Convert.ToInt32(reader["NumberOrders"]),
            };

            reader.Close();

            return worker;
        }
    }
}
