using SCM_API.Clase;
using SCM_API.Connection;
using SCM_API.Lib;
using System.Data.SqlClient;

namespace SCM_API.Models
{
    public class Estudiantes
    {
        public List<EstudiantesClass>? ObtenerEstudiantes()
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();

                string query = "EXEC sp_obtener_estudiantes";
                SqlCommand command = new(query, conexion);
                SqlDataReader reader = command.ExecuteReader();

                List<EstudiantesClass> estudiantes = [];

                while (reader.Read())
                {
                    EstudiantesClass estudiante = new()
                    { 
                        IdEstudiante = reader["id_estudiante"].ToString() ?? string.Empty,
                        Nombres = reader["nombres"].ToString() ?? string.Empty,
                        Apellidos = reader["apellidos"].ToString() ?? string.Empty,
                        Cedula = reader["cedula"].ToString() ?? string.Empty,
                        FechaNacimiento = reader["fecha_nacimiento"].ToString() ?? string.Empty,
                        Direccion = reader["direccion"].ToString() ?? string.Empty,
                        Telefono = reader["telefono"].ToString() ?? string.Empty,
                        PartidaNacimiento = Convert.ToDouble(reader["partida_nacimiento"]) == 1,
                        FechaMatricula = reader["fecha_matricula"].ToString() ?? string.Empty,
                        Barrio = reader["barrio"].ToString() ?? string.Empty,
                        Peso = Convert.ToDouble(reader["peso"]),
                        Talla = Convert.ToDouble(reader["talla"]),
                        TerritorioIndigena = reader["territorio_indigena"].ToString() ?? string.Empty,
                        ComunidadIndigena = reader["comunidad_indigena"].ToString() ?? string.Empty,
                        Pais = reader["pais"].ToString() ?? string.Empty,
                        Departamento = reader["departamento"].ToString() ?? string.Empty,
                        Municipio = reader["municipio"].ToString() ?? string.Empty,
                        Nacionalidad = reader["nacionalidad"].ToString() ?? string.Empty,
                        Sexo = reader["sexo"].ToString() ?? string.Empty,
                        Etnia = reader["etnia"].ToString() ?? string.Empty,
                        Lengua = reader["lengua"].ToString() ?? string.Empty,
                        Discapacidad = reader["discapacidad"].ToString() ?? string.Empty,
                        TutorEstudiante = reader["nombres_tutor"].ToString() ?? string.Empty
                    };

                    estudiantes.Add(estudiante);
                }

                new DBConnection().CerrarConexion(conexion);
                return estudiantes;
            } catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public EstudiantesClass? ObtenerEstudiante(int id)
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();
                string query = "EXEC sp_obtener_estudiante @id_estudiante";

                SqlCommand command = new(query, conexion);
                command.Parameters.AddWithValue("@id_estudiante", id);

                SqlDataReader reader = command.ExecuteReader();

                EstudiantesClass? estudiante = null;

                if (reader.Read())
                {
                    estudiante = new()
                    {
                        IdEstudiante = reader["id_estudiante"].ToString() ?? string.Empty,
                        Nombres = reader["nombres"].ToString() ?? string.Empty,
                        Apellidos = reader["apellidos"].ToString() ?? string.Empty,
                        Cedula = reader["cedula"].ToString() ?? string.Empty,
                        FechaNacimiento = reader["fecha_nacimiento"].ToString() ?? string.Empty,
                        Direccion = reader["direccion"].ToString() ?? string.Empty,
                        Telefono = reader["telefono"].ToString() ?? string.Empty,
                        PartidaNacimiento = Convert.ToDouble(reader["partida_nacimiento"]) == 1,
                        FechaMatricula = reader["fecha_matricula"].ToString() ?? string.Empty,
                        Barrio = reader["barrio"].ToString() ?? string.Empty,
                        Peso = Convert.ToDouble(reader["peso"]),
                        Talla = Convert.ToDouble(reader["talla"]),
                        TerritorioIndigena = reader["territorio_indigena"].ToString() ?? string.Empty,
                        ComunidadIndigena = reader["comunidad_indigena"].ToString() ?? string.Empty,
                        Pais = reader["pais"].ToString() ?? string.Empty,
                        Departamento = reader["departamento"].ToString() ?? string.Empty,
                        Municipio = reader["municipio"].ToString() ?? string.Empty,
                        Nacionalidad = reader["nacionalidad"].ToString() ?? string.Empty,
                        Sexo = reader["sexo"].ToString() ?? string.Empty,
                        Etnia = reader["etnia"].ToString() ?? string.Empty,
                        Lengua = reader["lengua"].ToString() ?? string.Empty,
                        Discapacidad = reader["discapacidad"].ToString() ?? string.Empty,
                        TutorEstudiante = reader["nombres_tutor"].ToString() ?? string.Empty
                    };
                }

