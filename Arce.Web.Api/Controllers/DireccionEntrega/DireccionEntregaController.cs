using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionEntregaController : ControllerBase
    {
        public readonly IDireccionEntregaService _service;

        public DireccionEntregaController(IDireccionEntregaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarDireccionEntregaActivo")]
        public async Task<IActionResult> ListarDireccionEntregaActivo(int? Dir_Id, string? Dir_Des, string? Flg_Est)
        {
            var result = await _service.ListarDireccionEntregaActivo(Dir_Id ?? 0, Dir_Des ?? "", Flg_Est ?? "");

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarDireccionEntrega")]
        public async Task<IActionResult> RegistrarDireccionEntrega([FromBody] DireccionEntregaEntity valores)
        {
            var result = await _service.RegistrarDireccionEntrega(valores);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarDireccionEntrega")]
        public async Task<IActionResult> ActualizarDireccionEntrega([FromBody] DireccionEntregaEntity valores)
        {
            var result = await _service.ActualizarDireccionEntrega(valores);

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
