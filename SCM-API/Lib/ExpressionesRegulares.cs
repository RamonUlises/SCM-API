namespace SCM_API.Lib
{
    public class ExpressionesRegulares
    {
        public readonly string Cedula = @"^[0-9]{3}-[0-9]{6}-[0-9]{4}[A-Z]{1}$";
        public readonly string Fecha = @"[0-9]{4}-[0-9]{2}-[0-9]{2}$";
        public readonly string Telefono = @"^[0-9]{4}-[0-9]{4}$";
        public readonly string Number = @"^\d+(\.\d+)?$";
        public readonly string CodigoEstudiante = @"^[A-Z]{3,5}[0-9]{13,15}$";
    }
}
