using System.Data.SqlClient;

namespace SCM_API.Connection
{
    public class DBConnection
    {
        private readonly string connectionString = "Server=DESKTOP-EM729M7\\SQLEXPRESS;Database=sistema_gestion_matricula;Integrated Security=True;";

        public SqlConnection AbrirConexion()
        {
            SqlConnection conexion = new(connectionString);

            try
            {
                conexion.Open();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return conexion;
        }

        public void CerrarConexion(SqlConnection conexion)
        {
            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }
    }
}
