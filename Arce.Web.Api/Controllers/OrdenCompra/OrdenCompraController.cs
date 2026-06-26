using Arce.Web.Entity;
using Arce.Web.Service;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

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

        [HttpGet]
        [Route("getListarOrdenCompraModificar")]
        public async Task<IActionResult> ListarOrdenCompraModificar(int? Ord_Com_Id)
        {
            var result = await _service.ListarOrdenCompraModificar(Ord_Com_Id ?? 0);
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

            NormalizarDecimalesOrdenCompra(valores);

            if (archivo != null && archivo.Length > 0)
            {
                var carpeta = Path.Combine(@"C:\Archivos");
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
        public async Task<IActionResult> ActualizarOdenCompra([FromForm] OrdenCompraEntity valores, IFormFile archivo)
        {   
            if (valores == null)
            {
                return BadRequest(new { Success = false, Message = "Datos incompletos" });
            }

            NormalizarDecimalesOrdenCompra(valores);

            if (archivo != null && archivo.Length > 0)
            {
                var carpeta = Path.Combine(@"C:\Archivos");
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

            var result = await _service.ActualizarOdenCompra(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet("getArchivoOrdenCompra")]
        public IActionResult GetArchivoPedido(string nombreArchivo)
        {
            var carpeta = @"C:\Archivos";
            var ruta = Path.Combine(carpeta, nombreArchivo);

            if (!System.IO.File.Exists(ruta))
                return NotFound("El archivo no existe en disco");

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(ruta, out var mimeType))
            {
                mimeType = "application/octet-stream";
            }

            var fileBytes = System.IO.File.ReadAllBytes(ruta);
            return File(fileBytes, mimeType);
        }

        [HttpGet]
        [Route("getListarOrdenCompraPendienteAlmacen")]
        public async Task<IActionResult> ListarOrdenCompraPendienteAlmacen(int? Ord_Com_Id, string? Ord_Com_Prv, string? Flg_Est)
        {
            var result = await _service.ListarOrdenCompraPendienteAlmacen(Ord_Com_Id ?? 0, Ord_Com_Prv ?? "", Flg_Est ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarCabeceraIngresoAlmacen")]
        public async Task<IActionResult> ListarCabeceraIngresoAlmacen(int? Ord_Com_Id)
        {
            var result = await _service.ListarCabeceraIngresoAlmacen(Ord_Com_Id ?? 0);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchCambiarEstadoOrdenCompra")]
        public async Task<IActionResult> CambiarEstadoOrdenCompra([FromBody] OrdenCompraEntity valores)
        {   
            var result = await _service.CambiarEstadoOrdenCompra(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        private void NormalizarDecimalesOrdenCompra(OrdenCompraEntity valores)
        {
            valores.Ord_Com_Sub_Tot = LeerDecimalForm("Ord_Com_Sub_Tot", valores.Ord_Com_Sub_Tot);
            valores.Ord_Com_Igv = LeerDecimalForm("Ord_Com_Igv", valores.Ord_Com_Igv);
            valores.Ord_Com_Tot = LeerDecimalForm("Ord_Com_Tot", valores.Ord_Com_Tot);
            valores.Ord_Com_Det_Mon = LeerDecimalForm("Ord_Com_Det_Mon", valores.Ord_Com_Det_Mon);
            valores.Igv_Por = LeerDecimalForm("Igv_Por", valores.Igv_Por);

            if (valores.Igv_Por.HasValue && valores.Igv_Por.Value > 100)
            {
                valores.Igv_Por = Math.Round(valores.Igv_Por.Value / 100, 2);
            }
        }

        private decimal? LeerDecimalForm(string key, decimal? fallback)
        {
            if (!Request.HasFormContentType || !Request.Form.TryGetValue(key, out var value))
            {
                return fallback;
            }

            var rawValue = value.ToString().Trim();
            if (string.IsNullOrWhiteSpace(rawValue))
            {
                return fallback;
            }

            var normalizedValue = NormalizarTextoDecimal(rawValue);
            return decimal.TryParse(normalizedValue, NumberStyles.Number, CultureInfo.InvariantCulture, out var parsedValue)
                ? Math.Round(parsedValue, 2)
                : fallback;
        }

        private static string NormalizarTextoDecimal(string value)
        {
            var rawValue = value.Trim();
            var lastDot = rawValue.LastIndexOf('.');
            var lastComma = rawValue.LastIndexOf(',');

            if (lastDot >= 0 && lastComma >= 0)
            {
                if (lastDot > lastComma)
                {
                    return rawValue.Replace(",", "");
                }

                return rawValue.Replace(".", "").Replace(",", ".");
            }

            if (lastComma >= 0)
            {
                return rawValue.Replace(".", "").Replace(",", ".");
            }

            return rawValue.Replace(",", "");
        }
    }
}
