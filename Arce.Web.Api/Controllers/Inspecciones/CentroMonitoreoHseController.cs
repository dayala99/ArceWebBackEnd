using Arce.Web.Api.Models.Inspecciones;
using Arce.Web.Entity.Inspecciones;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Arce.Web.Api.Controllers.Inspecciones;

[ApiController]
[Route("api/[controller]")]
public class CentroMonitoreoHseController : ControllerBase
{
    private readonly string _connectionString;

    public CentroMonitoreoHseController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    [HttpGet]
    [Route("getListarCentroMonitoreoHse")]
    public async Task<IActionResult> ListarCentroMonitoreoHse([FromQuery] int? Id = 0, [FromQuery] string? Estado = "A")
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Centro_Monitoreo_Id", Id ?? 0);
        parametros.Add("@Estado", NormalizarEstado(Estado));

        var filas = await connection.QueryAsync("[dbo].[SP_Filtrar_Centro_Monitoreo_HSE]", parametros, commandType: CommandType.StoredProcedure);
        var resultado = filas.Select(MapearFila).ToList();
        return Ok(resultado);
    }

    // Alimenta la tabla de Centro de Monitoreo HSE (columnas: Nro, Inspector, Supervisor, Cliente, Revisión, Puntaje).
    [HttpGet]
    [Route("getFiltrarCentroMonitoreoHse")]
    public async Task<IActionResult> FiltrarCentroMonitoreoHse([FromQuery] DateTime? Fecha_Desde, [FromQuery] DateTime? Fecha_Hasta, [FromQuery] string? Estado = "A")
    {
        if (!Fecha_Desde.HasValue || !Fecha_Hasta.HasValue)
        {
            return Ok(new List<CentroHseListadoEntity>());
        }

        var resultado = new List<CentroHseListadoEntity>();

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new SqlCommand("[dbo].[SP_Filtrar_Centro_HSE]", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@Fecha_Desde", Fecha_Desde.Value);
        command.Parameters.AddWithValue("@Fecha_Hasta", Fecha_Hasta.Value);
        command.Parameters.AddWithValue("@Estado", NormalizarEstado(Estado));

        // El SP selecciona t4.Usr_Nom (Inspector) y t3.Usr_Nom (Supervisor) sin alias,
        // por lo que ambas columnas llegan con el mismo nombre "Usr_Nom". Dapper/dynamic
        // busca por nombre y solo encontraría la primera coincidencia, así que aquí se lee
        // el resultado por posición (tal como las selecciona el SP, en este orden):
        // 0 Centro_HSE_Id, 1 Centro_HSE_Cod, 2 Usr_Nom (Inspector), 3 Usr_Nom (Supervisor),
        // 4 Cliente_Nombre, 5 Centro_Revision, 6 Centro_Puntaje.
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            resultado.Add(new CentroHseListadoEntity
            {
                Centro_HSE_Id = LeerInt(reader, 0),
                Centro_HSE_Cod = LeerTexto(reader, 1),
                Usr_Inspector = LeerTexto(reader, 2),
                Usr_Supervisor = LeerTexto(reader, 3),
                Cliente_Nombre = LeerTexto(reader, 4),
                Centro_Revision = LeerTexto(reader, 5),
                Centro_Puntaje = LeerTexto(reader, 6),
            });
        }

        return Ok(resultado);
    }

    [HttpGet]
    [Route("getMostrarActualizarCentroMonitoreoHse")]
    public async Task<IActionResult> MostrarActualizarCentroMonitoreoHse([FromQuery] int Centro_HSE_Id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Centro_HSE_Id", Centro_HSE_Id);

        // Este SP devuelve Usr_Nom, Cliente_Nombre, Centro_Hse_Documento,
        // Centro_HSE_Audio y Estado para pintar el formulario de edición.
        var filas = await connection.QueryAsync("[dbo].[SP_Mostrar_Actualizar_Centro_HSE]", parametros, commandType: CommandType.StoredProcedure);
        return Ok(filas.ToList());
    }

    [HttpPost]
    [Route("postInsertarCentroMonitoreoHse")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> InsertarCentroMonitoreoHse([FromForm] RegistrarCentroMonitoreoHseFormRequest valores)
    {
        var carpeta = @"C:\Inspecciones\Centro_Monitoreo_HSE";
        Directory.CreateDirectory(carpeta);

        var documentos = await GuardarArchivosAsync(valores.Monitoreo_Documentos, carpeta, "Documento", esAudio: false);
        var audiosNuevos = await GuardarArchivosAsync(valores.Monitoreo_Audio, carpeta, "Audio", esAudio: true);
        var audiosExistentes = NormalizarRutasExistentes(valores.Monitoreo_Audio_Ubicacion, carpeta);
        var audio = CombinarRutas(audiosExistentes.Concat(audiosNuevos));

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Usr_Supervisor", valores.Usr_Cod);
        parametros.Add("@Cliente_Id", valores.Cliente_Id);
        parametros.Add("@Centro_HSE_Documento", CombinarRutas(documentos));
        parametros.Add("@Centro_HSE_Audio", audio);
        parametros.Add("@Usr_Reg", valores.Usr_Reg);

        var result = await connection.ExecuteAsync("[dbo].[SP_Insertar_Centro_HSE]", parametros, commandType: CommandType.StoredProcedure);
        return result > 0
            ? Ok(new { Success = true, Message = "Centro de Monitoreo HSE registrado correctamente." })
            : BadRequest(new { Success = false, Message = "No se pudo registrar el Centro de Monitoreo HSE." });
    }

    [HttpPost]
    [Route("postActualizarCentroMonitoreoHse")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> ActualizarCentroMonitoreoHse([FromForm] ActualizarCentroMonitoreoHseFormRequest valores)
    {
        var carpeta = @"C:\Inspecciones\Centro_Monitoreo_HSE";
        Directory.CreateDirectory(carpeta);

        var documentosNuevos = await GuardarArchivosAsync(valores.Monitoreo_Documentos, carpeta, "Documento", esAudio: false);
        var documentosExistentes = NormalizarRutasExistentes(valores.Monitoreo_Documentos_Ubicacion, carpeta);
        var documentos = CombinarRutas(documentosExistentes.Concat(documentosNuevos));

        var audiosNuevos = await GuardarArchivosAsync(valores.Monitoreo_Audio, carpeta, "Audio", esAudio: true);
        var audiosExistentes = NormalizarRutasExistentes(valores.Monitoreo_Audio_Ubicacion, carpeta);
        var audio = CombinarRutas(audiosExistentes.Concat(audiosNuevos));

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Centro_HSE_Id", valores.Centro_Monitoreo_Id);
        parametros.Add("@Cliente_Id", valores.Cliente_Id);
        parametros.Add("@Centro_Hse_Documento", documentos);
        parametros.Add("@Centro_HSE_Audio", audio);
        parametros.Add("@Usr_Mod", valores.Usr_Mod ?? valores.Usr_Cod);
        parametros.Add("@Fec_Mod", DateTime.Now);
        parametros.Add("@Estado", NormalizarEstado(valores.Estado));

        try
        {
            await connection.ExecuteAsync("[dbo].[SP_Actualizar_Centro_HSE]", parametros, commandType: CommandType.StoredProcedure);
            return Ok(new { Success = true, Message = "Centro de Monitoreo HSE actualizado correctamente." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Success = false, Message = "No se pudo actualizar el Centro de Monitoreo HSE.", Detail = ex.Message });
        }
    }

    [HttpPost]
    [Route("postEliminarCentroMonitoreoHse")]
    public async Task<IActionResult> EliminarCentroMonitoreoHse([FromBody] EliminarCentroMonitoreoHseRequest valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Centro_HSE_Id", valores.Centro_Monitoreo_Id);
        parametros.Add("@Usr_Mod", valores.Usr_Mod);

        var result = await connection.ExecuteAsync("[dbo].[SP_Eliminar_Centro_HSE]", parametros, commandType: CommandType.StoredProcedure);
        return result > 0
            ? Ok(new { Success = true, Message = "Centro de Monitoreo HSE eliminado correctamente." })
            : BadRequest(new { Success = false, Message = "No se pudo eliminar el Centro de Monitoreo HSE." });
    }

    [HttpGet]
    [Route("getArchivoCentroMonitoreoHse")]
    public IActionResult GetArchivoCentroMonitoreoHse(string rutaArchivo)
    {
        if (string.IsNullOrWhiteSpace(rutaArchivo))
        {
            return BadRequest("La ruta del archivo es obligatoria");
        }

        var ruta = rutaArchivo.Trim();
        if (!Path.IsPathRooted(ruta))
        {
            ruta = Path.Combine(@"C:\Inspecciones\Centro_Monitoreo_HSE", ruta);
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

    private static CentroMonitoreoHseEntity MapearFila(dynamic fila)
    {
        var dict = (IDictionary<string, object>)fila;
        return new CentroMonitoreoHseEntity
        {
            Centro_Monitoreo_Id = ObtenerInt(dict, "Centro_Monitoreo_Id", "CentroMonitoreo_Id", "Id", "id"),
            Codigo_Centro_Monitoreo = ObtenerTexto(dict, "Codigo_Centro_Monitoreo", "Codigo", "codigo"),
            Usr_Cod = ObtenerTexto(dict, "Usr_Cod", "usr_Cod"),
            Supervisor_Nom = ObtenerTexto(dict, "Supervisor_Nom", "Usr_Nom", "usr_Nom"),
            Cliente_Id = ObtenerInt(dict, "Cliente_Id", "cliente_Id"),
            Cliente_Nombre = ObtenerTexto(dict, "Cliente_Nombre", "cliente_Nombre"),
            Monitoreo_Documentos_Ubicacion = ObtenerTexto(dict, "Monitoreo_Documentos_Ubicacion", "monitoreo_Documentos_Ubicacion"),
            Monitoreo_Audio_Ubicacion = ObtenerTexto(dict, "Monitoreo_Audio_Ubicacion", "monitoreo_Audio_Ubicacion"),
            Estado = ObtenerTexto(dict, "Estado", "estado"),
        };
    }

    private static int? LeerInt(SqlDataReader reader, int ordinal)
    {
        if (ordinal >= reader.FieldCount || reader.IsDBNull(ordinal)) return null;
        return int.TryParse(reader.GetValue(ordinal).ToString(), out var n) ? n : null;
    }

    private static string? LeerTexto(SqlDataReader reader, int ordinal)
    {
        if (ordinal >= reader.FieldCount || reader.IsDBNull(ordinal)) return null;
        var texto = reader.GetValue(ordinal)?.ToString()?.Trim();
        return string.IsNullOrWhiteSpace(texto) ? null : texto;
    }

    private static int? ObtenerInt(IDictionary<string, object> dict, params string[] keys)
    {
        foreach (var key in keys)
        {
            if (!dict.TryGetValue(key, out var valor) || valor is null || valor == DBNull.Value) continue;
            if (int.TryParse(valor.ToString(), out var n)) return n;
        }
        return null;
    }

    private static string? ObtenerTexto(IDictionary<string, object> dict, params string[] keys)
    {
        foreach (var key in keys)
        {
            if (!dict.TryGetValue(key, out var valor) || valor is null || valor == DBNull.Value) continue;
            var texto = valor.ToString()?.Trim();
            if (!string.IsNullOrWhiteSpace(texto)) return texto;
        }
        return null;
    }

    private static string NormalizarEstado(string? valor)
    {
        return string.IsNullOrWhiteSpace(valor) ? "A" : (valor.Trim().ToUpperInvariant().StartsWith("I") ? "I" : "A");
    }

    private static async Task<List<string>> GuardarArchivosAsync(IEnumerable<IFormFile>? archivos, string carpeta, string prefijo, bool esAudio = false)
    {
        var rutas = new List<string>();
        if (archivos is null) return rutas;

        foreach (var archivo in archivos)
        {
            var ruta = await GuardarArchivoAsync(archivo, carpeta, prefijo, esAudio);
            if (!string.IsNullOrWhiteSpace(ruta))
            {
                rutas.Add(ruta);
            }
        }
        return rutas;
    }

    private static async Task<string> GuardarArchivoAsync(IFormFile? archivo, string carpeta, string prefijo, bool esAudio = false)
    {
        if (archivo is null || archivo.Length <= 0) return string.Empty;

        if (EsVideo(archivo, esAudio))
        {
            return string.Empty;
        }

        var extension = Path.GetExtension(archivo.FileName);
        if (string.IsNullOrWhiteSpace(extension))
        {
            extension = esAudio ? ".webm" : ".bin";
        }

        var nombreArchivo = $"{prefijo}_{DateTime.Now:yyyyMMdd_HHmmssfff}_{Guid.NewGuid():N}{extension}";
        var rutaCompleta = Path.Combine(carpeta, nombreArchivo);

        await using var stream = new FileStream(rutaCompleta, FileMode.Create, FileAccess.Write, FileShare.None);
        await archivo.CopyToAsync(stream);
        return rutaCompleta;
    }

    private static bool EsVideo(IFormFile archivo, bool esAudio = false)
    {
        var contentType = archivo.ContentType ?? string.Empty;

        // El Content-Type es la fuente más confiable: si el navegador dice que es
        // audio (p. ej. "audio/webm" al grabar desde el micrófono), nunca se descarta,
        // sin importar la extensión del archivo.
        if (contentType.StartsWith("audio/", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (contentType.StartsWith("video/", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (esAudio)
        {
            // Viene del flujo de audio (grabación o selección de archivo de audio):
            // no se descarta por extensión, ya que .webm/.ogg también son formatos de audio válidos.
            return false;
        }

        var ext = Path.GetExtension(archivo.FileName).ToLowerInvariant();
        return ext is ".mp4" or ".mkv" or ".mov" or ".avi" or ".webm" or ".wmv" or ".flv" or ".m4v";
    }

    private static List<string> NormalizarRutasExistentes(IEnumerable<string>? rutasArchivos, string carpetaBase)
    {
        var rutas = new List<string>();
        if (rutasArchivos is null) return rutas;

        foreach (var rutaArchivo in rutasArchivos)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivo)) continue;
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
        if (rutasArchivos is null) return string.Empty;

        var rutas = rutasArchivos
            .Select(ruta => (ruta ?? string.Empty).Trim())
            .Where(ruta => !string.IsNullOrWhiteSpace(ruta))
            .ToList();

        return rutas.Count > 0 ? string.Join(Environment.NewLine, rutas) : string.Empty;
    }
}
