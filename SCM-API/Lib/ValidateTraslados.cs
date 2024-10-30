using SCM_API.Clase;

namespace SCM_API.Lib
{
    public class ValidateTraslados
    {
        public Errors ValidarTraslados(TrasladoClass traslado)
        {
            try
            {
                new Validates().NotNull(traslado.IdEstudiante);
                new Validates().NotNull(traslado.FechaTraslado);
                new Validates().NotNull(traslado.MotivoTraslado);
                new Validates().NotNull(traslado.CodigoEstudiante);
                new Validates().NotNull(traslado.IdCentro);
                new Validates().NotNull(traslado.IdPeriodo);
                new Validates().Fecha(traslado.FechaTraslado);

                return new Errors { message = "Traslado creado", status = true };
            }
            catch (Exception ex)
            {
                return new Errors { message = ex.Message, status = false };
            }
        }
    }
}
