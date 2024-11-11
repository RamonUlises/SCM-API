using SCM_API.Clase;

namespace SCM_API.Lib
{
    public class ValidateTutoresxEstudiantes
    {
        public Errors ValidarTutores(EstudiantesClass estudiante)
        {
            try
            {
                new Validates().String(estudiante.Nombres, "Nombres");
                new Validates().String(estudiante.Apellidos, "Apellidos");
                new Validates().Cedula(estudiante.Cedula);
                new Validates().Telefono(estudiante.Telefono);
                return new Errors { message = "Tutor del estudiante creado", status = true };
            }
            catch (Exception ex)
            {
                return new Errors { message = ex.Message, status = false };
            }
        }
    }
}
