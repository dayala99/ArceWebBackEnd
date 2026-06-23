using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Inspecciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ITareaService _service;

        public TareaController(ITareaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarTarea")]
        public async Task<IActionResult> ListarTarea([FromQuery] int? Id = 0, [FromQuery] string? Nombre = "", [FromQuery] string? Estado = "A")
        {
            var result = await _service.ListarTarea(Id, Nombre, Estado);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getConsultarDatosTarea")]
        public async Task<IActionResult> ConsultarDatosTarea([FromQuery(Name = "Tarea_Id")] int? Tarea_Id)
        {
            var result = await _service.ConsultarDatosTarea(Tarea_Id);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarTarea")]
        public async Task<IActionResult> RegistrarTarea([FromBody] TareaEntity valores)
        {
            var parametros = new TareaEntity()
            {
                Nombre = valores.Nombre,
                Usr_Reg = valores.Usr_Reg
            };

            var result = await _service.RegistrarTarea(parametros);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarTarea")]
        public async Task<IActionResult> ActualizarTarea([FromBody] TareaEntity valores)
        {
            var parametros = new TareaEntity()
            {
                Id = valores.Id,
                Nombre = valores.Nombre,
                Estado = valores.Estado,
                Usr_Mod = valores.Usr_Mod
            };

            var result = await _service.ActualizarTarea(parametros);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarTarea/{Id}")]
        public async Task<IActionResult> EliminarTarea(int? Id, [FromQuery] string? Usr_Mod)
        {
            var result = await _service.EliminarTarea(Id, Usr_Mod);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }
    }
}
