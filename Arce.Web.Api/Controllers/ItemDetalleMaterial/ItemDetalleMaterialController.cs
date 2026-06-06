using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemDetalleMaterialController : ControllerBase
    {
        public readonly IItemDetalleMaterialService _service;

        public ItemDetalleMaterialController(IItemDetalleMaterialService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarItemDetalleMaterial")]
        public async Task<IActionResult> ListarItemDetalleMaterial(string? Det_Mat_Cod, string? Det_Mat_Des, int? Grp_Id, int? Sub_Grp_Id, string? Flg_Est)
        {
            var result = await _service.ListarItemDetalleMaterial(Det_Mat_Cod ?? "", Det_Mat_Des ?? "", Grp_Id ?? 0, Sub_Grp_Id ?? 0, Flg_Est ?? "");

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarItemDetalleMaterial")]
        public async Task<IActionResult> RegistrarItemDetalleMaterial([FromBody] ItemDetalleMaterialEntity valores)
        {
            var result = await _service.RegistrarItemDetalleMaterial(valores);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarItemDetalleMaterial")]
        public async Task<IActionResult> ActualizarItemDetalleMaterial([FromBody] ItemDetalleMaterialEntity valores)
        {
            var result = await _service.ActualizarItemDetalleMaterial(valores);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarItemDetalleMaterialPorGrupoySubgrupo")]
        public async Task<IActionResult> ListarItemDetalleMaterialPorGrupoySubgrupo(int? Grp_Id, int? Sub_Grp_Id)
        {
            var result = await _service.ListarItemDetalleMaterialPorGrupoySubgrupo(Grp_Id ?? 0, Sub_Grp_Id ?? 0);

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
