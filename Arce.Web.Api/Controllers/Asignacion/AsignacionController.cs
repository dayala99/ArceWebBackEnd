using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionController : ControllerBase
    {
        public readonly IAsignacionService _service;

        public AsignacionController(IAsignacionService service)
        {
            _service = service;
        }

        // [HttpGet]
        // [Route("getListarBanco")]
        // public async Task<IActionResult> RegistrarAsignacion(int? Ban_Id, string? Ban_Des, string? Flg_Est)
        // {
        //     var result = await _service.RegistrarAsignacion(Ban_Id ?? 0, Ban_Des ?? "", Flg_Est ?? "");

        //     if (result!.Success)
        //     {
        //         result.CodeResult = StatusCodes.Status200OK;
        //         return Ok(result);
        //     }

        //     result.CodeResult = StatusCodes.Status400BadRequest;
        //     return BadRequest(result);
        // }

        [HttpPost]
        [Route("postRegistrarAsignacion")]
        public async Task<IActionResult> RegistrarAsignacion([FromBody] AsignacionCabeceraEntity valores)
        {   
            var result = await _service.RegistrarAsignacion(valores);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarAsignacion")]
        public async Task<IActionResult> ActualizarAsignacion([FromBody] AsignacionCabeceraEntity valores)
        {
            var result = await _service.ActualizarAsignacion(valores);

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
