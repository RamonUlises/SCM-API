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
                string query = "EXEC sp_obtener_traslados_por_estudiante";
                SqlCommand command = new(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TrasladoClass traslado = new()
                    {
                        IdTraslado = reader["id_traslado"].ToString() ?? string.Empty,
                        IdEstudiante = reader["id_estudiante"].ToString() ?? string.Empty,
                        FechaTraslado = reader["fecha"].ToString() ?? string.Empty,
                        MotivoTraslado = reader["motivo_traslado"].ToString() ?? string.Empty,
                        IdCentro = reader["id_centro"].ToString() ?? string.Empty,
                        IdPeriodo = reader["id_periodo"].ToString() ?? string.Empty,
                        CodigoEstudiante = reader["codigo_estudiante"].ToString() ?? string.Empty

                    };

                    traslados.Add(traslado);
                }

                dbConnection.CerrarConexion(connection);
                return traslados;
            }
            catch (SqlException ex)
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

                string query = "EXEC sp_obtener_traslados_por_estudiante @Id_Estudiante";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TrasladoClass traslado = new()
                    {
                        IdTraslado = reader["id_traslado"].ToString() ?? string.Empty,
                        IdEstudiante = reader["id_estudiante"].ToString() ?? string.Empty,
                        FechaTraslado = reader["fecha"].ToString() ?? string.Empty,
                        MotivoTraslado = reader["motivo_traslado"].ToString() ?? string.Empty,
                        IdCentro = reader["id_centro"].ToString() ?? string.Empty,
                        IdPeriodo = reader["id_periodo"].ToString() ?? string.Empty ,
                        CodigoEstudiante = reader["codigo_estudiante"].ToString() ?? string.Empty


                    };

                    dbConnection.CerrarConexion(connection);
                    return traslado;
                }

                dbConnection.CerrarConexion(connection);
                return null;
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

        public Errors CrearTraslado(TrasladoClass traslado)
        {
            try
            {
                SqlConnection connection = new DBConnection().AbrirConexion();
                string query = "EXEC sp_agregar_traslado @Id_Estudiante, @Fecha_Traslado, @Motivo_Traslado, @Id_Centro, @Id_Periodo, @Codigo_Estudiante";

                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@IdEstudiante", traslado.IdEstudiante);
                command.Parameters.AddWithValue("@FechaTraslado", traslado.FechaTraslado);
                command.Parameters.AddWithValue("@MotivoTraslado", traslado.MotivoTraslado);
                command.Parameters.AddWithValue("@IdCentro", traslado.IdCentro);
                command.Parameters.AddWithValue("@IdPeriodo", traslado.IdPeriodo);
                command.Parameters.AddWithValue("@CodigoEstudiante", traslado.CodigoEstudiante);

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




    }
}


