using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCM_API.Models;

using SCM_API.Clase;

namespace SCM_API.Controllers
{
    [Route("api/traslado")]
    [ApiController]
    public class Traslado : Controller
    {
        [HttpGet]
        public IActionResult ObtenerTraslados()
        {
            var traslados = new Traslados().ObtenerTraslado();

            if (traslados == null)
            {
                return NotFound(new { message = "No se encontraron traslados" });
            }

            return Ok(traslados);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerTraslado(int id)
        {
            var traslado = new Traslados().ObtenerTraslado(id);

            if (traslado == null)
            {
                return NotFound(new { message = "Traslado no encontrado" });
            }

            return Ok(traslado);
        }

        [HttpPost]
        public IActionResult CrearTraslado([FromBody] TrasladoClass traslado)
        {
            try
            {
                var result = new Traslados().CrearTraslado(traslado);

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
    }
}
