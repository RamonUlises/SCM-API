using SCM_API.Clase;
using SCM_API.Connection;
using SCM_API.Controllers;
using SCM_API.Lib;
using System.Data.SqlClient;

namespace SCM_API.Models
{
    public class Tutoresxestudiantes
    {
        public List<TutoresxestudiantesClass>? ObtenerTutor()
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();

                string query = "EXEC sp_obtener_tutores_x_estudiantes";
                SqlCommand command = new(query, conexion);
                SqlDataReader reader = command.ExecuteReader();

                List<TutoresxestudiantesClass> tutores = [];

                while (reader.Read())
                {
                    TutoresxestudiantesClass ttutores = new()
                    {
                        IdTutorxEstudiante = reader["id_estudiante"].ToString() ?? string.Empty,
                        Nombres = reader["nombres"].ToString() ?? string.Empty,
                        Apellidos = reader["apellidos"].ToString() ?? string.Empty,
                        Cedula = reader["cedula"].ToString() ?? string.Empty,
                        Telefono = reader["telefono"].ToString() ?? string.Empty
                    };
                    tutores.Add(ttutores);
                }

                new DBConnection().CerrarConexion(conexion);
                return tutores;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
     }
}
