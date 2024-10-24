using SCM_API.Clase;
using SCM_API.Connection;
using SCM_API.Lib;
using System.Data.SqlClient;

namespace SCM_API.Models
{
    public class Traslados
    {
        public List<TrasladoClass>? ObtenerTraslado()
        {
            DBConnection dbConnection = new();
            SqlConnection connection = dbConnection.AbrirConexion();

            try
            {
                List<TrasladoClass> traslados = [];
                string query = "EXEC sp_obtener_traslados";
                SqlCommand command = new(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TrasladoClass traslado = new()
                    {
                        IdTraslado = Convert.ToInt32(reader["id_traslado"]),
                        MotivoTraslado = reader["motivo_traslado"].ToString() ?? string.Empty,
                        FechaTraslado = reader["fecha_traslado"].ToString() ?? string.Empty,
                        CodigoEstudiante = reader["codigo_estudiante"].ToString() ?? string.Empty,
                        IdCentro = reader["centro"].ToString() ?? string.Empty,
                        IdPeriodo = reader["periodo"].ToString() ?? string.Empty,
                        IdEstudiante = reader["estudiante"].ToString() ?? string.Empty,

                    };
                    traslados.Add(traslado);
                }

                dbConnection.CerrarConexion(connection);
                return traslados;
            }
            catch
            {
                dbConnection.CerrarConexion(connection);
                return null;
            }
        }
        public TrasladoClass? ObtenerTraslado(int id)
        {
            try
            {
                DBConnection dbConnection = new();
                SqlConnection connection = dbConnection.AbrirConexion();

                string query = "EXEC sp_obtener_traslados_id @id";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TrasladoClass traslado = new()
                    {
                        IdTraslado = Convert.ToInt32(reader["id_traslado"]),
                        MotivoTraslado = reader["motivo_traslado"].ToString() ?? string.Empty,
                        FechaTraslado = reader["fecha_traslado"].ToString() ?? string.Empty,
                        CodigoEstudiante = reader["codigo_estudiante"].ToString() ?? string.Empty,
                        IdCentro = reader["centro"].ToString() ?? string.Empty,
                        IdPeriodo = reader["periodo"].ToString() ?? string.Empty,
                        IdEstudiante = reader["estudiante"].ToString() ?? string.Empty,
                    };

                    dbConnection.CerrarConexion(connection);
                    return traslado;
                }

                dbConnection.CerrarConexion(connection);
                return null;
            }
            catch
            {
                return null;
            }
        }

        public Errors CrearTraslado(TrasladoClass traslado)
        {
            try
            {
                SqlConnection connection = new DBConnection().AbrirConexion();
                string query = "EXEC sp_agregar_traslado @Motivo_Traslado, @Fecha_Traslado, @Codigo_Estudiante, @Id_Centro, @Id_Periodo, @Id_Estudiante";
                
                int idEstudiante = Convert.ToInt32(traslado.IdEstudiante);
                int idCentro = Convert.ToInt32(traslado.IdCentro);
                int idPeriodo = Convert.ToInt32(traslado.IdPeriodo);

                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Id_Estudiante", idEstudiante);
                command.Parameters.AddWithValue("@Fecha_Traslado", traslado.FechaTraslado);
                command.Parameters.AddWithValue("@Motivo_Traslado", traslado.MotivoTraslado);
                command.Parameters.AddWithValue("@Id_Centro", idCentro);
                command.Parameters.AddWithValue("@Id_Periodo", idPeriodo);
                command.Parameters.AddWithValue("@Codigo_Estudiante", traslado.CodigoEstudiante);

                if (command.ExecuteNonQuery() > 0)
                {
                    new DBConnection().CerrarConexion(connection);
                    return new Errors { message = "Traslado creado correctamente", status = true };
                }

                new DBConnection().CerrarConexion(connection);
                return new Errors { message = "Error al crear traslado", status = false };
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return new Errors { message = "Error al crear traslado", status = false };
            }
        }
        public Errors EditarTraslado(int id, TrasladoClass traslado)
        {
            try
            {
                SqlConnection connection = new DBConnection().AbrirConexion();
                string query = "EXEC sp_actualizar_traslado @Id_Traslado, @Motivo_Traslado, @Fecha_Traslado, @Codigo_Estudiante, @Id_Centro, @Id_Periodo, @Id_Estudiante";

                int idEstudiante = Convert.ToInt32(traslado.IdEstudiante);
                int idCentro = Convert.ToInt32(traslado.IdCentro);
                int idPeriodo = Convert.ToInt32(traslado.IdPeriodo);

                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Id_Traslado", id);
                command.Parameters.AddWithValue("@Id_Estudiante", idEstudiante);
                command.Parameters.AddWithValue("@Fecha_Traslado", traslado.FechaTraslado);
                command.Parameters.AddWithValue("@Motivo_Traslado", traslado.MotivoTraslado);
                command.Parameters.AddWithValue("@Id_Centro", idCentro);
                command.Parameters.AddWithValue("@Id_Periodo", idPeriodo);
                command.Parameters.AddWithValue("@Codigo_Estudiante", traslado.CodigoEstudiante);

                if (command.ExecuteNonQuery() > 0)
                {
                    new DBConnection().CerrarConexion(connection);
                    return new Errors { message = "Traslado editado correctamente", status = true };
                }

                new DBConnection().CerrarConexion(connection);
                return new Errors { message = "Error al editar traslado", status = false };
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return new Errors { message = "Error al editar traslado", status = false };
            }
        }   
        public Errors EliminarTraslado(int id)
        {
            try
            {
                SqlConnection connection = new DBConnection().AbrirConexion();
                string query = "EXEC sp_eliminar_traslado @Id_Traslado";

                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Id_Traslado", id);

                if (command.ExecuteNonQuery() > 0)
                {
                    new DBConnection().CerrarConexion(connection);
                    return new Errors { message = "Traslado eliminado correctamente", status = true };
                }

                new DBConnection().CerrarConexion(connection);
                return new Errors { message = "Error al eliminar traslado", status = false };
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return new Errors { message = "Error al eliminar traslado", status = false };
            }
        }


    }
}


