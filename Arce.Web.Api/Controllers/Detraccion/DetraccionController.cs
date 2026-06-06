using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetraccionController : ControllerBase
    {
        public readonly IDetraccionService _service;

        public DetraccionController(IDetraccionService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarDetraccion")]
        public async Task<IActionResult> ListarDetraccion(int? Det_Id, string? Det_Des, string? Flg_Est)
        {
            var result = await _service.ListarDetraccion(Det_Id ?? 0, Det_Des ?? "", Flg_Est ?? "");

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarDetraccion")]
        public async Task<IActionResult> RegistrarDetraccion([FromBody] DetraccionEntity valores)
        {   
            var result = await _service.RegistrarDetraccion(valores);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarDetraccion")]
        public async Task<IActionResult> ActualizarDetraccion([FromBody] DetraccionEntity valores)
        {
            var result = await _service.ActualizarDetraccion(valores);

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
