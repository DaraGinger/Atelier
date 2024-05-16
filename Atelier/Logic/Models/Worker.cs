using Atelier.Logic.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Logic.Models
{
    public class Worker
    {
        public int WorkerId { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int NumberOfOrders { get; set;}

        public static Worker FromDataReader(SqlDataReader reader)
        {
            reader.Read();
            return new Worker
            {
                WorkerId = Convert.ToInt32(reader["СlotheId"]),
                LastName = Convert.ToString(reader["LastName"]),
                Name = Convert.ToString(reader["Name"]),
                Surname = Convert.ToString(reader["SurName"]),
                NumberOfOrders = Convert.ToInt32(reader["NumberOfOrders"]),
            };
        }
    }
}
