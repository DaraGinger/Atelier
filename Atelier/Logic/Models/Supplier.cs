namespace Atelier.Logic.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        public string CompanyName { get; set; }

        public static Supplier ToModel(string query, Database database)
        {
            var reader = database.GetSingleRow(query);

            reader.Read();

            Supplier supplier = new Supplier
            {
                SupplierId = Convert.ToInt32(reader["SupplierId"]),
                CompanyName = Convert.ToString(reader["CompanyName"])
            };

            reader.Close();

            return supplier;
        }
    }
}
