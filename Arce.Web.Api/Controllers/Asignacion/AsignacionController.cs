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

        [HttpGet]
        [Route("getListarAsignacion")]
        public async Task<IActionResult> ListarAsignacion(int? Asg_Id, DateTime? Fec_Ini, DateTime? Fec_Fin,
    string? Asg_Usr, string? Usr_Reg, string? Flg_Est, int? Asg_Usr_Cen_Cos)
        {
            var result = await _service.ListarAsignacion(Asg_Id ?? 0, Fec_Ini, Fec_Fin, Asg_Usr ?? "", Usr_Reg ?? "", Flg_Est ?? "", Asg_Usr_Cen_Cos ?? 0);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

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

        [HttpPost]
        [Route("postRegistrarAsignacionDetalle")]
        public async Task<IActionResult> RegistrarAsignacionDetalle([FromBody] AsignacionDetalleEntity valores)
        {   
            var result = await _service.RegistrarAsignacionDetalle(valores);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarAsignacionDetalle")]
        public async Task<IActionResult> ActualizarAsignacionDetalle([FromBody] AsignacionDetalleEntity valores)
        {
            var result = await _service.ActualizarAsignacionDetalle(valores);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarDetallesXAsignacion")]
        public async Task<IActionResult> ListarDetallesXAsignacion(int? Asg_Id)
        {
            var result = await _service.ListarDetallesXAsignacion(Asg_Id ?? 0);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarAsignacionDetalleModificar")]
        public async Task<IActionResult> ListarAsignacionDetalleModificar(int? Asg_Det_Id)
        {
            var result = await _service.ListarAsignacionDetalleModificar(Asg_Det_Id ?? 0);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarAsignacionModificar")]
        public async Task<IActionResult> ListarAsignacionModificar(int? Asg_Id)
        {
            var result = await _service.ListarAsignacionModificar(Asg_Id);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerStockReservadoAsignacion")]
        public async Task<IActionResult> ObtenerStockReservadoAsignacion(int? Asg_Usr_Cen_Cos, int? Asg_Det_Itm_Id)
        {
            var result = await _service.ObtenerStockReservadoAsignacion(Asg_Usr_Cen_Cos, Asg_Det_Itm_Id);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchEliminarAsignacionDetalle")]
        public async Task<IActionResult> EliminarAsignacionDetalle([FromBody] AsignacionDetalleEntity valores)
        {
            var result = await _service.EliminarAsignacionDetalle(valores);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchEliminarAsignacion")]
        public async Task<IActionResult> EliminarAsignacion([FromBody] AsignacionCabeceraEntity valores)
        {
            var result = await _service.EliminarAsignacion(valores);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchEliminarAsignacionDetalleTotal")]
        public async Task<IActionResult> EliminarAsignacionDetalleTotal([FromBody] AsignacionDetalleEntity valores)
        {
            var result = await _service.EliminarAsignacionDetalleTotal(valores);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getReporteAsignacionUsuario")]
        public async Task<IActionResult> ReporteAsignacionUsuario(string? Flg_Est, 
        string? Asg_Usr, string? Usr_Reg, int? Asg_Usr_Cen_Cos, int? Asg_Id, int? Asg_Det_Itm_Id,
        DateTime? Fec_Ini, DateTime? Fec_Fin)
        {
            var result = await _service.ReporteAsignacionUsuario(Flg_Est ?? "", Asg_Usr ?? "", Usr_Reg ?? "", Asg_Usr_Cen_Cos ?? 0, Asg_Id ?? 0, 
            Asg_Det_Itm_Id ?? 0, Fec_Ini, Fec_Fin);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerDatosCabeceraValeSalidaPDF")]
        public async Task<IActionResult> ObtenerDatosCabeceraValeSalidaPDF(int? Asg_Id)
        {
            var result = await _service.ObtenerDatosCabeceraValeSalidaPDF(Asg_Id ?? 0);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerDatosDetalleValeSalidaPDF")]
        public async Task<IActionResult> ObtenerDatosDetalleValeSalidaPDF(int? Asg_Id)
        {
            var result = await _service.ObtenerDatosDetalleValeSalidaPDF(Asg_Id ?? 0);

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
