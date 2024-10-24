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
                        Repitente = Convert.ToDouble(reader["repitente"]) == 1,
                        Modalidad = reader["modalidad"].ToString() ?? string.Empty,
                        Grado = reader["grado"].ToString() ?? string.Empty,
                        Seccion = reader["seccion"].ToString() ?? string.Empty,
                        Turno = reader["turno"].ToString() ?? string.Empty,
                        Centro = reader["centro"].ToString() ?? string.Empty,
                        IdEstudiante = reader["estudiante"].ToString() ?? string.Empty
                    };

                    datosAcademicos.Add(datosAcademico);
                }
                new DBConnection().CerrarConexion(conexion);
                return datosAcademicos;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public DatosAcademicosClass? ObtenerDatosAcademicosCodigo(string codigo)
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();

                string query = "EXEC sp_obtener_datos_academicos_codigo @codigo";
                SqlCommand command = new(query, conexion);
                command.Parameters.AddWithValue("@codigo", codigo);
                SqlDataReader reader = command.ExecuteReader();

                DatosAcademicosClass? datosAcademico = null;
                if (reader.Read())
                {
                    datosAcademico = new()
                    {
                        CodigoEstudiante = reader["codigo_estudiante"].ToString() ?? string.Empty,
                        FechaMatricula = reader["fecha_matricula"].ToString() ?? string.Empty,
                        NivelEducativo = reader["nivel_educativo"].ToString() ?? string.Empty,
                        Repitente = Convert.ToDouble(reader["repitente"]) == 1,
                        Modalidad = reader["modalidad"].ToString() ?? string.Empty,
                        Grado = reader["grado"].ToString() ?? string.Empty,
                        Seccion = reader["seccion"].ToString() ?? string.Empty,
                        Turno = reader["turno"].ToString() ?? string.Empty,
                        Centro = reader["centro"].ToString() ?? string.Empty,
                        IdEstudiante = reader["estudiante"].ToString() ?? string.Empty
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
    }
}