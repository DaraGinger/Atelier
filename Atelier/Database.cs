using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier
{
    public class Database
    {
        public Database() 
        {
            connectionString = "DataSource=(localdb)\MSSQLLocalDB; Initial Cataloq = ";
        }

        public string connectionString;
    }
}