                new DBConnection().CerrarConexion(conexion);
                return estudiante;
            } catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public Errors CrearEstudiante(EstudiantesClass estudiante)
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();
                string query = "EXEC sp_insertar_estudiante @nombres, @apellidos, @cedula, @fecha_nacimiento, @direccion, @telefono, @partida_nacimiento, @fecha_matricula, @barrio, @peso, @talla, @territorio_indigena, @comunidad_indigena, @pais, @departamento, @municipio, @nacionalidad, @id_sexo, @id_etnia, @id_lengua, @id_discapacidad, @id_tutor_estudiante";

                int idSexo = new ExistingDate().ExistingDateId("id_sexo", "sexos", "sexo", estudiante.Sexo.ToLower());
                int idEtnia = new ExistingDate().ExistingDateId("id_etnia", "etnias", "etnia", estudiante.Etnia.ToLower());
                int idLengua = new ExistingDate().ExistingDateId("id_lengua", "lenguas", "lengua", estudiante.Lengua.ToLower());
                int idDiscapacidad = new ExistingDate().ExistingDateId("id_discapacidad", "discapacidades", "discapacidad", estudiante.Discapacidad.ToLower());
                int idTutorEstudiante = new TutoresEstudiantes().TutorCedula(estudiante.TutorEstudiante);

                if(idSexo == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { message = "Sexo no encontrado", status = false };
                }

                if(idEtnia == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { message = "Etnia no encontrada", status = false };
                }

                if(idLengua == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { message = "Lengua no encontrada", status = false };
                }

                if(idDiscapacidad == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { message = "Discapacidad no encontrada", status = false };
                }

                if(idTutorEstudiante == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { message = "Tutor no encontrado", status = false };
                }

                SqlCommand command = new(query, conexion);
                command.Parameters.AddWithValue("@nombres", estudiante.Nombres);
                command.Parameters.AddWithValue("@apellidos", estudiante.Apellidos);
                command.Parameters.AddWithValue("@cedula", estudiante.Cedula);
                command.Parameters.AddWithValue("@fecha_nacimiento", estudiante.FechaNacimiento);
                command.Parameters.AddWithValue("@direccion", estudiante.Direccion);
                command.Parameters.AddWithValue("@telefono", estudiante.Telefono);
                command.Parameters.AddWithValue("@partida_nacimiento", estudiante.PartidaNacimiento);
                command.Parameters.AddWithValue("@fecha_matricula", estudiante.FechaMatricula);
                command.Parameters.AddWithValue("@barrio", estudiante.Barrio);
                command.Parameters.AddWithValue("@peso", estudiante.Peso);
                command.Parameters.AddWithValue("@talla", estudiante.Talla);
                command.Parameters.AddWithValue("@territorio_indigena", estudiante.TerritorioIndigena);
                command.Parameters.AddWithValue("@comunidad_indigena", estudiante.ComunidadIndigena);
                command.Parameters.AddWithValue("@pais", estudiante.Pais);
                command.Parameters.AddWithValue("@departamento", estudiante.Departamento);
                command.Parameters.AddWithValue("@municipio", estudiante.Municipio);
                command.Parameters.AddWithValue("@nacionalidad", estudiante.Nacionalidad);
                command.Parameters.AddWithValue("@id_sexo", idSexo);
                command.Parameters.AddWithValue("@id_etnia", idEtnia);
                command.Parameters.AddWithValue("@id_lengua", idLengua);
                command.Parameters.AddWithValue("@id_discapacidad", idDiscapacidad);
                command.Parameters.AddWithValue("@id_tutor_estudiante", idTutorEstudiante);
                   
                if (command.ExecuteNonQuery() > 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { message = "Estudiante creado correctamente", status = true };
                }

                new DBConnection().CerrarConexion(conexion);
                return new Errors { message = "Error al crear estudiante", status = false };
            } catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return new Errors { message = "Error al crear estudiante", status = false };
            }
        }
        public Errors EditarEstudiante(int id, EstudiantesClass estudiante)
        {
            try
            {
                int idEstudiante = new ExistingDate().ExistingDateId("id_estudiante", "estudiantes", "id_estudiante", id.ToString());

                if(idEstudiante == 0)
                {
                    return new Errors { message = "Estudiante no encontrado", status = false };
                }

                SqlConnection conexion = new DBConnection().AbrirConexion();
                string query = "EXEC sp_editar_estudiante @id_estudiante, @nombres, @apellidos, @cedula, @fecha_nacimiento, @direccion, @telefono, @partida_nacimiento, @fecha_matricula, @barrio, @peso, @talla, @territorio_indigena, @comunidad_indigena, @pais, @departamento, @municipio, @nacionalidad, @id_sexo, @id_etnia, @id_lengua, @id_discapacidad, @id_tutor_estudiante";
                
                int idSexo = new ExistingDate().ExistingDateId("id_sexo", "sexos", "sexo", estudiante.Sexo.ToLower());
                int idEtnia = new ExistingDate().ExistingDateId("id_etnia", "etnias", "etnia", estudiante.Etnia.ToLower());
                int idLengua = new ExistingDate().ExistingDateId("id_lengua", "lenguas", "lengua", estudiante.Lengua.ToLower());
                int idDiscapacidad = new ExistingDate().ExistingDateId("id_discapacidad", "discapacidades", "discapacidad", estudiante.Discapacidad.ToLower());
                int idTutorEstudiante = new TutoresEstudiantes().TutorCedula(estudiante.TutorEstudiante);

                if(idSexo == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { message = "Sexo no encontrado", status = false };
                }

                if(idEtnia == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { message = "Etnia no encontrada", status = false };
                }

                if(idLengua == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { message = "Lengua no encontrada", status = false };
                }

                if(idDiscapacidad == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { message = "Discapacidad no encontrada", status = false };
                }

                if(idTutorEstudiante == 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { message = "Tutor no encontrado", status = false };
                }

                SqlCommand command = new(query, conexion);
                command.Parameters.AddWithValue("@id_estudiante", id);
                command.Parameters.AddWithValue("@nombres", estudiante.Nombres);
                command.Parameters.AddWithValue("@apellidos", estudiante.Apellidos);
                command.Parameters.AddWithValue("@cedula", estudiante.Cedula);
                command.Parameters.AddWithValue("@fecha_nacimiento", estudiante.FechaNacimiento);
                command.Parameters.AddWithValue("@direccion", estudiante.Direccion);
                command.Parameters.AddWithValue("@telefono", estudiante.Telefono);
                command.Parameters.AddWithValue("@partida_nacimiento", estudiante.PartidaNacimiento);   
                command.Parameters.AddWithValue("@fecha_matricula", estudiante.FechaMatricula);
                command.Parameters.AddWithValue("@barrio", estudiante.Barrio);
                command.Parameters.AddWithValue("@peso", estudiante.Peso);
                command.Parameters.AddWithValue("@talla", estudiante.Talla);
                command.Parameters.AddWithValue("@territorio_indigena", estudiante.TerritorioIndigena);
                command.Parameters.AddWithValue("@comunidad_indigena", estudiante.ComunidadIndigena);
                command.Parameters.AddWithValue("@pais", estudiante.Pais);
                command.Parameters.AddWithValue("@departamento", estudiante.Departamento);
                command.Parameters.AddWithValue("@municipio", estudiante.Municipio);
                command.Parameters.AddWithValue("@nacionalidad", estudiante.Nacionalidad);
                command.Parameters.AddWithValue("@id_sexo", idSexo);
                command.Parameters.AddWithValue("@id_etnia", idEtnia);
                command.Parameters.AddWithValue("@id_lengua", idLengua);
                command.Parameters.AddWithValue("@id_discapacidad", idDiscapacidad);
                command.Parameters.AddWithValue("@id_tutor_estudiante", idTutorEstudiante);
                    
                if (command.ExecuteNonQuery() > 0)
                {
                    new DBConnection().CerrarConexion(conexion);
                    return new Errors { message = "Estudiante actualizado correctamente", status = true };
                }

                new DBConnection().CerrarConexion(conexion);
                return new Errors { message = "Error al actualizar estudiante", status = false };
            } catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return new Errors { message = "Error al actualizar estudiante", status = false };
            }
        }
        public Errors EliminarEstudiante(int id)
        {
            try
            {
                SqlConnection conexion = new DBConnection().AbrirConexion();
                string query = "EXEC sp_eliminar_estudiante @id_estudiante";

                int idEstudiante = new ExistingDate().ExistingDateId("id_estudiante", "estudiantes", "id_estudiante", id.ToString());

                if(idEstudiante == 0)
                {
                    return new Errors { message = "Estudiante no encontrado", status = false };
                }

                SqlCommand command = new(query, conexion);
                command.Parameters.AddWithValue("@id_estudiante", id);
                command.ExecuteNonQuery();

                new DBConnection().CerrarConexion(conexion);
                return new Errors { message = "Estudiante eliminado correctamente", status = true };
            } catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return new Errors { message = "Error al eliminar estudiante", status = false };
            }
        }
    }
}
