using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCM_API.Models;

using SCM_API.Clase;
using SCM_API.Lib;

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

            if (traslados == null || traslados.Count == 0)
            {
                return BadRequest(new { message = "No se encontraron traslados" });
            }

            return Ok(traslados);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerTraslado(int id)
        {
            var traslado = new Traslados().ObtenerTraslado(id);

            if (traslado == null)
            {
                return BadRequest(new { message = "Traslado no encontrado" });
            }

            return Ok(traslado);
        }

        [HttpPost]
        public IActionResult CrearTraslado([FromBody] TrasladoClass traslado)
        {
            try
            {
                var res = new ValidateTraslados().ValidarTraslados(traslado);

                if (res.status == false)
                {
                    return BadRequest(new { res.message });
                }

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

        [HttpPut("{id}")]
        public IActionResult EditarTraslado(int id, [FromBody] TrasladoClass traslado)
        {
            try
            {
                var res = new ValidateTraslados().ValidarTraslados(traslado);

                if (res.status == false)
                {
                    return BadRequest(new { res.message });
                }

                var result = new Traslados().EditarTraslado(id, traslado); 
                if(result.status == false)
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

        [HttpDelete("{id}")]
        public IActionResult EliminarTraslado(int id)
        {
            try
            {
                var result = new Traslados().EliminarTraslado(id);

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
