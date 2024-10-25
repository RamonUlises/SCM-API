using SCM_API.Clase;
using SCM_API.Models;

namespace SCM_API.Lib
{
    public class ValidateTutores
    {
        public Errors ValidarTutores(TutoresXEstudiantesClass TutoresEstudiantes)
        {
            try
            {
                new Validates().String(TutoresEstudiantes.Nombre, "Nombres");
                new Validates().String(TutoresEstudiantes.Apellido, "Apellidos");
                new Validates().Cedula(TutoresEstudiantes.Cedula);
                new Validates().Telefono(TutoresEstudiantes.Telefono);

                //si el id del tutor por estudiante existe entonces no se crea el tutor, si no existe se crea un nuevo tutor por estudiante
                var tutor = new Models.TutoresEstudiantes().ObtenerTutoresEstudiantes(TutoresEstudiantes.Id_tutor_x_estudiante);
                if (tutor != null)
                {
                    return new Errors { message = "Tutor ya existe", status = false };
                }

                return new Errors { message = "Tutor creado", status = true };


            }
            catch (Exception ex)
            {
                return new Errors { message = ex.Message, status = false };
            }
        }
    }
}
