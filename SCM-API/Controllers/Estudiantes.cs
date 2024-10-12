using Microsoft.AspNetCore.Mvc;
using SCM_API.Clase;
using SCM_API.Lib;

namespace SCM_API.Controllers
{
    [Route("/api/estudiantes")]
    [ApiController]
    public class Estudiantes : Controller
    {
        [HttpGet]
        public IActionResult ObtenerEstudiante()
        {
            var estudiantes = new Models.Estudiantes().ObtenerEstudiantes();

            if (estudiantes == null)
            {
                return NotFound();
            }

            return Ok(estudiantes);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerEstudiante(int id)
        {
            var estudiante = new Models.Estudiantes().ObtenerEstudiante(id);

            if (estudiante == null)
            {
                return NotFound(new { message = "Estudiante no encontrado" });
            }

            return Ok(estudiante);
        }

        [HttpPost]
        public IActionResult CrearEstudiante([FromBody] EstudiantesClass estudiante)
        {
            try
            {
                var res = new ValidateEstudiantes().ValidarEstudiantes(estudiante);

                if(res.status == false)
                {
                    return BadRequest(new { res.message });
                }

                var result = new Models.Estudiantes().CrearEstudiante(estudiante);
                if (result.status == false)
                {
                    return BadRequest(new { result.message });
                }

                return Ok(new { result.message });
            } catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPut("{id}")]
        public IActionResult ActualizarEstudiante(int id, [FromBody] EstudiantesClass estudiante)
        {
            try
            {
                var res = new ValidateEstudiantes().ValidarEstudiantes(estudiante);

                if(res.status == false)
                {
                    return BadRequest(new { res.message });
                }

                var result = new Models.Estudiantes().EditarEstudiante(id, estudiante);
                if (result.status == false)
                {
                    return BadRequest(new { result.message });
                }

                return Ok(new { result.message });
            } catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarEstudiante(int id)
        {
            var result = new Models.Estudiantes().EliminarEstudiante(id);

            if (result.status == false)
            {
                return BadRequest(new { result.message });
            }

            return Ok(new { result.message });
        }
    }
}
