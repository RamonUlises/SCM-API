using SCM_API.Connection;
using System.Data.SqlClient;
namespace SCM_API.Models
{
    public class ExistingDate
    {
        public int ExistingDateId(string column, string table, string campo, string value)
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();

                string query = $"SELECT {column} FROM {table} WHERE {campo} = @value";
                SqlCommand command = new(query, conexion);
                command.Parameters.AddWithValue("@value", value);

                SqlDataReader reader = command.ExecuteReader();

                int id = 0;

                if (reader.Read())
                {
                    id = Convert.ToInt32(reader[column]);
                }

                new DBConnection().CerrarConexion(conexion);
                return id;
            } catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
