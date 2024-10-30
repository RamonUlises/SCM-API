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
                        Grado = Convert.ToInt32(reader["grado"].ToString()),
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
            catch
            {
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
                        Grado = Convert.ToInt32(reader["grado"].ToString()),
                        Seccion = reader["seccion"].ToString() ?? string.Empty,
                        Turno = reader["turno"].ToString() ?? string.Empty,
                        Centro = reader["centro"].ToString() ?? string.Empty,
                        IdEstudiante = reader["estudiante"].ToString() ?? string.Empty
                    };
                }
                new DBConnection().CerrarConexion(conexion);
                return datosAcademico;
            }
            catch
            {
                return null;
            }
        }
        public Errors CrearDatosAcademicos(DatosAcademicosClass datosAcademicos)
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();

                string query = "EXEC sp_agregar_datos_academicos @codigo_estudiante, @fecha_matricula, @nivel_educativo, @repitente, @id_modalidad, @id_grado, @id_seccion, @id_turno, @id_centro, @id_estudiante";

                int idModalidad = new ExistingDate().ExistingDateId("id_modalidad", "modalidades", "modalidad", datosAcademicos.Modalidad.ToLower());
                int idGrado = new ExistingDate().ExistingDateId("id_grado", "grados", "grado", datosAcademicos.Grado.ToString());
                int idSeccion = new ExistingDate().ExistingDateId("id_seccion", "secciones", "seccion", datosAcademicos.Seccion.ToLower());
                int idTurno = new ExistingDate().ExistingDateId("id_turno", "turnos", "turno", datosAcademicos.Turno.ToLower());
                int idCentro = new ExistingDate().ExistingDateId("id_centro", "centros", "centro", datosAcademicos.Centro.ToLower());
                int idEstudiante = new ExistingDate().ExistingDateId("id_estudiante", "estudiantes", "id_estudiante", datosAcademicos.IdEstudiante.ToLower());
                bool existDatos = new ExistingDate().ExistingDatosAcademicos(datosAcademicos.CodigoEstudiante);
                bool existEstudiante = new ExistingDate().ExistingEstudentDatosAcademicos(datosAcademicos.IdEstudiante);

                if(existDatos)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { status = false, message = "Ya existen datos académicos con este código de estudiante" };
                }

                if (existEstudiante)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { status = false, message = "Este estudiante ya posee datos académicos" };
                }                           

                if (idModalidad == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { status = false, message = "Modalidad no encontrada" };
                }
                if (idGrado == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { status = false, message = "Grado no encontrado" };
                }
                if (idSeccion == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { status = false, message = "Seccion no encontrada" };
                }
                if (idTurno == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { status = false, message = "Turno no encontrado" };
                }
                if (idCentro == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { status = false, message = "Centro no encontrado" };
                }
                if (idEstudiante == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { status = false, message = "Estudiante no encontrado" };
                }

                SqlCommand command = new(query, conexion);
                command.Parameters.AddWithValue("@codigo_estudiante", datosAcademicos.CodigoEstudiante);
                command.Parameters.AddWithValue("@fecha_matricula", datosAcademicos.FechaMatricula);
                command.Parameters.AddWithValue("@nivel_educativo", datosAcademicos.NivelEducativo);
                command.Parameters.AddWithValue("@repitente", datosAcademicos.Repitente);
                command.Parameters.AddWithValue("@id_modalidad", idModalidad);
                command.Parameters.AddWithValue("@id_grado", idGrado);
                command.Parameters.AddWithValue("@id_seccion", idSeccion);
                command.Parameters.AddWithValue("@id_turno", idTurno);
                command.Parameters.AddWithValue("@id_centro", idCentro);
                command.Parameters.AddWithValue("@id_estudiante", idEstudiante);

                if (command.ExecuteNonQuery() > 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { status = true, message = "Datos academicos creados" };
                }
                new DBConnection().CerrarConexion(conexion);
                return new Errors { status = false, message = "Error al crear datos academicos" };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new Errors { message = "Error al crear datos academicos", status = false };
            }
        }
        public Errors EditarDatosAcademicos(string codigo, DatosAcademicosClass datosAcademicos)
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();

                string query = "EXEC sp_actualizar_datos_academicos @codigo_estudiante, @fecha_matricula, @nivel_educativo, @repitente, @id_modalidad, @id_grado, @id_seccion, @id_turno, @id_centro, @id_estudiante";

                int idModalidad = new ExistingDate().ExistingDateId("id_modalidad", "modalidades", "modalidad", datosAcademicos.Modalidad.ToLower());
                int idGrado = new ExistingDate().ExistingDateId("id_grado", "grados", "grado", datosAcademicos.Grado.ToString());
                int idSeccion = new ExistingDate().ExistingDateId("id_seccion", "secciones", "seccion", datosAcademicos.Seccion.ToLower());
                int idTurno = new ExistingDate().ExistingDateId("id_turno", "turnos", "turno", datosAcademicos.Turno.ToLower());
                int idCentro = new ExistingDate().ExistingDateId("id_centro", "centros", "centro", datosAcademicos.Centro.ToLower());
                int idEstudiante = new ExistingDate().ExistingDateId("id_estudiante", "estudiantes", "id_estudiante", datosAcademicos.IdEstudiante.ToLower());
                bool existDatos = new ExistingDate().ExistingDatosAcademicos(codigo);

                if (!existDatos)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { status = false, message = "Datos academicos no encontrados" };
                }

                if (idModalidad == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { status = false, message = "Modalidad no encontrada" };
                }
                if (idGrado == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { status = false, message = "Grado no encontrado" };
                }
                if (idSeccion == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { status = false, message = "Seccion no encontrada" };
                }
                if (idTurno == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { status = false, message = "Turno no encontrado" };
                }
                if (idCentro == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors
                    {
                        status = false,
                        message = "Centro no encontrado"
                    };

                }
                if (idEstudiante == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { status = false, message = "Estudiante no encontrado" };
                }

                SqlCommand command = new(query, conexion);
                command.Parameters.AddWithValue("@codigo_estudiante", codigo);
                command.Parameters.AddWithValue("@fecha_matricula", datosAcademicos.FechaMatricula);
                command.Parameters.AddWithValue("@nivel_educativo", datosAcademicos.NivelEducativo);
                command.Parameters.AddWithValue("@repitente", datosAcademicos.Repitente);
                command.Parameters.AddWithValue("@id_modalidad", idModalidad);
                command.Parameters.AddWithValue("@id_grado", idGrado);
                command.Parameters.AddWithValue("@id_seccion", idSeccion);
                command.Parameters.AddWithValue("@id_turno", idTurno);
                command.Parameters.AddWithValue("@id_centro", idCentro);
                command.Parameters.AddWithValue("@id_estudiante", idEstudiante);

                if (command.ExecuteNonQuery() > 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { status = true, message = "Datos academicos editados" };
                }
                new DBConnection().CerrarConexion(conexion);
                return new Errors { status = false, message = "Error al editar datos academicos" };
            }
            catch
            {
                return new Errors { message = "Error al editar datos academicos", status = false };
            }
        }
        public Errors EliminarDatosAcademicos(string codigo)
        {
            try
            {
                bool exist = new ExistingDate().ExistingDatosAcademicos(codigo);

                if (!exist)
                {
                    return new Errors { message = "Datos academicos no encontrados", status = false };
                }

                SqlConnection conexion = new DBConnection().AbrirConexion();

                string query = "EXEC sp_eliminar_datos_academicos @codigo";

                SqlCommand command = new(query, conexion);
                command.Parameters.AddWithValue("@codigo", codigo);
                command.ExecuteNonQuery();

                new DBConnection().CerrarConexion(conexion);
                return new Errors { message = "Datos academicos eliminado correctamente", status = true };
            }
            catch
            {
                return new Errors { message = $"Para eliminar los datos academicos de {codigo} primero debe eliminar sus traslados", status = false };
            }
        }
    }
}