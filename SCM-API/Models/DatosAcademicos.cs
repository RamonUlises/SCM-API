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

                    datosAcademicos.Add(datosAcademico);
                }
            }
            catch (System.Exception ex)
            {
                Log.WriteLog(ex.Message);
            }
        }
    }