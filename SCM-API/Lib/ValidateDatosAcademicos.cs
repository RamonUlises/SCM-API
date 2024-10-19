using SCM.API.Clase;

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
                new validates().string(datosAcademicos.NivelEducativo, "Nivel Educativo");
                new validates().Boolean(datosAcademicos.repitente);
                new validates().string(datosAcademicos.Grado, "Grado");
                new validates().string(datosAcademicos.Seccion, "Seccion");
                new validates().string(datosAcademicos.Turno, "Turno");
                new validates().string(datosAcademicos.Centro, "Centro");
                new validates().IdEstudiante(datosAcademicos.IdEstudiante);
                new validates().string(datosAcademicos.Modalidad, "Modalidad");

                return new Errors { message = "Datos Academicos creados", status = true };
            }
            catch (Exception ex)
            {
                return new Errors { message = ex.Message, status = false };
            }
        }
    }
}
