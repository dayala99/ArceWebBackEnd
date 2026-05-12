using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        public readonly IItemService _service;

        public ItemController(IItemService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Route("getListarItem")]
        public async Task<IActionResult> ListarItem(int? Itm_Id, string? Itm_Des, int? Itm_Grp, string? Flg_Est)
        {
            var result = await _service.ListarItem(Itm_Id, Itm_Des, Itm_Grp, Flg_Est);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarItem")]
        public async Task<IActionResult> RegistrarItem([FromBody] ItemEntity valores)
        {
            ItemEntity parametros = new ItemEntity
            {
                Itm_Des = valores.Itm_Des,
                Itm_Grp = valores.Itm_Grp,
                Usr_Reg = valores.Usr_Reg
            };
            
            var result = await _service.RegistrarItem(parametros);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarItem")]
        public async Task<IActionResult> ActualizarItem([FromBody] ItemEntity valores)
        {
            ItemEntity parametros = new ItemEntity
            {
                Itm_Id = valores.Itm_Id,
                Itm_Des = valores.Itm_Des,
                Itm_Grp = valores.Itm_Grp,
                Flg_Est = valores.Flg_Est,
                Usr_Mod = valores.Usr_Mod
            };
            
            var result = await _service.ActualizarItem(parametros);
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
