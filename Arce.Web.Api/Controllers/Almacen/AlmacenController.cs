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

        [HttpPost]
        [Route("postRegistrarTransferenciaAlmacen")]
        public async Task<IActionResult> RegistrarTransferenciaAlmacen([FromBody] AlmacenEntity valores)
        {
            var result = await _service.RegistrarTransferenciaAlmacen(valores);

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

        [HttpPost]
        [Route("postRegistrarIngresoAlmacenOrdenCompra")]
        public async Task<IActionResult> RegistrarIngresoAlmacenOrdenCompra([FromBody] AlmacenEntity valores)
        {   
            var result = await _service.RegistrarIngresoAlmacenOrdenCompra(valores);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarIngresoAlmacenDetalleOrdenCompra")]
        public async Task<IActionResult> ActualizarIngresoAlmacenDetalleOrdenCompra([FromBody] AlmacenDetalleEntity valores)
        {
            var result = await _service.ActualizarIngresoAlmacenDetalleOrdenCompra(valores);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarMotivoRechazoAlmacen")]
        public async Task<IActionResult> ActualizarMotivoRechazoAlmacen([FromBody] AlmacenEntity valores)
        {
            var result = await _service.ActualizarMotivoRechazoAlmacen(valores);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarIngresoSalidaAlmacenPorCentroCosto")]
        public async Task<IActionResult> ListarIngresoSalidaAlmacenPorCentroCosto(int? Alm_Det_Itm_Id)
        {
            var result = await _service.ListarIngresoSalidaAlmacenPorCentroCosto(Alm_Det_Itm_Id ?? 0);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getReporteListarSalidas")]
        public async Task<IActionResult> ReporteListarSalidas(DateTime? Fec_Ini, DateTime? Fec_Fin ,int? Alm_Det_Itm_Id)
        {
            var result = await _service.ReporteListarSalidas(Fec_Ini, Fec_Fin, Alm_Det_Itm_Id ?? 0);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getReporteIngresoSalidasAlmacen")]
        public async Task<IActionResult> ReporteIngresoSalidasAlmacen(string? Usr_Cod, DateTime? Fec_Ini, DateTime? Fec_Fin
        , int? Alm_Det_Cen_Cos_Id, int? Alm_Det_Prv_Id, int? Alm_Det_Itm_Id, int? Alm_Tip_Ing)
        {
            var result = await _service.ReporteIngresoSalidasAlmacen(Usr_Cod ?? "", Fec_Ini, Fec_Fin, Alm_Det_Cen_Cos_Id ?? 0, Alm_Det_Prv_Id ?? 0, Alm_Det_Itm_Id ?? 0, Alm_Tip_Ing ?? 0);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }
 
        [HttpGet]
        [Route("getListarTransferenciaAlmacen")]
        public async Task<IActionResult> ListarTransferenciaAlmacen(
        DateTime? Fec_Ini, DateTime? Fec_Fin , string? Alm_Sol_Dni, int? Alm_Cen_Cos, int? Alm_Destino,
        int? Alm_Tip_Ing, string? Alm_Usr_Apr, int? Alm_Mov_Ori
        )
        {
            var result = await _service.ListarTransferenciaAlmacen(
                Fec_Ini, Fec_Fin, Alm_Sol_Dni ?? "", Alm_Cen_Cos ?? 0, Alm_Destino ?? 0,
                Alm_Tip_Ing ?? 0, Alm_Usr_Apr ?? "", Alm_Mov_Ori ?? 0);
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
