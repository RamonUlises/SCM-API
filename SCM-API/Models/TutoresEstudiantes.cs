using System.Data.SqlClient;
using SCM_API.Clase;
using SCM_API.Connection;
using SCM_API.Controllers;
using SCM_API.Lib;

namespace SCM_API.Models
{
    public class TutoresEstudiantes
    {
        public TutoresXEstudiantesClass? ObtenerTutoresEstudiantes(int id)
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();
                string query = "EXEC sp_obtener_tutor_x_estudiante @id_tutor_x_estudiante";

                SqlCommand command = new(query, conexion);
                command.Parameters.AddWithValue("@id_tutor_x_estudiante", id);

                SqlDataReader reader = command.ExecuteReader();

                TutoresXEstudiantesClass? tutor = null;

                if (reader.Read())
                {
                    tutor = new()
                    {
                        Id_tutor_x_estudiante = Convert.ToInt32(reader["id_tutor_x_estudiante"]),
                        Nombre = reader["nombres"].ToString() ?? string.Empty,
                        Apellido = reader["apellidos"].ToString() ?? string.Empty,
                        Cedula = reader["cedula"].ToString() ?? string.Empty,
                        Telefono = reader["telefono"].ToString() ?? string.Empty,
                    };
                }

                new DBConnection().CerrarConexion(conexion);
                return tutor;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public List<TutoresXEstudiantesClass>? ObtenerTutoresEstudiantes()
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();
                string query = "EXEC sp_obtener_tutores_x_estudiantes";

                SqlCommand command = new(query, conexion);
                SqlDataReader reader = command.ExecuteReader();

                List<TutoresXEstudiantesClass>? tutores = [];

                while (reader.Read()) {
                    TutoresXEstudiantesClass tutor = new()
                    {
                        Id_tutor_x_estudiante = Convert.ToInt32(reader["id_tutor_x_estudiante"]),
                        Nombre = reader["nombres"].ToString() ?? string.Empty,
                        Apellido = reader["apellidos"].ToString() ?? string.Empty,
                        Telefono = reader["telefono"].ToString() ?? string.Empty,
                        Cedula = reader["cedula"].ToString() ?? string.Empty,
                    };

                    tutores.Add(tutor);
                }

                new DBConnection().CerrarConexion(conexion);
                return tutores;
            } catch {
                return null;
            }
        }
        public Errors CrearTutoresEstudiantes(TutoresXEstudiantesClass estudiante)
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();
                string query = "EXEC sp_agregar_tutor_x_estudiante @nombres, @apellidos, @cedula, @telefono";

                int idTutorEstudiante = new TutoresEstudiantes().TutorCedula(estudiante.Cedula);

                 if (idTutorEstudiante == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { message = "Tutor no encontrado", status = false };
                }

                SqlCommand command = new(query, conexion);
                command.Parameters.AddWithValue("@nombres", estudiante.Nombre);
                command.Parameters.AddWithValue("@apellidos", estudiante.Apellido);
                command.Parameters.AddWithValue("@telefono", estudiante.Telefono);
                

                if (command.ExecuteNonQuery() > 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { message = "Tutor creado correctamente", status = true };
                }

                new DBConnection().CerrarConexion(conexion);
                return new Errors { message = "Error al crear Tutor", status = false };
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return new Errors { message = "Error al crear Tutor", status = false };
            }
        }
        public Errors EditarTutoresEstudiantes(int id, TutoresXEstudiantesClass estudiante)
        {
            try
            {
                int id_tutor_x_estudiante = new ExistingDate().ExistingDateId("id_tutor_x_estudiante", "tutor", "id_tutor_x_estudiante", id.ToString());

                if (id_tutor_x_estudiante == 0)
                {
                    return new Errors { message = "Tutor no encontrado", status = false };
                }

                SqlConnection conexion = new DBConnection().AbrirConexion();
                string query = "EXEC sp_editar_tutor_x_estudiante @id_tutor_x_estudiante, @nombres, @apellidos, @cedula, @telefono";

                //editar tutor
                int id_tutor = new ExistingDate().ExistingDateId("id_tutor_x_estudiante", "Tutor", "id_tutor_x_estudiante", id.ToString());



                if (id_tutor_x_estudiante == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { message = "Tutor no encontrado", status = false };
                }

                SqlCommand command = new(query, conexion);
                command.Parameters.AddWithValue("@id_tutor_x_estudiante", id);
                command.Parameters.AddWithValue("@nombres", estudiante.Nombre);
                command.Parameters.AddWithValue("@apellidos", estudiante.Apellido);
                command.Parameters.AddWithValue("@cedula", estudiante.Cedula);
                command.Parameters.AddWithValue("@telefono", estudiante.Telefono);
                
                if (command.ExecuteNonQuery() > 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { message = "Estudiante actualizado correctamente", status = true };
                }

                new DBConnection().CerrarConexion(conexion);
                return new Errors { message = "Error al actualizar tutor", status = false };
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return new Errors { message = "Error al actualizar tutor", status = false };
            }
        }
        public Errors EliminarTutoresEstudiantes(int id)
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();
                string query = "EXEC sp_borrar_tutor_x_estudiante @id_tutor_x_estudiante";

                SqlCommand command = new(query, conexion);
                command.Parameters.AddWithValue("@id_tutor_x_estudiante", id);
                command.ExecuteNonQuery();

                new DBConnection().CerrarConexion(conexion);
                return new Errors { message = "Tutor eliminado con exito", status = true };
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return new Errors { message = "No se puede eliminar el tutor porque esta enlazado a un estudiante", status = false };
            }
        }
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
