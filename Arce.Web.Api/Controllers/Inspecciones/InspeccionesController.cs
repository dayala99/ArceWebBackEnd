using Arce.Web.Entity.Inspecciones;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Inspecciones
{
    [ApiController]
    [Route("api/[controller]")]
    public class InspeccionesController : ControllerBase
    {
        private readonly IInspeccionesService _inspeccionesService;

        public InspeccionesController(IInspeccionesService inspeccionesService)
        {
            _inspeccionesService = inspeccionesService;
        }

        [HttpGet]
        [Route("getConsultaDatosUsuario")]
        public async Task<IActionResult> ConsultarDatosUsuario(string? Usr_Cod)
        {
            var result = await _inspeccionesService.ConsultarDatosUsuario(Usr_Cod ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getSubEstacionesPorCliente")]
        public async Task<IActionResult> ListarSubEstacionesPorCliente(int Cliente_Id)
        {
            var result = await _inspeccionesService.ListarSubEstacionesPorCliente(Cliente_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarSubEstaciones")]
        public async Task<IActionResult> ListarSubEstaciones(int? Id, string? Nombre, int? Cliente_Id, string? Estado)
        {
            var result = await _inspeccionesService.ListarSubEstaciones(Id, Nombre, Cliente_Id, Estado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarClientes")]
        public async Task<IActionResult> ListarClientes()
        {
            var result = await _inspeccionesService.ListarClientes();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarMotivos")]
        public async Task<IActionResult> ListarMotivos()
        {
            var result = await _inspeccionesService.ListarMotivos();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarClimas")]
        public async Task<IActionResult> ListarClimas()
        {
            var result = await _inspeccionesService.ListarClimas();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarTareas")]
        public async Task<IActionResult> ListarTareas()
        {
            var result = await _inspeccionesService.ListarTareas();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarSubContratas")]
        public async Task<IActionResult> ListarSubContratas()
        {
            var result = await _inspeccionesService.ListarSubContratas();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarJefesArea")]
        public async Task<IActionResult> ListarJefesArea()
        {
            var result = await _inspeccionesService.ListarJefesArea();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
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

        [HttpPost]
        [Route("postInsertarMedioAmbiente")]
        public async Task<IActionResult> InsertarMedioAmbiente([FromBody] InsMedioAmbienteEntity valores)
        {
            var result = await _inspeccionesService.InsertarMedioAmbiente(valores);
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

        [HttpGet]
        [Route("getFiltrarMedioAmbiente")]
        public async Task<IActionResult> FiltrarMedioAmbiente(DateTime? Fecha_Desde, DateTime? Fecha_Hasta, string? Estado)
        {
            var result = await _inspeccionesService.FiltrarMedioAmbiente(Fecha_Desde, Fecha_Hasta, Estado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getMostrarMedioAmbiente")]
        public async Task<IActionResult> MostrarMedioAmbiente(int Medio_Ambiente_Id)
        {
            var result = await _inspeccionesService.MostrarMedioAmbiente(Medio_Ambiente_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPut]
        [Route("putActualizarMedioAmbiente")]
        public async Task<IActionResult> ActualizarMedioAmbiente([FromBody] ActualizarMedioAmbienteEntity valores)
        {
            var result = await _inspeccionesService.ActualizarMedioAmbiente(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarMedioAmbiente")]
        public async Task<IActionResult> EliminarMedioAmbiente(int Medio_Ambiente_Id, string? Usr_Mod)
        {
            var valores = new EliminarMedioAmbienteEntity
            {
                Medio_Ambiente_Id = Medio_Ambiente_Id,
                Usr_Mod = Usr_Mod
            };

            var result = await _inspeccionesService.EliminarMedioAmbiente(valores);
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