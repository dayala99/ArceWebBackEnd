using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormaPagoController : ControllerBase
    {
        public readonly IFormaPagoService _formaPagoService;

        public FormaPagoController(IFormaPagoService formaPagoService)
        {
            _formaPagoService = formaPagoService;
        }

        [HttpGet]
        [Route("getListarFormaPagoActivo")]
        public async Task<IActionResult> ListarFormaPagoActivo(int? For_Pag_Id, string? For_Pag_Des, string? Flg_Est)
        {
            var result = await _formaPagoService.ListarFormaPagoActivo(For_Pag_Id ?? 0, For_Pag_Des ?? "", Flg_Est ?? "");

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("getRegistrarFormaPago")]
        public async Task<IActionResult> RegistrarFormaPago(FormaPagoEntity valores)
        {
            FormaPagoEntity parametros = new FormaPagoEntity
            {
                For_Pag_Des = valores.For_Pag_Des,
                Usr_Reg = valores.Usr_Reg
            };

            var result = await _formaPagoService.RegistrarFormaPago(parametros);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarFormaPago")]
        public async Task<IActionResult> ActualizarFormaPago(FormaPagoEntity valores)
        {
            FormaPagoEntity parametros = new FormaPagoEntity
            {
                For_Pag_Id = valores.For_Pag_Id,
                For_Pag_Des = valores.For_Pag_Des,
                Flg_Est = valores.Flg_Est,
                Usr_Mod = valores.Usr_Mod,

            };

            var result = await _formaPagoService.ActualizarFormaPago(parametros);

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
