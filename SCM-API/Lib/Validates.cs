namespace SCM_API.Lib
{
    public class Validates
    {
        public void NotNull<T>(T element)
        {
            if (element == null)
            {
                throw new Exception($"El {element} no puede ser null");
            }
        }
        public void String(string element, string campo)
        {
            if (string.IsNullOrEmpty(element))
            {
                throw new Exception($"{campo} debe ser una cadena de texto");
            }
        }
        public void Number(string element)
        {
            if(!System.Text.RegularExpressions.Regex.IsMatch(element, new ExpressionesRegulares().Number))
            {
                throw new Exception($"El {element} debe ser un número");
            }
        }
        public void Boolean(bool element)
        {
            if(bool.TryParse(element.ToString(), out _))
            {
               throw new Exception($"El {element} debe ser un booleano");
            }
        }
        public void Expression(string element, string expression, string campo, string formato)
        {
            if(!System.Text.RegularExpressions.Regex.IsMatch(element, expression))
            {
                throw new Exception($"{campo} no cumple con el formato {formato}");
            }
        } 
        public void Cedula(string element)
        {
            Expression(element, new ExpressionesRegulares().Cedula, "Cédula", "000-000000-0000X");
        }
        public void Fecha(string element)
        {
            Expression(element, new ExpressionesRegulares().Fecha, "Fecha", "YYYY-MM-DD");
        }
        public void Telefono(string element)
        {
            Expression(element, new ExpressionesRegulares().Telefono, "Teléfono", "0000-0000");
        }
    }
}
