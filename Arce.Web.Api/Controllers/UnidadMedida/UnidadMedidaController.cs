using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadMedidaController : ControllerBase
    {
        private readonly IUnidadMedidaService _service;

        public UnidadMedidaController(IUnidadMedidaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarUnidadMedida")]
        public async Task<IActionResult> ListarUnidadMedida(int? Uni_Med_Id, string? Uni_Med_Des, string? Flg_Est)
        {
            var result = await _service.ListarUnidadMedida(Uni_Med_Id ?? 0, Uni_Med_Des ?? "", Flg_Est ?? "");

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarUnidadMedida")]
        public async Task<IActionResult> RegistrarUnidadMedida([FromBody] UnidadMedidaEntity valores)
        {
            UnidadMedidaEntity parametros = new UnidadMedidaEntity()
            {
                Uni_Med_Des = valores.Uni_Med_Des,
                Uni_Med_Abr = valores.Uni_Med_Abr,
                Usr_Reg = valores.Usr_Reg
            };

            var result = await _service.RegistrarUnidadMedida(parametros);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("patchActualizarUnidadMedida")]
        public async Task<IActionResult> ActualizarUnidadMedida([FromBody] UnidadMedidaEntity valores)
        {
            UnidadMedidaEntity parametros = new UnidadMedidaEntity()
            {
                Uni_Med_Id = valores.Uni_Med_Id,
                Uni_Med_Des = valores.Uni_Med_Des,
                Uni_Med_Abr = valores.Uni_Med_Abr,
                Flg_Est = valores.Flg_Est,
                Usr_Mod = valores.Usr_Mod
            };

            var result = await _service.ActualizarUnidadMedida(parametros);

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
