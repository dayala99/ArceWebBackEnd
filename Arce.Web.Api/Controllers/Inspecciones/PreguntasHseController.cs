using Arce.Web.Data;
using Arce.Web.Entity.Inspecciones;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Inspecciones
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreguntasHseController : ControllerBase
    {
        private readonly IPreguntasHseRepository _repository;

        public PreguntasHseController(IPreguntasHseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getListarPreguntasHse")]
        public async Task<IActionResult> ListarPreguntasHse([FromQuery(Name = "Pregunta_Id")] int? Pregunta_Id = null, [FromQuery(Name = "Pregunta_Nombre")] string? Pregunta_Nombre = "", [FromQuery] string? Estado = "A")
        {
            var result = await _repository.ListarPreguntasHse(Pregunta_Id, Pregunta_Nombre, Estado);
            return Ok(result);
        }

        [HttpGet]
        [Route("getListarPreguntasHseSinEstado")]
        public async Task<IActionResult> ListarPreguntasHseSinEstado()
        {
            var result = await _repository.ListarPreguntasHseSinEstado();
            return Ok(result);
        }

        [HttpGet]
        [Route("getConsultarDatosPreguntasHse")]
        public async Task<IActionResult> ConsultarDatosPreguntasHse([FromQuery(Name = "Pregunta_Id")] int? Pregunta_Id)
        {
            var result = await _repository.ConsultarDatosPreguntasHse(Pregunta_Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("postRegistrarPreguntasHse")]
        public async Task<IActionResult> RegistrarPreguntasHse([FromBody] PreguntasHseEntity valores)
        {
            var result = await _repository.RegistrarPreguntasHse(valores);
            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Pregunta HSE registrada correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }

        [HttpPatch]
        [Route("patchActualizarPreguntasHse")]
        public async Task<IActionResult> ActualizarPreguntasHse([FromBody] PreguntasHseEntity valores)
        {
            var result = await _repository.ActualizarPreguntasHse(valores);
            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Pregunta HSE actualizada correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }

        [HttpDelete]
        [Route("deleteEliminarPreguntasHse/{Pregunta_Id}")]
        public async Task<IActionResult> EliminarPreguntasHse(int? Pregunta_Id, [FromQuery] string? Usr_Mod)
        {
            var result = await _repository.EliminarPreguntasHse(Pregunta_Id, Usr_Mod);
            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Pregunta HSE eliminada correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }
    }
}
