using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Inspecciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubContrataController : ControllerBase
    {
        private readonly ISubContrataService _service;

        public SubContrataController(ISubContrataService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarSubContrata")]
        public async Task<IActionResult> ListarSubContrata([FromQuery] int? Id = 0, [FromQuery] string? Nombre = "", [FromQuery] string? Estado = "A")
        {
            var result = await _service.ListarSubContrata(Id ?? 0, Nombre ?? string.Empty, Estado ?? "A");

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getConsultarDatosSubContrata")]
        public async Task<IActionResult> ConsultarDatosSubContrata([FromQuery(Name = "SubContrata_Id")] int? SubContrata_Id)
        {
            var result = await _service.ConsultarDatosSubContrata(SubContrata_Id);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarSubContrata")]
        public async Task<IActionResult> RegistrarSubContrata([FromBody] SubContrataEntity valores)
        {
            var parametros = new SubContrataEntity()
            {
                Nombre = valores.Nombre,
                Usr_Reg = valores.Usr_Reg
            };

            var result = await _service.RegistrarSubContrata(parametros);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarSubContrata")]
        public async Task<IActionResult> ActualizarSubContrata([FromBody] SubContrataEntity valores)
        {
            var parametros = new SubContrataEntity()
            {
                Id = valores.Id,
                Nombre = valores.Nombre,
                Estado = valores.Estado,
                Usr_Mod = valores.Usr_Mod
            };

            var result = await _service.ActualizarSubContrata(parametros);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarSubContrata/{Id}")]
        public async Task<IActionResult> EliminarSubContrata(int? Id, [FromQuery] string? Usr_Mod)
        {
            var result = await _service.EliminarSubContrata(Id, Usr_Mod);

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
