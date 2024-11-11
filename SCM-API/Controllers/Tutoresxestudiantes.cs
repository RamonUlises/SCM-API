using Microsoft.AspNetCore.Mvc;
using SCM_API.Clase;
using SCM_API.Lib;

namespace SCM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tutoresxestudiantes : ControllerBase
    {
        [HttpGet]
        public IActionResult ObtenerTutor()
        {
            var tutor = new Models.Tutoresxestudiantes().ObtenerTutor();

            if (tutor == null)
            {
                return NotFound();
            }

            return Ok(tutor);
        }

    }
}
