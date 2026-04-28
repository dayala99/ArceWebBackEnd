using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoServicioController : ControllerBase
    {
        private readonly ITipoServicioService _service;

        public TipoServicioController(ITipoServicioService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarTipoServicioActivo")]
        public async Task<IActionResult> ListarTipoServicioActivo(int? Tip_Ser_Id, string? Tip_Ser_Des, string? Flg_Est)
        {
            var result = await _service.ListarTipoServicioActivo(Tip_Ser_Id ?? 0, Tip_Ser_Des ?? "", Flg_Est ?? "");

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarTipoServicio")]
        public async Task<IActionResult> RegistrarTipoServicio(TipoServicioEntity valores)
        {
            TipoServicioEntity parametros = new TipoServicioEntity()
            {
                Tip_Ser_Des = valores.Tip_Ser_Des,
                Usr_Reg = valores.Usr_Reg
            };

            var result = await _service.RegistrarTipoServicio(parametros);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("patchActualizarTipoServicio")]
        public async Task<IActionResult> ActualizarTipoServicio(TipoServicioEntity valores)
        {
            TipoServicioEntity parametros = new TipoServicioEntity()
            {
                Tip_Ser_Id = valores.Tip_Ser_Id,
                Tip_Ser_Des = valores.Tip_Ser_Des,
                Flg_Est = valores.Flg_Est,
                Usr_Mod = valores.Usr_Mod
            };

            var result = await _service.ActualizarTipoServicio(parametros);

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
