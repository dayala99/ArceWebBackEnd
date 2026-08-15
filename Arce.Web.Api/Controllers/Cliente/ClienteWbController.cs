using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteWbController : ControllerBase
    {
        public readonly IClienteWbService _service;

        public ClienteWbController(IClienteWbService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarClienteWb")]
        public async Task<IActionResult> ListarCliente(int? Cli_Id, string? Cli_Nom, string? Cli_Ruc, string? Flg_Est)
        {
            var result = await _service.ListarCliente(Cli_Id ?? 0, Cli_Nom ?? "", Cli_Ruc ?? "", Flg_Est ?? "");

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
