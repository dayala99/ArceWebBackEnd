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

        [HttpPost]
        [Route("postRegistrarObservacionPlaneada")]
        [HttpPost]
        [Route("registrarObservacionPlaneada")]
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
    }
}