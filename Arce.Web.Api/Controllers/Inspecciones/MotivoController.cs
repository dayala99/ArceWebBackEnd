using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Inspecciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotivoController : ControllerBase
    {
        private readonly IMotivoService _service;

        public MotivoController(IMotivoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarMotivo")]
        public async Task<IActionResult> ListarMotivo(int? Id, string? Nombre, string? Estado)
        {
            var result = await _service.ListarMotivo(Id, Nombre, Estado);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getConsultarDatosMotivo")]
        public async Task<IActionResult> ConsultarDatosMotivo(int? Motivo_Id)
        {
            var result = await _service.ConsultarDatosMotivo(Motivo_Id);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarMotivo")]
        public async Task<IActionResult> RegistrarMotivo([FromBody] MotivoEntity valores)
        {
            var parametros = new MotivoEntity()
            {
                Nombre = valores.Nombre,
                Usr_Reg = valores.Usr_Reg
            };

            var result = await _service.RegistrarMotivo(parametros);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarMotivo")]
        public async Task<IActionResult> ActualizarMotivo([FromBody] MotivoEntity valores)
        {
            var parametros = new MotivoEntity()
            {
                Id = valores.Id,
                Nombre = valores.Nombre,
                Estado = valores.Estado,
                Usr_Mod = valores.Usr_Mod
            };

            var result = await _service.ActualizarMotivo(parametros);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarMotivo/{Id}")]
        public async Task<IActionResult> EliminarMotivo(int? Id, string? Usr_Mod)
        {
            var result = await _service.EliminarMotivo(Id, Usr_Mod);

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
