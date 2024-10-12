using System.Data.SqlClient;
using SCM_API.Connection;

namespace SCM_API.Models
{
    public class TutoresEstudiantes
    {
        public int TutorCedula(string cedula)
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();
                string query = "SELECT id_tutor_x_estudiante FROM tutores_x_estudiantes WHERE cedula = @cedula";
                SqlCommand command = new(query, conexion);

                command.Parameters.AddWithValue("@cedula", cedula);
                SqlDataReader reader = command.ExecuteReader();

                int id = 0;

                if(reader.Read())
                {
                    id = Convert.ToInt32(reader["id_tutor_x_estudiante"]);
                }

                return id;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
