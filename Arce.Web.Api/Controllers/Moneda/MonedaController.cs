using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedaController : ControllerBase
    {
        public readonly IMonedaService _service;

        public MonedaController(IMonedaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarMoneda")]
        public async Task<IActionResult> ListarMoneda(int? Mon_Id, string? Mon_Des, string? Flg_Est)
        {
            var result = await _service.ListarMoneda(Mon_Id ?? 0, Mon_Des ?? "", Flg_Est ?? "");

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarMoneda")]
        public async Task<IActionResult> RegistrarMoneda([FromBody] MonedaEntity valores)
        {   
            var result = await _service.RegistrarMoneda(valores);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarMoneda")]
        public async Task<IActionResult> ActualizarMoneda([FromBody] MonedaEntity valores)
        {
            var result = await _service.ActualizarMoneda(valores);

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
