using Arce.Web.Data;
using Arce.Web.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Inspecciones
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteTjh2bController : ControllerBase
    {
        private readonly IClienteTjh2bRepository _repository;

        public ClienteTjh2bController(IClienteTjh2bRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getListarClienteTjh2b")]
        public async Task<IActionResult> ListarClienteTjh2b([FromQuery] int? Id = 0, [FromQuery] string? Nombre = "", [FromQuery] string? Estado = "A")
        {
            var result = await _repository.ListarClienteTjh2b(Id, Nombre, Estado);
            return Ok(result);
        }

        [HttpGet]
        [Route("getConsultarDatosClienteTjh2b")]
        public async Task<IActionResult> ConsultarDatosClienteTjh2b([FromQuery(Name = "Cliente_Id")] int? Cliente_Id)
        {
            var result = await _repository.ConsultarDatosClienteTjh2b(Cliente_Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("postRegistrarClienteTjh2b")]
        public async Task<IActionResult> RegistrarClienteTjh2b([FromBody] ClienteTjh2bEntity valores)
        {
            var result = await _repository.RegistrarClienteTjh2b(valores);

            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Cliente TJH2B registrado correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }

        [HttpPatch]
        [Route("patchActualizarClienteTjh2b")]
        public async Task<IActionResult> ActualizarClienteTjh2b([FromBody] ClienteTjh2bEntity valores)
        {
            var result = await _repository.ActualizarClienteTjh2b(valores);

            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Cliente TJH2B actualizado correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }

        [HttpDelete]
        [Route("deleteEliminarClienteTjh2b/{Id}")]
        public async Task<IActionResult> EliminarClienteTjh2b(int? Id, [FromQuery] string? Usr_Mod)
        {
            var result = await _repository.EliminarClienteTjh2b(Id, Usr_Mod);

            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Cliente TJH2B eliminado correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }
    }
}
