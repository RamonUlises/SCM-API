using Microsoft.AspNetCore.Mvc;
using SCM_API.Clase;
using SCM_API.Lib;

namespace SCM_API.Controllers
{
    [Route("/api/datos-academicos")]
    [ApiController]
    public class DatosAcademicos : Controller
    {
        [HttpGet]
        public IActionResult ObtenerDatosAcademicos()
        {
            var datosAcademicos = new Models.DatosAcademicos().ObtenerDatosAcademicos();

            if (datosAcademicos == null)
            {
                return NotFound();
            }

            return Ok(datosAcademicos);
        }

        [HttpGet("{codigo}")]
        public IActionResult ObtenerDatosAcademicos(string codigo)
        {
            var datosAcademicos = new Models.DatosAcademicos().ObtenerDatosAcademicosCodigo(codigo);

            if (datosAcademicos == null)
            {
                return NotFound(new { message = "Datos academicos no encontrados" });
            }

            return Ok(datosAcademicos);
        }

        
    }
}