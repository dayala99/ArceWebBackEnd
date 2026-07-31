using Arce.Web.Data;
using Arce.Web.Entity.Inspecciones;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Inspecciones
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoRiesgoController : ControllerBase
    {
        private readonly ITipoRiesgoRepository _repository;

        public TipoRiesgoController(ITipoRiesgoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getListarTipoRiesgo")]
        public async Task<IActionResult> ListarTipoRiesgo([FromQuery] int? Id = 0, [FromQuery] string? Nombre = "", [FromQuery] string? Estado = "A")
        {
            var result = await _repository.ListarTipoRiesgo(Id, Nombre, Estado);
            return Ok(result);
        }

        [HttpGet]
        [Route("getConsultarDatosTipoRiesgo")]
        public async Task<IActionResult> ConsultarDatosTipoRiesgo([FromQuery(Name = "Tipo_Riesgo_Id")] int? Tipo_Riesgo_Id)
        {
            var result = await _repository.ConsultarDatosTipoRiesgo(Tipo_Riesgo_Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("postRegistrarTipoRiesgo")]
        public async Task<IActionResult> RegistrarTipoRiesgo([FromBody] TipoRiesgoEntity valores)
        {
            var result = await _repository.RegistrarTipoRiesgo(valores);

            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Tipo de riesgo registrado correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }

        [HttpPatch]
        [Route("patchActualizarTipoRiesgo")]
        public async Task<IActionResult> ActualizarTipoRiesgo([FromBody] TipoRiesgoEntity valores)
        {
            var result = await _repository.ActualizarTipoRiesgo(valores);

            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Tipo de riesgo actualizado correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }

        [HttpDelete]
        [Route("deleteEliminarTipoRiesgo/{Id}")]
        public async Task<IActionResult> EliminarTipoRiesgo(int? Id, [FromQuery] string? Usr_Mod)
        {
            var result = await _repository.EliminarTipoRiesgo(Id, Usr_Mod);

            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Tipo de riesgo eliminado correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }
    }
}
