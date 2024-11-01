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
                new Validates().NotNull(TutoresEstudiantes.Nombre);
                new Validates().NotNull(TutoresEstudiantes.Apellido);
                new Validates().String(TutoresEstudiantes.Nombre, "Nombres");
                new Validates().String(TutoresEstudiantes.Apellido, "Apellidos");
                new Validates().Cedula(TutoresEstudiantes.Cedula);
                new Validates().Telefono(TutoresEstudiantes.Telefono);

                return new Errors { message = "Tutor creado", status = true };
            }
            catch (Exception ex)
            {
                return new Errors { message = ex.Message, status = false };
            }
        }
    }
}
