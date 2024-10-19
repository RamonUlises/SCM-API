using SCM_API.Clase;
using SCM_API.Connection;
using SCM_API.Lib;
using System.Data.SqlClient;

namespace SCM_API.Models
{
    public class DatosAcademicos
    {
        public List<DatosAcademicosClass>? ObtenerDatosAcademicos()
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();

                string query = "EXEC sp_obtener_datos_academicos";
                SqlCommand command = new(query, conexion);
                SqlDataReader reader = command.ExecuteReader();

                List<DatosAcademicosClass> datosAcademicos = [];

                while (reader.Read())
                {
                    DatosAcademicosClass datosAcademico = new()
                    {
                        CodigoEstudiante = reader["codigo_estudiante"].ToString() ?? string.Empty,
                        FechaMatricula = reader["fecha_matricula"].ToString() ?? string.Empty,
                        NivelEducativo = reader["nivel_educativo"].ToString() ?? string.Empty,
                        Repitente = reader["repitente"].ToString() ?? string.Empty,
                        Grado = reader["grado"].ToString() ?? string.Empty,
                        Seccion = reader["seccion"].ToString() ?? string.Empty,
                        Turno = reader["turno"].ToString() ?? string.Empty,
                        Centro = reader["centro"].ToString() ?? string.Empty,
                        IdEstudiante = reader["IdEstudiante"].ToString() ?? string.Empty
                    };

                    datosAcademicos.Add(datosAcademico);
                }
                new DBConnection().CerrarConexion(conexion);
                return datosAcademicos;
            }catch (SqlException ex)\
            {
                Console.WriteLine(ex.Message);
                return null;
            }
           
        }
          
        }
        public DatosAcademicosClass? ObtenerDatosAcademicosPorId(string id)
            {
                try
                {
                    SqlConnection conexion = new DBConnection().AbrirConexion();

                    string query = "EXEC sp_obtener_datos_academicos_por_id @id";
                    SqlCommand command = new(query, conexion);
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();

                    DatosAcademicosClass datosAcademico = new();

                    while (reader.Read())
                    {
                        datosAcademico = new()
                        {
                            CodigoEstudiante = reader["CodigoEstudiante"].ToString() ?? string.Empty,
                            FechaMatricula = reader["FechaMatricula"].ToString() ?? string.Empty,
                            NivelEducativo = reader["NivelEducativo"].ToString() ?? string.Empty,
                            Repitente = reader["Repitente"].ToString() ?? string.Empty,
                            Grado = reader["Grado"].ToString() ?? string.Empty,
                            Seccion = reader["Seccion"].ToString() ?? string.Empty,
                            Turno = reader["Turno"].ToString() ?? string.Empty,
                            Centro = reader["Centro"].ToString() ?? string.Empty,
                            IdEstudiante = reader["IdEstudiante"].ToString() ?? string.Empty
                        };
                    }
                    new DBConnection().CerrarConexion(conexion);
                    return datosAcademico;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        public Errors CrearDatosAcademicos(DatosAcademicosClass datosAcademicos)
            {
                try
                {
                    SqlConnection conexion = new DBConnection().AbrirConexion();

                    string query = "EXEC sp_crear_datos_academicos @CodigoEstudiante, @FechaMatricula, @NivelEducativo, @Repitente, @Grado, @Seccion, @Turno, @Centro, @IdEstudiante";
                    SqlCommand command = new(query, conexion);
                    command.Parameters.AddWithValue("@CodigoEstudiante", datosAcademicos.CodigoEstudiante);
                    command.Parameters.AddWithValue("@FechaMatricula", datosAcademicos.FechaMatricula);
                    command.Parameters.AddWithValue("@NivelEducativo", datosAcademicos.NivelEducativo);
                    command.Parameters.AddWithValue("@Repitente", datosAcademicos.Repitente);
                    command.Parameters.AddWithValue("@Grado", datosAcademicos.Grado);
                    command.Parameters.AddWithValue("@Seccion", datosAcademicos.Seccion);
                    command.Parameters.AddWithValue("@Turno", datosAcademicos.Turno);
                    command.Parameters.AddWithValue("@Centro", datosAcademicos.Centro);
                    command.Parameters.AddWithValue("@IdEstudiante", datosAcademicos.IdEstudiante);
                    command.ExecuteNonQuery();

                    new DBConnection().CerrarConexion(conexion);
                    return Errors.None;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return Errors.SqlException;
                }
            }
    }