using System.Data.SqlClient;

namespace Atelier.Logic.Models
{
    public class Client
    {
        public int ClientID { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public int FlatNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public static Client ToModel(SqlDataReader reader)
        {
            reader.Read();
            return new Client
            {
                ClientID = Convert.ToInt32(reader["ClientID"]),
                Name = Convert.ToString(reader["Name"]),
                Surname = Convert.ToString(reader["Surname"]),
                LastName = Convert.ToString(reader["LastName"]),
                Country = Convert.ToString(reader["Country"]),
                City = Convert.ToString(reader["City"]),
                Street = Convert.ToString(reader["Street"]),
                HouseNumber = Convert.ToString(reader["HouseNumber"]),
                FlatNumber = Convert.ToInt32(reader["FlatNumber"]),
                PhoneNumber = Convert.ToString(reader["PhoneNumber"]),
                Email = Convert.ToString(reader["Email"]),
            };
        }

        public static List<Client> ToModelList(SqlDataReader reader)
        {
            List<Client> list = new List<Client>();

            while (reader.Read())
            {
                Client client = new Client();
                client.ClientID = Convert.ToInt32(reader["ClientID"]);
                client.Name = Convert.ToString(reader["Name"]);
                client.Surname = Convert.ToString(reader["Surname"]);
                client.LastName = Convert.ToString(reader["LastName"]);
                client.Country = Convert.ToString(reader["Country"]);
                client.City = Convert.ToString(reader["City"]);
                client.Street = Convert.ToString(reader["Street"]);
                client.HouseNumber = Convert.ToString(reader["HouseNumber"]);
                client.FlatNumber = Convert.ToInt32(reader["FlatNumber"]);
                client.PhoneNumber = Convert.ToString(reader["PhoneNumber"]);
                client.Email = Convert.ToString(reader["Email"]);
                list.Add(client);
            }

            return list;
        }
    }
}
