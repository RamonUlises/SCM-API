using SCM_API.Clase;
using SCM_API.Connection;
using SCM_API.Controllers;
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

                int idEstudiante = new ExistingDate().ExistingDateId("id_estudiante", "estudiantes", "id_estudiante", traslado.IdEstudiante.ToLower());
                int idCentro = new ExistingDate().ExistingDateId("id_centro", "centros", "centro", traslado.IdCentro.ToLower());
                int idPeriodo = new ExistingDate().ExistingDateId("id_periodo", "periodos", "periodo", traslado.IdPeriodo.ToLower());
                bool existDatos = new ExistingDate().ExistingDatosAcademicos(traslado.CodigoEstudiante);
                bool existTraslado = new ExistingDate().ExistingTraslado(traslado.CodigoEstudiante, traslado.IdEstudiante);

                if (existTraslado)
                {
                    new DBConnection().CerrarConexion(connection);
                    return new Errors { status = false, message = "Traslado ya existe" };
                }

                if (!existDatos)
                {
                    new DBConnection().CerrarConexion(connection);
                    return new Errors { status = false, message = "Datos academicos no encontrados" };
                }

                if (idEstudiante == 0)
                {
                    new DBConnection().CerrarConexion(connection);
                    return new Errors { message = "Estudiante no encontrado", status = false };
                }

                if (idCentro == 0)
                { 
                    new DBConnection().CerrarConexion(connection);
                    return new Errors { message = "Centro no encontrado", status = false };
                }

                if (idPeriodo == 0)
                {
                    new DBConnection().CerrarConexion(connection);
                    return new Errors { message = "Periodo no encontrado", status = false };
                }

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

                int idEstudiante = new ExistingDate().ExistingDateId("id_estudiante", "estudiantes", "id_estudiante", traslado.IdEstudiante.ToLower());
                int idCentro = new ExistingDate().ExistingDateId("id_centro", "centros", "centro", traslado.IdCentro.ToLower());
                int idPeriodo = new ExistingDate().ExistingDateId("id_periodo", "periodos", "periodo", traslado.IdPeriodo.ToLower());
                bool existDatos = new ExistingDate().ExistingDatosAcademicos(traslado.CodigoEstudiante);
                int idTraslado = new ExistingDate().ExistingDateId("id_traslado", "traslados", "id_traslado", id.ToString());

                if (idTraslado == 0)
                {
                    new DBConnection().CerrarConexion(connection);
                    return new Errors { message = "Traslado no encontrado", status = false };
                }
                if (!existDatos)
                {
                    new DBConnection().CerrarConexion(connection);
                    return new Errors { status = false, message = "Datos academicos no encontrados" };
                }
                if (idEstudiante == 0)
                {
                    new DBConnection().CerrarConexion(connection);
                    return new Errors { message = "Estudiante no encontrado", status = false };
                }
                if (idCentro == 0)
                {
                    new DBConnection().CerrarConexion(connection);
                    return new Errors { message = "Centro no encontrado", status = false };
                }
                if (idPeriodo == 0)
                {
                    new DBConnection().CerrarConexion(connection);
                    return new Errors { message = "Periodo no encontrado", status = false };
                }

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
                return new Errors { message = "Traslado no encontrado", status = false };
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return new Errors { message = "Traslado no encontrado", status = false };
            }
        }


    }
}


