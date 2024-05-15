namespace Atelier.Logic.Models
{
    using System.Data.SqlClient;

    public interface IToModel
    {
        void ToModel(SqlDataReader sqlDataReader);
    }
}
