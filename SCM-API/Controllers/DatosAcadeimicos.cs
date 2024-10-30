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
            List<DatosAcademicosClass>? datosAcademicos = new Models.DatosAcademicos().ObtenerDatosAcademicos();

            if (datosAcademicos == null || datosAcademicos.Count == 0)
            {
                return BadRequest(new { message = "Sin datos académicos registrados"});
            }

            return Ok(datosAcademicos);
        }

        [HttpGet("{codigo}")]
        public IActionResult ObtenerDatosAcademicos(string codigo)
        {
            var datosAcademicos = new Models.DatosAcademicos().ObtenerDatosAcademicosCodigo(codigo);

            if (datosAcademicos == null)
            {
                return BadRequest(new { message = "Datos academicos no encontrados" });
            }

            return Ok(datosAcademicos);
        }

        [HttpPost]
        public IActionResult CrearDatosAcademicos([FromBody] DatosAcademicosClass datosAcademicos)
        {
            try
            {
                var res = new ValidateDatosAcademicos().ValidarDatosAcademicos(datosAcademicos, false);

                if (res.status == false)
                {
                    return BadRequest(new { res.message });
                }

                var result = new Models.DatosAcademicos().CrearDatosAcademicos(datosAcademicos);
                if (result.status == false)
                {
                    return BadRequest(new { result.message });
                }

                return Ok(new { result.message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{codigo}")]
        public IActionResult ActualizarDatosAcademicos(string codigo, [FromBody] DatosAcademicosClass datosAcademicos)
        {
            try
            {
                var res = new ValidateDatosAcademicos().ValidarDatosAcademicos(datosAcademicos, true);

                if (res.status == false)
                {
                    return BadRequest(new { res.message });
                }

                var result = new Models.DatosAcademicos().EditarDatosAcademicos(codigo, datosAcademicos);
                if (result.status == false)
                {
                    return BadRequest(new { result.message });
                }

                return Ok(new { result.message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{codigo}")]
        public IActionResult EliminarDatosAcademicos(string codigo)
        {
            var result = new Models.DatosAcademicos().EliminarDatosAcademicos(codigo);

            if (result.status == false)
            {
                return BadRequest(new { result.message });
            }

            return Ok(new { result.message });
        }
    }
}