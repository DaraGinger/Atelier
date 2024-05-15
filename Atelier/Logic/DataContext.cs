using System.Data;
using System.Data.SqlClient;

namespace Atelier.Logic
{
    public class DataContext
    {
        public DataContext()
        {
            connectionString = "Data Source=(localdb)\\k2024; Initial Catalog=Atelier; Integrated Security=True";
        }

        public string connectionString;

        public void ExecuteQuery(string query)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public SqlDataReader GetListDataQuery(string query)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            { 
                SqlDataReader reader = sqlCommand.ExecuteReader(CommandBehavior.SingleResult);
                return reader;
            }
        }

        public SqlDataReader GetSingleRow(string query)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                SqlDataReader reader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow);
                return reader;
            }
        }
    }
}
