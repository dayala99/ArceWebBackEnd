using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        public readonly IPerfilService _service;

        public PerfilController(IPerfilService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarPerfil")]
        public async Task<IActionResult> ListarPerfil(string? Prf_Cod, string? Prf_Des, string? Flg_Est)
        {
            var result = await _service.ListarPerfil(Prf_Cod, Prf_Des, Flg_Est);
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
