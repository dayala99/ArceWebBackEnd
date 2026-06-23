using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Inspecciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClimaController : ControllerBase
    {
        private readonly IClimaService _service;

        public ClimaController(IClimaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarClima")]
        public async Task<IActionResult> ListarClima([FromQuery] int? Id = 0, [FromQuery] string? Nombre = "", [FromQuery] string? Estado = "A")
        {
            var result = await _service.ListarClima(Id ?? 0, Nombre ?? string.Empty, Estado ?? "A");

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getConsultarDatosClima")]
        public async Task<IActionResult> ConsultarDatosClima([FromQuery(Name = "Clima_Id")] int? Clima_Id)
        {
            var result = await _service.ConsultarDatosClima(Clima_Id);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarClima")]
        public async Task<IActionResult> RegistrarClima([FromBody] ClimaEntity valores)
        {
            var parametros = new ClimaEntity()
            {
                Nombre = valores.Nombre,
                Usr_Reg = valores.Usr_Reg
            };

            var result = await _service.RegistrarClima(parametros);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarClima")]
        public async Task<IActionResult> ActualizarClima([FromBody] ClimaEntity valores)
        {
            var parametros = new ClimaEntity()
            {
                Id = valores.Id,
                Nombre = valores.Nombre,
                Estado = valores.Estado,
                Usr_Mod = valores.Usr_Mod
            };

            var result = await _service.ActualizarClima(parametros);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarClima/{Id}")]
        public async Task<IActionResult> EliminarClima(int? Id, [FromQuery] string? Usr_Mod)
        {
            var result = await _service.EliminarClima(Id, Usr_Mod);

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
