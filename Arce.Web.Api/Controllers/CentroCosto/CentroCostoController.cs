using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentroCostoController : ControllerBase
    {
        public readonly ICentroCostoService _centroCostoService;

        public CentroCostoController(ICentroCostoService centroCostoService)
        {
            _centroCostoService = centroCostoService;
        }

        [HttpGet]
        [Route("getListarCentroCostoActivo")]
        public async Task<IActionResult> ListarCentroCostoActivo(int? Cen_Cos_Id, string? Cen_Cos_Des, string? Flg_Est)
        {
            var result = await _centroCostoService.ListarCentroCostoActivo(Cen_Cos_Id ?? 0, Cen_Cos_Des ?? "", Flg_Est ?? "");

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarCentroCosto")]
        public async Task<IActionResult> RegistrarCentroCosto(CentroCostoEntity valores)
        {
            CentroCostoEntity parametros = new CentroCostoEntity
            {
                Cen_Cos_Des = valores.Cen_Cos_Des,
                Usr_Reg = valores.Usr_Reg
            };

            var result = await _centroCostoService.RegistrarCentroCosto(parametros);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarCentroCosto")]
        public async Task<IActionResult> ActualizarCentroCosto(CentroCostoEntity valores)
        {
            CentroCostoEntity parametros = new CentroCostoEntity
            {
                Cen_Cos_Id = valores.Cen_Cos_Id,
                Cen_Cos_Des = valores.Cen_Cos_Des,
                Flg_Est = valores.Flg_Est,
                Usr_Mod = valores.Usr_Mod,
            };

            var result = await _centroCostoService.ActualizarCentroCosto(parametros);

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
