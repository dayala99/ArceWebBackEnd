using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoItemController : ControllerBase
    {
        public readonly IGrupoItemService _service;

        public GrupoItemController(IGrupoItemService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarGrupoItem")]
        public async Task<IActionResult> ListarGrupoItem(int? Grp_Id, string? Grp_Des, string? Flg_Est)
        {
            var result = await _service.ListarGrupoItem(Grp_Id ?? 0, Grp_Des ?? "", Flg_Est ?? "");

            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarGrupoItem")]
        public async Task<IActionResult> RegistrarGrupoItem([FromBody] GrupoItemEntity valores)
        {
            GrupoItemEntity parametros = new GrupoItemEntity
            {
                Grp_Des = valores.Grp_Des,
                Usr_Reg = valores.Usr_Reg
            };

            var result = await _service.RegistrarGrupoItem(parametros);

            if (result.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarGrupoItem")]
        public async Task<IActionResult> ActualizarGrupoItem([FromBody] GrupoItemEntity valores)
        {
            GrupoItemEntity parametros = new GrupoItemEntity
            {
                Grp_Id = valores.Grp_Id,
                Grp_Des = valores.Grp_Des,
                Flg_Est = valores.Flg_Est,
                Usr_Mod = valores.Usr_Mod,

            };

            var result = await _service.ActualizarGrupoItem(parametros);

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
