using Arce.Web.Data;
using Arce.Web.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Inspecciones
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _repository;

        public ClienteController(IClienteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getListarCliente")]
        public async Task<IActionResult> ListarCliente([FromQuery] int? Id, [FromQuery] string? Nombre, [FromQuery] string? Estado)
        {
            var result = await _repository.ListarCliente(Id, Nombre, Estado);
            return Ok(result);
        }

        [HttpGet]
        [Route("getConsultarDatosCliente")]
        public async Task<IActionResult> ConsultarDatosCliente([FromQuery(Name = "Cliente_Id")] int? Cliente_Id)
        {
            var result = await _repository.ConsultarDatosCliente(Cliente_Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("postRegistrarCliente")]
        public async Task<IActionResult> RegistrarCliente([FromBody] ClienteEntity valores)
        {
            var result = await _repository.RegistrarCliente(valores);

            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Cliente registrado correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }

        [HttpPatch]
        [Route("patchActualizarCliente")]
        public async Task<IActionResult> ActualizarCliente([FromBody] ClienteEntity valores)
        {
            var result = await _repository.ActualizarCliente(valores);

            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Cliente actualizado correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }

        [HttpDelete]
        [Route("deleteEliminarCliente/{Id}")]
        public async Task<IActionResult> EliminarCliente(int? Id, [FromQuery] string? Usr_Mod)
        {
            var result = await _repository.EliminarCliente(Id, Usr_Mod);

            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Cliente eliminado correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }
    }
}
