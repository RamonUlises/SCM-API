using SCM_API.Clase;

namespace SCM_API.Lib
{
    public class ValidateDatosAcademicos
    {
        public Errors ValidarDatosAcademicos(DatosAcademicosClass datosAcademicos, bool edit)
        {
            try
            {
                if (!edit)
                {
                    new Validates().CodigoEstudiante(datosAcademicos.CodigoEstudiante);
                }
                new Validates().FechaMatricula(datosAcademicos.FechaMatricula);
                new Validates().String(datosAcademicos.NivelEducativo, "Nivel Educativo");
                new Validates().String(datosAcademicos.Modalidad, "Modalidad");
                new Validates().Number(datosAcademicos.Grado.ToString());
                new Validates().String(datosAcademicos.Seccion, "Seccion");
                new Validates().String(datosAcademicos.Turno, "Turno");
                new Validates().String(datosAcademicos.Centro, "Centro");

                return new Errors { message = "Datos Academicos creados", status = true };
            }
            catch (Exception ex)
            {
                return new Errors { message = ex.Message, status = false };
            }
        }
    }
}
