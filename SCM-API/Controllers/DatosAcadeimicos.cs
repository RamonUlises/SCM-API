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

        //[HttpPost]
        //public IActionResult CrearDatosAcademicos([FromBody] DatosAcademicosClass datosAcademicos)
        //{
        //    try
        //    {
        //        var res = new ValidateDatosAcademicos().ValidarDatosAcademicos(datosAcademicos);

        //        if (res.status == false)
        //        {
        //            return BadRequest(new { res.message });
        //        }

        //        var result = new Models.DatosAcademicos().CrearDatosAcademicos(datosAcademicos);
        //        if (result.status == false)
        //        {
        //            return BadRequest(new { result.message });
        //        }

        //        return Ok(new { result.message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpPut("{id}")]
        //public IActionResult ActualizarDatosAcademicos(int id, [FromBody] DatosAcademicosClass datosAcademicos)
        //{
        //    try
        //    {
        //        var res = new ValidateDatosAcademicos().ValidarDatosAcademicos(datosAcademicos);

        //        if (res.status == false)
        //        {
        //            return BadRequest(new { res.message });
        //        }

        //        var result = new Models.DatosAcademicos().ActualizarDatosAcademicos(id, datosAcademicos);
        //        if (result.status == false)
        //        {
        //            return BadRequest(new { result.message });
        //        }

        //        return Ok(new { result.message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpDelete("{id}")]
        //public IActionResult EliminarDatosAcademicos(int id)
        //{
        //    var result = new Models.DatosAcademicos().EliminarDatosAcademicos(id);

        //    if (result.status == false)
        //    {
        //        return BadRequest(new { result.message });
        //    }

        //    return Ok(new { result.message });
        //}
    }
}