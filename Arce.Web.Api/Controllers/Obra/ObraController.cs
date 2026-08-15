using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObraController : ControllerBase
    {
        public readonly IObraService _service;

        public ObraController(IObraService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarObra")]
        public async Task<IActionResult> ListarObra(
        int? Obr_Id, int? Obr_Cen_Cos, string? Obr_Nom, string? Obr_Ubi, string? Obr_Are,
        int? Obr_Cli_Id, string? Flg_Est, string? Obr_Rsp
        )
        {
            var result = await _service.ListarObra(
                Obr_Id ?? 0, Obr_Cen_Cos ?? 0, Obr_Nom ?? "", Obr_Ubi ?? "", Obr_Are ?? "",
                Obr_Cli_Id ?? 0, Flg_Est ?? "", Obr_Rsp ?? ""
                );

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarObra")]
        public async Task<IActionResult> RegistrarObra([FromBody] ObraEntity valores)
        {   
            var result = await _service.RegistrarObra(valores);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarObra")]
        public async Task<IActionResult> ActualizarObra([FromBody] ObraEntity valores)
        {   
            var result = await _service.ActualizarObra(valores);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getCargarObraModificar")]
        public async Task<IActionResult> CargarObraModificar(int? Obr_Id)
        {
            var result = await _service.CargarObraModificar(Obr_Id ?? 0);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarFechaInicioObra")]
        public async Task<IActionResult> ActualizarFechaInicioObra([FromBody] ObraEntity valores)
        {   
            var result = await _service.ActualizarFechaInicioObra(valores);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarFechaFinObra")]
        public async Task<IActionResult> ActualizarFechaFinObra([FromBody] ObraEntity valores)
        {   
            var result = await _service.ActualizarFechaFinObra(valores);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarFechaCierreObra")]
        public async Task<IActionResult> ActualizarFechaCierreObra([FromBody] ObraEntity valores)
        {   
            var result = await _service.ActualizarFechaCierreObra(valores);

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
