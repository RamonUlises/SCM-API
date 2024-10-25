using Microsoft.AspNetCore.Mvc;
using SCM_API.Clase;
using SCM_API.Lib;
using SCM_API.Models;

namespace SCM_API.Controllers
{
    public class TutoresXEstudiantes
    {
        [Route("/api/tutores-estudiantes")]
        [ApiController]
        public class TutoresXEstudiante : Controller
        {
            [HttpGet]
            public IActionResult ObtenerTutoresEstudiantes ()
            {
                var tutor = new Models.TutoresEstudiantes().ObtenerTutoresEstudiantes();

                if (tutor == null)
                {
                    return NotFound();
                }

                return Ok(tutor);
            }

            [HttpGet("{id}")]
            public IActionResult ObtenerTutoresEstudiantes(int id)
            {
                var tutor = new Models.TutoresEstudiantes().ObtenerTutoresEstudiantes(id);

                if (tutor == null)
                {
                    return NotFound(new { message = "Estudiante no encontrado" });
                }

                return Ok(tutor);
            }
            [HttpPost]
            public IActionResult CrearTutorEstudiante([FromBody] TutoresXEstudiantesClass tutores)
            {
                try
                {

                    var res = new ValidateTutores().ValidarTutores(tutores);

                    if (res.status == false)
                    {
                        return BadRequest(new { res.message });
                    }

                    var result = new Models.TutoresEstudiantes().CrearTutoresEstudiantes(tutores);
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
            public IActionResult ActualizarTutoresEstudiantes(int id, [FromBody] TutoresXEstudiantesClass tutores)
            {
                try
                {
                    var res = new ValidateTutores().ValidarTutores(tutores);

                    if (res.status == false)
                    {
                        return BadRequest(new { res.message });
                    }

                    var result = new Models.TutoresEstudiantes().EditarTutoresEstudiantes(id, tutores);
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

            [HttpDelete("{id}")]
            public IActionResult EliminarTutoresEstudiantes(int id)
            {
                var result = new Models.TutoresEstudiantes().EliminarTutoresEstudiantes(id);

                if (result.status == false)
                {
                    return BadRequest(new { result.message });
                }

                return Ok(new { result.message });
            }
        

        }
    }
}
