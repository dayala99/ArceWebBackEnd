using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        public readonly IAccesoService _service;

        public AccesoController(IAccesoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarAcceso")]
        public async Task<IActionResult> ListarAcceso(string? @Prf_Acc_Cod)
        {
            var result = await _service.ListarAcceso(Prf_Acc_Cod ?? "");

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarAcceso")]
        public async Task<IActionResult> RegistrarAcceso([FromBody] AccesoEntity valores)
        {   
            var result = await _service.RegistrarAcceso(valores);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarAcceso")]
        public async Task<IActionResult> EliminarAcceso([FromBody] AccesoEntity valores)
        {   
            var result = await _service.EliminarAcceso(valores);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }
    }
}
