using Arce.Web.Api.Models.Inspecciones;
using Arce.Web.Entity.Inspecciones;
using Arce.Web.Service;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Arce.Web.Api.Controllers.WeReport;

[ApiController]
[Route("api/WeReport")]
public class WeReportController : ControllerBase
{
    private readonly IInspeccionesService _inspeccionesService;
    private readonly string _connectionString;

    public WeReportController(IInspeccionesService inspeccionesService, IConfiguration configuration)
    {
        _inspeccionesService = inspeccionesService;
        _connectionString = configuration.GetConnectionString("Connection")!;
    }
        [HttpGet]
        [Route("getSubEstacionesPorCliente")]
        public async Task<IActionResult> ListarSubEstacionesPorCliente(int Cliente_Id)
        {
            var result = await _inspeccionesService.ListarSubEstacionesPorCliente(Cliente_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postInsertarWeReport")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> InsertarWeReport([FromForm] RegistrarWeReportFormRequest valores)
        {
            var carpeta = @"C:\Inspecciones\We_Report";
            Directory.CreateDirectory(carpeta);

            var foto1Ubicaciones = await GuardarArchivosAsync(valores.Report_Foto1, carpeta, "Foto1");
            var foto2Ubicacion = await GuardarArchivoAsync(valores.Report_Foto2, carpeta, "Foto2");

            Console.WriteLine($"[WeReport][Controller][Insertar] Foto1 archivos: {foto1Ubicaciones.Count}");
            Console.WriteLine($"[WeReport][Controller][Insertar] Foto2 archivo: {(string.IsNullOrWhiteSpace(foto2Ubicacion) ? "ninguno" : foto2Ubicacion)}");

            var entidad = new WeReportEntity
            {
                Usr_Cod = valores.Usr_Cod,
                Report_Anonimo = NormalizarMarca(valores.Report_Anonimo),
                Reporte_Id = valores.Reporte_Id,
                Cen_Cos_Id = valores.Cen_Cos_Id,
                Cliente_Id = valores.Cliente_Id,
                Subestacion_Id = valores.Subestacion_Id,
                Report_Descripcion = valores.Report_Descripcion,
                Report_Foto1_Ubicacion = CombinarRutas(foto1Ubicaciones),
                Report_Acciones_Inmediata = valores.Report_Acciones_Inmediata,
                Report_Foto2_Ubicacion = foto2Ubicacion,
                Report_Acciones_Propuestas = valores.Report_Acciones_Propuestas,
                Report_Potencial = valores.Report_Potencial,
                Report_Aplica = NormalizarMarca(valores.Report_Aplica),
                Usr_Reg = valores.Usr_Reg,
                Fec_Reg = DateTime.Now,
                Estado = "A"
            };

            var result = await _inspeccionesService.InsertarWeReport(entidad);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postActualizarWeReport")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ActualizarWeReport([FromForm] ActualizarWeReportFormRequest valores)
        {
            var carpeta = @"C:\Inspecciones\We_Report";
            Directory.CreateDirectory(carpeta);

            var foto1Nuevas = await GuardarArchivosAsync(valores.Report_Foto1, carpeta, "Foto1");
            var foto1Existentes = NormalizarRutasExistentes(valores.Report_Foto1_Ubicacion, carpeta);
            var foto1Ubicacion = CombinarRutas(foto1Existentes.Concat(foto1Nuevas));

            var foto2Ubicacion = await GuardarArchivoAsync(valores.Report_Foto2, carpeta, "Foto2");
            if (string.IsNullOrWhiteSpace(foto2Ubicacion))
            {
                foto2Ubicacion = NormalizarRutaExistente(valores.Report_Foto2_Ubicacion, carpeta);
            }

            Console.WriteLine($"[WeReport][Controller][Actualizar] Foto1 nuevas: {foto1Nuevas.Count}");
            Console.WriteLine($"[WeReport][Controller][Actualizar] Foto1 existentes: {foto1Existentes.Count}");
            Console.WriteLine($"[WeReport][Controller][Actualizar] Foto2 archivo: {(string.IsNullOrWhiteSpace(foto2Ubicacion) ? "ninguno" : foto2Ubicacion)}");

            var entidad = new WeReportActualizarEntity
            {
                We_Report_Id = valores.We_Report_Id,
                Usr_Cod = valores.Usr_Cod,
                Report_Anonimo = NormalizarMarca(valores.Report_Anonimo),
                Reporte_Id = valores.Reporte_Id,
                Cen_Cos_Id = valores.Cen_Cos_Id,
                Cliente_Id = valores.Cliente_Id,
                Subestacion_Id = valores.Subestacion_Id,
                Report_Descripcion = valores.Report_Descripcion,
                Report_Foto1_Ubicacion = foto1Ubicacion,
                Report_Acciones_Inmediata = valores.Report_Acciones_Inmediata,
                Report_Foto2_Ubicacion = foto2Ubicacion,
                Report_Acciones_Propuestas = valores.Report_Acciones_Propuestas,
                Report_Potencial = valores.Report_Potencial,
                Report_Aplica = NormalizarMarca(valores.Report_Aplica),
                Usr_Mod = valores.Usr_Mod,
                Fec_Mod = DateTime.Now,
                Estado = "A"
            };

            var result = await _inspeccionesService.ActualizarWeReport(entidad);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }




        [HttpGet]
        [Route("getMostrarActualizarWeReport")]
        public async Task<IActionResult> MostrarActualizarWeReport(int We_Report_Id)
        {
            var result = await _inspeccionesService.MostrarActualizarWeReport(We_Report_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarWeReport")]
        public async Task<IActionResult> ActualizarWeReport([FromBody] WeReportActualizarEntity valores)
        {
            var result = await _inspeccionesService.ActualizarWeReport(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postEliminarWeReport")]
        public async Task<IActionResult> EliminarWeReport([FromBody] EliminarWeReportEntity valores)
        {
            var result = await _inspeccionesService.EliminarWeReport(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getArchivoWeReport")]
        public IActionResult GetArchivoWeReport(string rutaArchivo)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivo))
            {
                return BadRequest("La ruta del archivo es obligatoria");
            }

            var ruta = rutaArchivo.Trim();
            if (!Path.IsPathRooted(ruta))
            {
                ruta = Path.Combine(@"C:\Inspecciones\We_Report", ruta);
            }

            if (!System.IO.File.Exists(ruta))
            {
                return NotFound("El archivo no existe en disco");
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(ruta, out var mimeType))
            {
                mimeType = "application/octet-stream";
            }

            var fileBytes = System.IO.File.ReadAllBytes(ruta);
            return File(fileBytes, mimeType);
        }

        // NUEVO: listado simple (sin filtros) de subestaciones para el combo de We Report
        [HttpGet]
        [Route("getListarSubEstacionesReporte")]
        public async Task<IActionResult> ListarSubEstacionesReporte()
        {
            var result = await _inspeccionesService.ListarSubEstacionesReporte();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarClientes")]
        public async Task<IActionResult> ListarClientes()
        {
            var result = await _inspeccionesService.ListarClientes();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarMotivos")]
        public async Task<IActionResult> ListarMotivos()
        {
            var result = await _inspeccionesService.ListarMotivos();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarClimas")]
        public async Task<IActionResult> ListarClimas()
        {
            var result = await _inspeccionesService.ListarClimas();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarTareas")]
        public async Task<IActionResult> ListarTareas()
        {
            var result = await _inspeccionesService.ListarTareas();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarSubContratas")]
        public async Task<IActionResult> ListarSubContratas()
        {
            var result = await _inspeccionesService.ListarSubContratas();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarJefesArea")]
        public async Task<IActionResult> ListarJefesArea()
        {
            var result = await _inspeccionesService.ListarJefesArea();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarTiposReporte")]
        public async Task<IActionResult> ListarTiposReporte()
        {
            var result = await _inspeccionesService.ListarTiposReporte();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarSupervisoresResponsables")]
        public async Task<IActionResult> ListarSupervisoresResponsables()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            const string sql = @"
                SELECT t1.Usr_Cod, t1.Usr_Nom
                FROM Sg_Usuario t1
                JOIN Ins_Cargo t2 ON (t1.Usr_Crg = t2.Cargo_Id)
                WHERE t2.Cargo_Nombre LIKE 'SUPERVISOR%'
                ORDER BY t1.Usr_Nom";

            var datos = (await connection.QueryAsync<InsJefeAreaEntity>(sql)).ToList();
            return Ok(datos);
        }

        [HttpGet]
        [Route("getListarInspectoresCliente")]
        public async Task<IActionResult> ListarInspectoresCliente()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            const string sql = @"
                SELECT t1.Usr_Cod, t1.Usr_Nom
                FROM Sg_Usuario t1
                JOIN Ins_Cargo t2 ON (t1.Usr_Crg = t2.Cargo_Id)
                WHERE t2.Cargo_Nombre LIKE 'INSPECTOR%'
                ORDER BY t1.Usr_Nom";

            var datos = (await connection.QueryAsync<InsJefeAreaEntity>(sql)).ToList();
            return Ok(datos);
        }

        [HttpGet]
        [Route("getListarTiposRiesgo")]
        public async Task<IActionResult> ListarTiposRiesgo()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            const string sql = @"
                SELECT t1.Tipo_Riesgo_Id, t1.Tipo_Riesgo
                FROM Ins_Tipo_Riesgo t1
                ORDER BY t1.Tipo_Riesgo";

            var datos = (await connection.QueryAsync<TipoRiesgoComboDto>(sql)).ToList();
            return Ok(datos);
        }

[HttpGet]
        [Route("getFiltrarWeReport")]
        public async Task<IActionResult> FiltrarWeReport(DateTime? Fecha_Desde, DateTime? Fecha_Hasta, string? Estado)
        {
            var result = await _inspeccionesService.FiltrarWeReport(Fecha_Desde, Fecha_Hasta, Estado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        private static async Task<List<string>> GuardarArchivosAsync(IEnumerable<IFormFile>? archivos, string carpeta, string prefijo)
        {
            var rutas = new List<string>();

            if (archivos is null)
            {
                return rutas;
            }

            foreach (var archivo in archivos)
            {
                var ruta = await GuardarArchivoAsync(archivo, carpeta, prefijo);
                if (!string.IsNullOrWhiteSpace(ruta))
                {
                    rutas.Add(ruta);
                }
            }

            return rutas;
        }

        private static List<string> NormalizarRutasExistentes(IEnumerable<string>? rutasArchivos, string carpetaBase)
        {
            var rutas = new List<string>();

            if (rutasArchivos is null)
            {
                return rutas;
            }

            foreach (var rutaArchivo in rutasArchivos)
            {
                if (string.IsNullOrWhiteSpace(rutaArchivo))
                {
                    continue;
                }

                var ruta = rutaArchivo.Trim();
                if (!Path.IsPathRooted(ruta))
                {
                    ruta = Path.Combine(carpetaBase, ruta);
                }

                rutas.Add(ruta);
            }

            return rutas;
        }

        private static string CombinarRutas(IEnumerable<string>? rutasArchivos)
        {
            if (rutasArchivos is null)
            {
                return string.Empty;
            }

            var rutas = rutasArchivos
                .Select(ruta => (ruta ?? string.Empty).Trim())
                .Where(ruta => !string.IsNullOrWhiteSpace(ruta))
                .ToList();

            return rutas.Count > 0 ? string.Join(Environment.NewLine, rutas) : string.Empty;
        }

        private static async Task<string> GuardarArchivoAsync(IFormFile? archivo, string carpeta, string prefijo)
        {
            if (archivo is null || archivo.Length <= 0)
            {
                return string.Empty;
            }

            var extension = Path.GetExtension(archivo.FileName);
            if (string.IsNullOrWhiteSpace(extension))
            {
                extension = ".jpg";
            }

            var nombreArchivo = $"{prefijo}_{DateTime.Now:yyyyMMdd_HHmmssfff}_{Guid.NewGuid():N}{extension}";
            var rutaCompleta = Path.Combine(carpeta, nombreArchivo);

            await using var stream = new FileStream(rutaCompleta, FileMode.Create, FileAccess.Write, FileShare.None);
            await archivo.CopyToAsync(stream);
            return rutaCompleta;
        }

        private static string NormalizarRutaExistente(string? rutaArchivo, string carpetaBase)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivo))
            {
                return string.Empty;
            }

            var ruta = rutaArchivo.Trim();
            if (!Path.IsPathRooted(ruta))
            {
                ruta = Path.Combine(carpetaBase, ruta);
            }

            return ruta;
        }


        private static string NormalizarMarca(string? valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return "N";
            }

            var limpio = valor.Trim().ToUpperInvariant();
            return limpio.StartsWith("S") ? "S" : "N";
        }

        private sealed class TipoRiesgoComboDto
        {
            public int? Tipo_Riesgo_Id { get; set; }
            public string? Tipo_Riesgo { get; set; }
        }
}
