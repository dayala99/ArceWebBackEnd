using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenController : ControllerBase
    {
        public readonly IAlmacenService _service;

        public AlmacenController(IAlmacenService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarIngresoAlmacen")]
        public async Task<IActionResult> ListarIngresoAlmacen(int? Alm_Mov_Id, string? Alm_Tip_Ing, string? Flg_Est, string? Flg_Est_Apr)
        {
            var result = await _service.ListarIngresoAlmacen(Alm_Mov_Id ?? 0, Alm_Tip_Ing ?? "", Flg_Est ?? "", Flg_Est_Apr ?? "");

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarIngresoAlmacenModificar")]
        public async Task<IActionResult> ListarIngresoAlmacenModificar(int? Alm_Mov_Id)
        {
            var result = await _service.ListarIngresoAlmacenModificar(Alm_Mov_Id ?? 0);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarIngresoAlmacen")]
        public async Task<IActionResult> RegistrarIngresoAlmacen([FromBody] AlmacenEntity valores)
        {   
            var result = await _service.RegistrarIngresoAlmacen(valores);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarIngresoAlmacen")]
        public async Task<IActionResult> ActualizarIngresoAlmacen([FromBody] AlmacenEntity valores)
        {
            var result = await _service.ActualizarIngresoAlmacen(valores);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarIngresoAlmacenDetalle")]
        public async Task<IActionResult> RegistrarIngresoAlmacenDetalle([FromBody] AlmacenDetalleEntity valores)
        {   
            var result = await _service.RegistrarIngresoAlmacenDetalle(valores);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarIngresoAlmacenDetalleModificar")]
        public async Task<IActionResult> ListarIngresoAlmacenDetalleModificar(int? Alm_Mov_Id)
        {
            var result = await _service.ListarIngresoAlmacenDetalleModificar(Alm_Mov_Id ?? 0);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarIngresoAlmacenDetalle")]
        public async Task<IActionResult> ActualizarIngresoAlmacenDetalle([FromBody] AlmacenDetalleEntity valores)
        {
            var result = await _service.ActualizarIngresoAlmacenDetalle(valores);

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
