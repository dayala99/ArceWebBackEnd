using Arce.Web.Api.Models.TJH2B;
using Arce.Web.Entity.TJH2B;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Arce.Web.Data;
using Arce.Web.Data.TJH2B;
using System.Collections.Generic;

namespace Arce.Web.Api.Controllers.TJH2B
{
    [ApiController]
    [Route("api/[controller]")]
    public class CotizacionTjh2bController : ControllerBase
    {
        private readonly ICotizacionTjh2bRepository _repository;

        public CotizacionTjh2bController(ICotizacionTjh2bRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getListarCotizacionTjh2b")]
        public async Task<IActionResult> ListarCotizacionTjh2b([FromQuery] string? Numero = "", [FromQuery] string? ClienteNombre = "", [FromQuery] string? Servicio = "", [FromQuery] string? Estado = "A", [FromQuery] string? EstadoCotizacion = "", [FromQuery] DateTime? FechaInicio = null, [FromQuery] DateTime? FechaFin = null)
        {
            var result = await _repository.ListarCotizacionTjh2b(Numero, ClienteNombre, Servicio, Estado, EstadoCotizacion, FechaInicio, FechaFin);
            return Ok(result);
        }

        [HttpGet]
        [Route("getConsultarDatosCotizacionTjh2b")]
        public async Task<IActionResult> ConsultarDatosCotizacionTjh2b([FromQuery(Name = "Cotizacion_Id")] int? Cotizacion_Id)
        {
            var result = await _repository.ConsultarDatosCotizacionTjh2b(Cotizacion_Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("postRegistrarCotizacionTjh2b")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> RegistrarCotizacionTjh2b([FromForm] RegistrarCotizacionTjh2bFormRequest valores)
        {
            var documentoPdf = await GuardarArchivosPdfAsync(valores.Cotizacion_DocumentoPDF_File, valores.Cotizacion_DocumentoPDF) ?? string.Empty;

            var entity = new CotizacionTjh2bEntity
            {
                Numero = valores.Cotizacion_Numero,
                Cliente_Id = valores.Cliente_Id,
                Servicio = valores.Cotizacion_Servicio,
                FechaIni = valores.Cotizacion_FechaIni,
                FechaFin = valores.Cotizacion_FechaFin,
                DocumentoPdf = documentoPdf,
                Cotizacion_UsuariosCorreo = valores.Cotizacion_UsuariosCorreo,
                Cotizacion_Alerta = valores.Cotizacion_Alerta,
                Usr_Reg = valores.Usr_Reg
            };

            var result = await _repository.RegistrarCotizacionTjh2b(entity);

            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Cotización registrada correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }

        [HttpPost]
        [Route("patchActualizarCotizacionTjh2b")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ActualizarCotizacionTjh2b([FromForm] ActualizarCotizacionTjh2bFormRequest valores)
        {
            var documentoPdf = await GuardarArchivosPdfAsync(valores.Cotizacion_DocumentoPDF_File, valores.Cotizacion_DocumentoPDF) ?? string.Empty;

            var entity = new CotizacionTjh2bEntity
            {
                Id = valores.Cotizacion_Id,
                Numero = valores.Cotizacion_Numero,
                Cliente_Id = valores.Cliente_Id,
                Servicio = valores.Cotizacion_Servicio,
                FechaIni = valores.Cotizacion_FechaIni,
                FechaFin = valores.Cotizacion_FechaFin,
                DocumentoPdf = documentoPdf,
                Cotizacion_UsuariosCorreo = valores.Cotizacion_UsuariosCorreo,
                Cotizacion_Alerta = valores.Cotizacion_Alerta,
                Usr_Mod = valores.Usr_Mod,
                Cotizacion_Estado = valores.Cotizacion_Estado,
                Estado = valores.Estado
            };

            var result = await _repository.ActualizarCotizacionTjh2b(entity);

            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Cotización actualizada correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }

        [HttpDelete]
        [Route("deleteEliminarCotizacionTjh2b/{Id}")]
        public async Task<IActionResult> EliminarCotizacionTjh2b(int? Id, [FromQuery] string? Usr_Mod)
        {
            var result = await _repository.EliminarCotizacionTjh2b(Id, Usr_Mod);

            if (result.Codigo == 0)
            {
                return Ok(new { Success = true, Message = "Cotización eliminada correctamente." });
            }

            return BadRequest(new { Success = false, Message = result.Mensaje });
        }

        // Sirve el documento PDF/adjunto de una cotización a partir de la ruta guardada
        // en Cotizacion_DocumentoPDF (ej: C:\Archivos\COT_20260731141247219_ccd8d99c9a844913bc1c0eae6b54458e.pdf).
        // Se valida que la ruta esté dentro de la carpeta permitida (RutaAlmacenamientoPdf)
        // para evitar que se pida cualquier archivo del servidor (path traversal).
        [HttpGet]
        [Route("getArchivoCotizacionTjh2b")]
        public IActionResult GetArchivoCotizacionTjh2b([FromQuery] string ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta))
            {
                return BadRequest("Debe indicar la ruta del documento.");
            }

            string rutaCompleta;
            string carpetaBase;

            try
            {
                rutaCompleta = Path.GetFullPath(ruta);
                carpetaBase = Path.GetFullPath(RutaAlmacenamientoPdf);
            }
            catch
            {
                return BadRequest("La ruta del documento no es válida.");
            }

            var dentroDeCarpetaPermitida = rutaCompleta.StartsWith(
                carpetaBase + Path.DirectorySeparatorChar,
                StringComparison.OrdinalIgnoreCase
            ) || string.Equals(rutaCompleta, carpetaBase, StringComparison.OrdinalIgnoreCase);

            if (!dentroDeCarpetaPermitida)
            {
                return BadRequest("Ruta de archivo no permitida.");
            }

            if (!System.IO.File.Exists(rutaCompleta))
            {
                return NotFound("El archivo no existe en disco.");
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(rutaCompleta, out var mimeType))
            {
                mimeType = "application/pdf";
            }

            var fileBytes = System.IO.File.ReadAllBytes(rutaCompleta);
            return File(fileBytes, mimeType);
        }

        // Ruta fija donde se guardan los PDF de cotizaciones.
        // Al abrir esta carpeta desde el explorador de Windows se pueden ver todos los documentos.
        private const string RutaAlmacenamientoPdf = @"C:\Archivos";

        private static async Task<string?> GuardarArchivosPdfAsync(IEnumerable<IFormFile>? archivos, string? rutasExistentes)
        {
            var rutas = new List<string>();

            foreach (var ruta in SepararRutasPdf(rutasExistentes))
            {
                if (!EsRutaTemporal(ruta))
                {
                    rutas.Add(ruta);
                }
            }

            if (archivos is not null)
            {
                foreach (var archivo in archivos)
                {
                    var rutaGuardada = await GuardarArchivoPdfAsync(archivo);
                    if (!string.IsNullOrWhiteSpace(rutaGuardada))
                    {
                        rutas.Add(rutaGuardada);
                    }
                }
            }

            return rutas.Count > 0 ? string.Join(" | ", rutas.Distinct(StringComparer.OrdinalIgnoreCase)) : string.Empty;
        }

        private static IEnumerable<string> SepararRutasPdf(string? valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                yield break;
            }

            var partes = valor.Split(new[] { '|', ';', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var parte in partes)
            {
                if (!string.IsNullOrWhiteSpace(parte))
                {
                    yield return parte.Trim();
                }
            }
        }

        private static bool EsRutaTemporal(string ruta)
        {
            return ruta.StartsWith("blob:", StringComparison.OrdinalIgnoreCase)
                || ruta.StartsWith("data:", StringComparison.OrdinalIgnoreCase);
        }

        private static async Task<string?> GuardarArchivoPdfAsync(IFormFile? archivo)
        {
            if (archivo is null || archivo.Length == 0)
            {
                return null;
            }

            var carpeta = RutaAlmacenamientoPdf;
            Directory.CreateDirectory(carpeta);

            var nombreOriginal = Path.GetFileName(archivo.FileName);
            if (string.IsNullOrWhiteSpace(nombreOriginal))
            {
                nombreOriginal = $"COT_{DateTime.Now:yyyyMMddHHmmssfff}.pdf";
            }

            var rutaCompleta = Path.Combine(carpeta, nombreOriginal);

            await using var stream = new FileStream(rutaCompleta, FileMode.Create, FileAccess.Write, FileShare.None);
            await archivo.CopyToAsync(stream);

            return rutaCompleta;
        }
    }
}
