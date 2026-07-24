using Arce.Web.Entity.Inspecciones;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Prevencion;

[ApiController]
[Route("api/Prevencion")]
public class PrevencionController : ControllerBase
{
    private readonly IInspeccionesService _inspeccionesService;

    public PrevencionController(IInspeccionesService inspeccionesService)
    {
        _inspeccionesService = inspeccionesService;
    }
        // NUEVO: devuelve Cen_Cos_Des y DNI del jefe a partir de su Usr_Cod
        [HttpGet]
        [Route("getMostrarJefe")]
        public async Task<IActionResult> MostrarJefe(string Jefe_Cod)
        {
            var result = await _inspeccionesService.MostrarJefe(Jefe_Cod);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarObservacionesPlaneadas")]
        public async Task<IActionResult> ListarObservacionesPlaneadas()
        {
            var result = await _inspeccionesService.ListarObservacionesPlaneadas();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getConsultarEstadoObservaciones")]
        public async Task<IActionResult> ConsultarEstadoObservaciones(string Estado)
        {
            var result = await _inspeccionesService.ConsultarEstadoObservaciones(Estado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getFiltrarObservaciones")]
        public async Task<IActionResult> FiltrarObservaciones(DateTime Fecha_Desde, DateTime Fecha_Hasta, string Estado)
        {
            var result = await _inspeccionesService.FiltrarObservaciones(Fecha_Desde, Fecha_Hasta, Estado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getMostrarObservacionPlaneada")]
        public async Task<IActionResult> MostrarObservacionPlaneada(string Codigo_Obs)
        {
            var result = await _inspeccionesService.MostrarObservacionPlaneada(Codigo_Obs);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarObservacionPlaneada")]
        public async Task<IActionResult> RegistrarObservacionPlaneada([FromBody] ObservacionPlaneadaEntity valores)
        {
            var result = await _inspeccionesService.RegistrarObservacionPlaneada(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarObservacionPlaneada")]
        public async Task<IActionResult> ActualizarObservacionPlaneada([FromBody] ActualizarObservacionPlaneadaEntity valores)
        {
            var result = await _inspeccionesService.ActualizarObservacionPlaneada(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postEliminarObservacionPlaneada")]
        public async Task<IActionResult> EliminarObservacionPlaneada([FromBody] EliminarObservacionPlaneadaEntity valores)
        {
            var result = await _inspeccionesService.EliminarObservacionPlaneada(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }
        [HttpGet]
        [Route("getListarTiposInspeccion")]
        public async Task<IActionResult> ListarTiposInspeccion()
        {
            var result = await _inspeccionesService.ListarTiposInspeccion();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getFiltrarPrevencion")]
        public async Task<IActionResult> FiltrarPrevencion(DateTime Fecha_Desde, DateTime Fecha_Hasta, string Estado)
        {
            var result = await _inspeccionesService.FiltrarPrevencion(Fecha_Desde, Fecha_Hasta, Estado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getMostrarPrevencion")]
        public async Task<IActionResult> MostrarPrevencion(int Prevencion_Id)
        {
            var result = await _inspeccionesService.MostrarPrevencion(Prevencion_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postInsertarPrevencion")]
        public async Task<IActionResult> InsertarPrevencion([FromBody] InsPrevencionEntity valores)
        {
            var result = await _inspeccionesService.InsertarPrevencion(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPut]
        [Route("putActualizarPrevencion")]
        public async Task<IActionResult> ActualizarPrevencion([FromBody] ActualizarPrevencionEntity valores)
        {
            var result = await _inspeccionesService.ActualizarPrevencion(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarPrevencion")]
        public async Task<IActionResult> EliminarPrevencion(int Prevencion_Id, string Usr_Mod)
        {
            var valores = new EliminarPrevencionEntity { Prevencion_Id = Prevencion_Id, Usr_Mod = Usr_Mod };
            var result = await _inspeccionesService.EliminarPrevencion(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }
}
