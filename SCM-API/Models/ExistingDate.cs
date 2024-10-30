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
        public bool ExistingDatosAcademicos(string codigoEstudiante)
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();

                string query = "SELECT * FROM datos_academicos WHERE codigo_estudiante = @codigoEstudiante";
                SqlCommand command = new(query, conexion);
                command.Parameters.AddWithValue("@codigoEstudiante", codigoEstudiante);

                SqlDataReader reader = command.ExecuteReader();

                bool exist = false;

                if (reader.Read())
                {
                    exist = true;
                }

                new DBConnection().CerrarConexion(conexion);
                return exist;
            } catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool ExistingEstudentDatosAcademicos(string idEstudiante)
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();

                string query = "SELECT * FROM datos_academicos WHERE id_estudiante = @idEstudiante";
                SqlCommand command = new(query, conexion);
                command.Parameters.AddWithValue("@idEstudiante", idEstudiante);

                SqlDataReader reader = command.ExecuteReader();

                bool exist = false;

                if (reader.Read())
                {
                    exist = true;
                }

                new DBConnection().CerrarConexion(conexion);
                return exist;
            } catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool ExistingTraslado(string codigoEstudiante, string idEstudiante)
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();

                string query = "SELECT * FROM traslados WHERE codigo_estudiante = @codigoEstudiante OR id_estudiante = @idEstudiante";
                SqlCommand command = new(query, conexion);
                command.Parameters.AddWithValue("@codigoEstudiante", codigoEstudiante);
                command.Parameters.AddWithValue("@idEstudiante", idEstudiante);

                SqlDataReader reader = command.ExecuteReader();

                bool exist = false;

                if (reader.Read())
                {
                    exist = true;
                }

                new DBConnection().CerrarConexion(conexion);
                return exist;
            } catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
