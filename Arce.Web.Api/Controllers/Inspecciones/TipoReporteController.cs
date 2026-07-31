using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Inspecciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoReporteController : ControllerBase
    {
        private readonly ITipoReporteService _service;

        public TipoReporteController(ITipoReporteService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarTipoReporte")]
        public async Task<IActionResult> ListarTipoReporte(int? Reporte_Id, string? Reporte_Tipo, string? Estado)
        {
            var result = await _service.ListarTipoReporte(Reporte_Id, Reporte_Tipo, Estado);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarTipoReporte")]
        public async Task<IActionResult> RegistrarTipoReporte([FromBody] TipoReporteEntity valores)
        {
            var parametros = new TipoReporteEntity()
            {
                Reporte_Tipo = valores.Reporte_Tipo,
                Usr_Reg = valores.Usr_Reg
            };

            var result = await _service.RegistrarTipoReporte(parametros);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarTipoReporte")]
        public async Task<IActionResult> ActualizarTipoReporte([FromBody] TipoReporteEntity valores)
        {
            var parametros = new TipoReporteEntity()
            {
                Reporte_Id = valores.Reporte_Id,
                Reporte_Tipo = valores.Reporte_Tipo,
                Estado = valores.Estado,
                Usr_Mod = valores.Usr_Mod
            };

            var result = await _service.ActualizarTipoReporte(parametros);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarTipoReporte/{Tipo_Reporte_Id}")]
        public async Task<IActionResult> EliminarTipoReporte(int? Tipo_Reporte_Id, string? Usr_Mod)
        {
            var result = await _service.EliminarTipoReporte(Tipo_Reporte_Id, Usr_Mod);

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
