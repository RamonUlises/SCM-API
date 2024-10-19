using SCM_API.Clase;

namespace SCM_API.Lib
{
    public class ValidateDatosAcademicos
    {
        public Errors ValidarDatosAcademicos(DatosAcademicosClass datosAcademicos)
        {
            try
            {
                new Validates().CodigoEstudiante(datosAcademicos.CodigoEstudiante);
                new Validates().FechaMatricula(datosAcademicos.FechaMatricula);
                new Validates().String(datosAcademicos.NivelEducativo, "Nivel Educativo");
                new Validates().Boolean(datosAcademicos.Repitente);
                new Validates().String(datosAcademicos.Modalidad, "Modalidad");
                new Validates().String(datosAcademicos.Grado, "Grado");
                new Validates().String(datosAcademicos.Seccion, "Seccion");
                new Validates().String(datosAcademicos.Turno, "Turno");
                new Validates().String(datosAcademicos.Centro, "Centro");
                new Validates().Number(datosAcademicos.IdEstudiante);

                return new Errors { message = "Datos Academicos creados", status = true };
            }
            catch (Exception ex)
            {
                return new Errors { message = ex.Message, status = false };
            }
        }
    }
}
