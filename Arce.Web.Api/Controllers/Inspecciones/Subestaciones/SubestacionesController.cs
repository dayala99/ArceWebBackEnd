using Arce.Web.Entity.Inspecciones;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Inspecciones.Subestaciones
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubestacionesController : ControllerBase
    {
        private readonly ISubestacionesService _subestacionesService;

        public SubestacionesController(ISubestacionesService subestacionesService)
        {
            _subestacionesService = subestacionesService;
        }

        [HttpGet]
        [Route("getListarSubEstaciones")]
        public async Task<IActionResult> ListarSubEstaciones([FromQuery] int? Id, [FromQuery] string? Nombre, [FromQuery] int? Cliente_Id, [FromQuery] string? Estado)
        {
            var result = await _subestacionesService.ListarSubEstaciones(Id, Nombre, Cliente_Id, Estado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getConsultarEditarSubEstaciones")]
        public async Task<IActionResult> ConsultarEditarSubEstaciones([FromQuery(Name = "Subestacion_Id")] int? Subestacion_Id)
        {
            var result = await _subestacionesService.ConsultarEditarSubEstaciones(Subestacion_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postInsertarSubestaciones")]
        public async Task<IActionResult> PostInsertarSubestaciones([FromBody] SubEstacionEntity valores)
        {
            var parametros = new SubEstacionEntity
            {
                Subestacion_Nombre = valores.Subestacion_Nombre,
                Cliente_Id = valores.Cliente_Id,
                Usr_Reg = valores.Usr_Reg
            };

            var result = await _subestacionesService.RegistrarSubEstacion(parametros);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchEditarSubEstaciones")]
        public async Task<IActionResult> PatchEditarSubEstaciones([FromBody] SubEstacionEntity valores)
        {
            var parametros = new SubEstacionEntity
            {
                Subestacion_Id = valores.Subestacion_Id,
                Subestacion_Nombre = valores.Subestacion_Nombre,
                Cliente_Id = valores.Cliente_Id,
                Usr_Mod = valores.Usr_Mod,
                Estado = valores.Estado
            };

            var result = await _subestacionesService.ActualizarSubEstacion(parametros);
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
