using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubGrupoItemController : ControllerBase
    {
        public readonly ISubGrupoItemService _service;

        public SubGrupoItemController(ISubGrupoItemService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarSubGrupoItem")]
        public async Task<IActionResult> ListarSubGrupoItem(string? Sub_Grp_Cod, string? Sub_Grp_Des, string? Flg_Est)
        {
            var result = await _service.ListarSubGrupoItem(Sub_Grp_Cod ?? "", Sub_Grp_Des ?? "", Flg_Est ?? "");

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarSubGrupoItem")]
        public async Task<IActionResult> RegistrarSubGrupoItem([FromBody] SubGrupoItemEntity valores)
        {
            var result = await _service.RegistrarSubGrupoItem(valores);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarSubGrupoItem")]
        public async Task<IActionResult> ActualizarSubGrupoItem([FromBody] SubGrupoItemEntity valores)
        {
            var result = await _service.ActualizarSubGrupoItem(valores);

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarSubGrupoItemPorGrpId")]
        public async Task<IActionResult> ListarSubGrupoItemPorGrpId(int? Grp_Id)
        {
            var result = await _service.ListarSubGrupoItemPorGrpId(Grp_Id ?? 0);

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
