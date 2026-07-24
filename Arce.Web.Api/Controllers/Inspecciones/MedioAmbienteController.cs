using Arce.Web.Entity.Inspecciones;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Inspecciones;

[ApiController]
[Route("api/Inspecciones")]
public class MedioAmbienteController : ControllerBase
{
    private readonly IInspeccionesService _inspeccionesService;

    public MedioAmbienteController(IInspeccionesService inspeccionesService)
    {
        _inspeccionesService = inspeccionesService;
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
