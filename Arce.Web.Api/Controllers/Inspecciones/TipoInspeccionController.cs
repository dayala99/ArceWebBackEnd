using Arce.Web.Data;
using Arce.Web.Entity.Inspecciones;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Inspecciones
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoInspeccionController : ControllerBase
    {
        private readonly ITipoInspeccionRepository _repository;

        public TipoInspeccionController(ITipoInspeccionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getListarTipoInspeccion")]
        public async Task<IActionResult> ListarTipoInspeccion([FromQuery] int? Id = 0, [FromQuery] string? Nombre = "", [FromQuery] string? Estado = "A")
        {
            var result = await _repository.ListarTipoInspeccion(Id, Nombre, Estado);
            return Ok(result);
        }

        [HttpGet]
        [Route("getConsultarDatosTipoInspeccion")]
        public async Task<IActionResult> ConsultarDatosTipoInspeccion([FromQuery(Name = "Tipo_Id")] int? Tipo_Id)
        {
            var result = await _repository.ConsultarDatosTipoInspeccion(Tipo_Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("postRegistrarTipoInspeccion")]
        public async Task<IActionResult> RegistrarTipoInspeccion([FromBody] TipoInspeccionEntity valores)
        {
            var result = await _repository.RegistrarTipoInspeccion(valores);

            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Tipo de inspección registrado correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }

        [HttpPatch]
        [Route("patchActualizarTipoInspeccion")]
        public async Task<IActionResult> ActualizarTipoInspeccion([FromBody] TipoInspeccionEntity valores)
        {
            var result = await _repository.ActualizarTipoInspeccion(valores);

            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Tipo de inspección actualizado correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }

        [HttpDelete]
        [Route("deleteEliminarTipoInspeccion/{Id}")]
        public async Task<IActionResult> EliminarTipoInspeccion(int? Id, [FromQuery] string? Usr_Mod)
        {
            var result = await _repository.EliminarTipoInspeccion(Id, Usr_Mod);

            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Tipo de inspección eliminado correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }
    }
}
