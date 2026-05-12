using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : ControllerBase
    {
        public readonly IBancoService _service;

        public BancoController(IBancoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarBanco")]
        public async Task<IActionResult> ListarBanco(int? Ban_Id, string? Ban_Des, string? Flg_Est)
        {
            var result = await _service.ListarBanco(Ban_Id ?? 0, Ban_Des ?? "", Flg_Est ?? "");

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarBanco")]
        public async Task<IActionResult> RegistrarBanco([FromBody] BancoEntity valores)
        {   
            var result = await _service.RegistrarBanco(valores);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarBanco")]
        public async Task<IActionResult> ActualizarBanco([FromBody] BancoEntity valores)
        {
            // FormaPagoEntity parametros = new FormaPagoEntity
            // {
            //     For_Pag_Id = valores.For_Pag_Id,
            //     For_Pag_Des = valores.For_Pag_Des,
            //     Flg_Est = valores.Flg_Est,
            //     Usr_Mod = valores.Usr_Mod,

            // };

            var result = await _service.ActualizarBanco(valores);

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
