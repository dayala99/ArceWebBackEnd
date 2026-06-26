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
        public async Task<IActionResult> ListarItem(string? Itm_Cod, string? Itm_Des, int? Itm_Grp, int? Itm_Sub_Grp, int? Itm_Det_Mat_Id,string? Flg_Est)
        {
            var result = await _service.ListarItem(Itm_Cod, Itm_Des, Itm_Grp, Itm_Sub_Grp, Itm_Det_Mat_Id, Flg_Est);
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
            var result = await _service.RegistrarItem(valores);
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
            var result = await _service.ActualizarItem(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarStockItem")]
        public async Task<IActionResult> ActualizarStockItem([FromBody] ItemEntity valores)
        {           
            var result = await _service.ActualizarStockItem(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarStockItemIngresoDirecto")]
        public async Task<IActionResult> ActualizarStockItemIngresoDirecto([FromBody] ItemEntity valores)
        {           
            var result = await _service.ActualizarStockItemIngresoDirecto(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarStockItemSalida")]
        public async Task<IActionResult> ActualizarStockItemSalida([FromBody] ItemEntity valores)
        {           
            var result = await _service.ActualizarStockItemSalida(valores);
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
