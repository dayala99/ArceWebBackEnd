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

        [HttpGet]
        [Route("getListarStocksItems")]
        public async Task<IActionResult> ListarStocksItems(int? Usr_Cen_Cos_Id, int? Alm_Det_Itm_Id)
        {
            var result = await _service.ListarStocksItems(Usr_Cen_Cos_Id, Alm_Det_Itm_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarStockItemSalidaAnulacion")]
        public async Task<IActionResult> ActualizarStockItemSalidaAnulacion([FromBody] ItemEntity valores)
        {           
            var result = await _service.ActualizarStockItemSalidaAnulacion(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getReporteOCOS")]
        public async Task<IActionResult> ReporteOCOS(DateTime Fec_Ini, DateTime Fec_Fin, int? Ped_Id, int? Ord_Com_Id, int? Ped_Tip_Com,
        int? Mon_Id, string? Usr_Reg, int? Ped_Cen_Cos_Asg, int? Ord_Com_For_Pag, int? Ord_Com_Prv)
        {
            var result = await _service.ReporteOCOS(Fec_Ini, Fec_Fin, Ped_Id ?? 0, Ord_Com_Id ?? 0, Ped_Tip_Com ?? 0,
            Mon_Id ?? 0, Usr_Reg ?? "", Ped_Cen_Cos_Asg ?? 0, Ord_Com_For_Pag ?? 0, Ord_Com_Prv ?? 0);
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
