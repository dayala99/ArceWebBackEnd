using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Inspecciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class JefeController : ControllerBase
    {
        private readonly IJefeService _service;

        public JefeController(IJefeService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarJefe")]
        public async Task<IActionResult> ListarJefe(int? Id, string? Nombre, string? Dni, string? Estado, int? Cen_Cos_Id)
        {
            var result = await _service.ListarJefe(Id, Nombre, Dni, Estado, Cen_Cos_Id ?? 0);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getConsultarDatosJefe")]
        public async Task<IActionResult> ConsultarDatosJefe(int? Jefe_Id)
        {
            var result = await _service.ConsultarDatosJefe(Jefe_Id);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarJefe")]
        public async Task<IActionResult> RegistrarJefe([FromBody] JefeEntity valores)
        {
            JefeEntity parametros = new JefeEntity()
            {
                Nombre = valores.Nombre,
                Dni = valores.Dni,
                Cen_Cos_Id = valores.Cen_Cos_Id,
                Usr_Reg = valores.Usr_Reg
            };

            var result = await _service.RegistrarJefe(parametros);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarJefe")]
        public async Task<IActionResult> ActualizarJefe([FromBody] JefeEntity valores)
        {
            JefeEntity parametros = new JefeEntity()
            {
                Id = valores.Id,
                Nombre = valores.Nombre,
                Dni = valores.Dni,
                Cen_Cos_Id = valores.Cen_Cos_Id,
                Estado = valores.Estado,
                Usr_Mod = valores.Usr_Mod
            };

            var result = await _service.ActualizarJefe(parametros);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarJefe/{Id}")]
        public async Task<IActionResult> EliminarJefe(int? Id, string? Usr_Mod)
        {
            var result = await _service.EliminarJefe(Id, Usr_Mod);

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
