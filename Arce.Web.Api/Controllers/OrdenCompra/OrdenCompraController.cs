using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenCompraController : ControllerBase
    {
        public readonly IOrdenCompraService _service;

        public OrdenCompraController(IOrdenCompraService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Route("getListarOrdenCompraActivo")]
        public async Task<IActionResult> ListarOrdenCompraActivo(int? Ord_Com_Id, string? Ord_Com_Prv, string? Flg_Est)
        {
            var result = await _service.ListarOrdenCompraActivo(Ord_Com_Id ?? 0, Ord_Com_Prv ?? "", Flg_Est ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        // [HttpPost]
        // [Route("postRegistrarOrdenCompra")]
        // public async Task<IActionResult> RegistrarOrdenCompra([FromBody] OrdenCompraEntity valores)
        // {            
        //     var result = await _service.RegistrarOrdenCompra(valores);
        //     if (result!.Success)
        //     {
        //         result.CodeResult = StatusCodes.Status200OK;
        //         return Ok(result);
        //     }

        //     result.CodeResult = StatusCodes.Status400BadRequest;
        //     return BadRequest(result);
        // }

        [HttpPost]
        [Route("postRegistrarOrdenCompra")]
        public async Task<IActionResult> RegistrarOrdenCompra([FromForm] OrdenCompraEntity valores, IFormFile archivo)
        {
            if (valores == null)
            {
                return BadRequest(new { Success = false, Message = "Datos incompletos" });
            }

            if (archivo != null && archivo.Length > 0)
            {
                var carpeta = Path.Combine(@"D:\Archivos");
                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                var nombreArchivo = $"{Path.GetFileName(archivo.FileName)}";
                var rutaCompleta = Path.Combine(carpeta, nombreArchivo);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                }

                valores.Ord_Com_Arc_Adj_Nom = archivo.FileName;
                valores.Ord_Com_Arc_Adj_Rut = rutaCompleta;
            }

            var result = await _service.RegistrarOrdenCompra(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarOdenCompra")]
        public async Task<IActionResult> ActualizarOdenCompra([FromBody] OrdenCompraEntity valores)
        {            
            var result = await _service.ActualizarOdenCompra(valores);
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
