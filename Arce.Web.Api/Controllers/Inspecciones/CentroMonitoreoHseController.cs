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
using System.Text;

namespace Arce.Web.Api.Controllers.Inspecciones;

[ApiController]
[Route("api/[controller]")]
public class CentroMonitoreoHseController : ControllerBase
{
    private readonly string _connectionString;

    static CentroMonitoreoHseController()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

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
        // 4 Cliente_Nombre, 5 Centro_Revision, 6 Centro_Puntaje, 7 Centro_Comentario.
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
                Centro_Comentario = LeerTexto(reader, 7),
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


    [HttpGet]
    [Route("getDatosReportePdfCentroMonitoreoHse")]
    public async Task<IActionResult> GetDatosReportePdfCentroMonitoreoHse([FromQuery] int Centro_HSE_Id)
    {
        if (Centro_HSE_Id <= 0)
        {
            return BadRequest(new { Success = false, Message = "El identificador del Centro de Monitoreo HSE es obligatorio." });
        }

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Centro_HSE_Id", Centro_HSE_Id);

        var fila = (await connection.QueryAsync("[dbo].[SP_ReportePDF_Centro_HSE]", parametros, commandType: CommandType.StoredProcedure))
            .FirstOrDefault();

        if (fila is null)
        {
            return NotFound(new { Success = false, Message = "No se encontró información para generar el PDF." });
        }

        return Ok(fila);
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
        parametros.Add("@Centro_Ubicacion", NormalizarUbicacionMaps(valores.Centro_Ubicacion));

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

    // Guarda la Nota de Centro de Monitoreo HSE: una fila en Ins_Puntaje_Centro_HSE
    // por cada pregunta y tipo (Audio/Documento) marcados en el diálogo, junto con
    // el comentario general del Centro de Monitoreo HSE (Centro_Comentario).
    [HttpPost]
    [Route("postInsertarPuntajeCentroHse")]
    public async Task<IActionResult> InsertarPuntajeCentroHse([FromBody] InsertarPuntajeCentroHseRequest valores)
    {
        if (valores.Centro_HSE_Id <= 0)
        {
            return BadRequest(new { Success = false, Message = "No se pudo identificar el Centro de Monitoreo HSE." });
        }

        if (valores.Detalles is null || valores.Detalles.Count == 0)
        {
            return BadRequest(new { Success = false, Message = "No hay respuestas para registrar." });
        }

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        // Puntaje final: cuántas respuestas fueron "Pasó" (S) sobre el total de
        // respuestas registradas (preguntas x 2, Audio + Documento). Ej.: 8 Pasó en
        // Audio + 3 Pasó en Documento, de 10 preguntas => "11/20".
        var aprobadas = valores.Detalles.Count(d => string.Equals(d.Puntaje_Rpta, "S", StringComparison.OrdinalIgnoreCase));
        var total = valores.Detalles.Count;
        var centroPuntaje = $"{aprobadas}/{total}";
        var centroComentario = (valores.Centro_Comentario ?? string.Empty).Trim();

        // SP_Insertar_Puntaje_Centro_HSE ahora fusiona lo que antes hacían
        // SP_Insertar_Puntaje_Centro_HSE + SP_Estado_Puntaje_Centro_HSE: por cada
        // detalle inserta la fila en Ins_Puntaje_Centro_HSE y además actualiza
        // Ins_Centro_HSE (Centro_Comentario, Usr_Mod, Fec_Mod, Usr_Inspector,
        // Centro_Revision, Centro_Puntaje). Por eso se envían esos datos en cada
        // llamada del bucle.
        foreach (var detalle in valores.Detalles)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Centro_HSE_Id", valores.Centro_HSE_Id);
            parametros.Add("@Pregunta_Id", detalle.Pregunta_Id);
            parametros.Add("@Puntaje_Tipo", detalle.Puntaje_Tipo);
            parametros.Add("@Puntaje_Rpta", detalle.Puntaje_Rpta);
            parametros.Add("@Usr_Reg", valores.Usr_Reg);
            parametros.Add("@Centro_Comentario", centroComentario);
            parametros.Add("@Usr_Mod", valores.Usr_Reg);
            parametros.Add("@Centro_Puntaje", centroPuntaje);

            await connection.ExecuteAsync("[dbo].[SP_Insertar_Puntaje_Centro_HSE]", parametros, commandType: CommandType.StoredProcedure);
        }

        return Ok(new { Success = true, Message = "Nota de Centro de Monitoreo HSE registrada correctamente." });
    }

    // Trae la respuesta vigente (Audio y Documento) de cada pregunta, para pintar
    // el formulario de edición de Puntaje, junto con el Centro_Comentario vigente.
    // El SP devuelve dos result sets: 1) el detalle de preguntas, 2) una fila con
    // el Centro_Comentario de Ins_Centro_HSE.
    [HttpGet]
    [Route("getMostrarActualizarPuntajeCentroHse")]
    public async Task<IActionResult> MostrarActualizarPuntajeCentroHse([FromQuery] int Centro_HSE_Id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Centro_HSE_Id", Centro_HSE_Id);

        using var multi = await connection.QueryMultipleAsync("[dbo].[SP_Mostrar_Actualizar_Puntaje_Centro_HSE]", parametros, commandType: CommandType.StoredProcedure);
        var detalles = (await multi.ReadAsync()).ToList();
        var comentarioFila = (await multi.ReadAsync()).FirstOrDefault();

        string? centroComentario = null;
        if (comentarioFila is not null)
        {
            var dict = (IDictionary<string, object>)comentarioFila;
            centroComentario = ObtenerTexto(dict, "Centro_Comentario", "centro_Comentario");
        }

        return Ok(new
        {
            Detalles = detalles,
            Centro_Comentario = centroComentario ?? string.Empty
        });
    }

    // Actualiza las respuestas de Puntaje (por Puntaje_Id), guarda el motivo y
    // recalcula el marcador o abre el centro cuando la revisión queda en ABIERTO.
    [HttpPost]
    [Route("postActualizarPuntajeCentroHse")]
    public async Task<IActionResult> ActualizarPuntajeCentroHse([FromBody] ActualizarPuntajeCentroHseRequest valores)
    {
        if (valores.Centro_HSE_Id <= 0)
        {
            return BadRequest(new { Success = false, Message = "No se pudo identificar el Centro de Monitoreo HSE." });
        }

        if (valores.Detalles is null || valores.Detalles.Count == 0)
        {
            return BadRequest(new { Success = false, Message = "No hay respuestas para actualizar." });
        }

        var revision = NormalizarRevision(valores.Centro_Revision);
        var motivo = (valores.Centro_Motivo ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(motivo))
        {
            return BadRequest(new { Success = false, Message = "Escriba el motivo por el cual está editando el puntaje." });
        }

        var centroComentario = (valores.Centro_Comentario ?? string.Empty).Trim();

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        using var transaction = connection.BeginTransaction();

        try
        {
            foreach (var detalle in valores.Detalles)
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Puntaje_Id", detalle.Puntaje_Id);
                parametros.Add("@Puntaje_Rpta", detalle.Puntaje_Rpta);
                parametros.Add("@Usr_Mod", valores.Usr_Mod);
                parametros.Add("@Centro_HSE_Id", valores.Centro_HSE_Id);
                parametros.Add("@Centro_Motivo", motivo);
                parametros.Add("@Centro_Comentario", centroComentario);
                parametros.Add("@Centro_Revision", revision);

                await connection.ExecuteAsync("[dbo].[SP_Actualizar_Puntaje_Centro_HSE]", parametros, transaction: transaction, commandType: CommandType.StoredProcedure);
            }

            if (revision == "ABIERTO")
            {
                var parametrosEliminar = new DynamicParameters();
                parametrosEliminar.Add("@Centro_HSE_Id", valores.Centro_HSE_Id);
                parametrosEliminar.Add("@Motivo", motivo);
                parametrosEliminar.Add("@Usr_Inspector", valores.Usr_Mod);

                await connection.ExecuteAsync("[dbo].[SP_Eliminar_Puntaje_Centro_HSE]", parametrosEliminar, transaction: transaction, commandType: CommandType.StoredProcedure);
            }
            else
            {
                var aprobadas = valores.Detalles.Count(d => string.Equals(d.Puntaje_Rpta, "S", StringComparison.OrdinalIgnoreCase));
                var total = valores.Detalles.Count;
                var centroPuntaje = $"{aprobadas}/{total}";

                var parametrosEstado = new DynamicParameters();
                parametrosEstado.Add("@Centro_HSE_Id", valores.Centro_HSE_Id);
                parametrosEstado.Add("@Usr_Mod", valores.Usr_Mod);
                parametrosEstado.Add("@Centro_Puntaje", centroPuntaje);

                // SP_Estado_Puntaje_Centro_HSE ya no existe: su lógica quedó
                // fusionada dentro de SP_Insertar_Puntaje_Centro_HSE. Para este
                // flujo de edición (Centro_Revision se mantiene CERRADO) se hace
                // el mismo UPDATE directamente, sin depender de un SP eliminado.
                const string actualizarEstadoSql = @"
                    UPDATE Ins_Centro_HSE
                    SET
                        Usr_Mod         = @Usr_Mod,
                        Fec_Mod         = GETDATE(),
                        Usr_Inspector   = @Usr_Mod,
                        Centro_Revision = 'CERRADO',
                        Centro_Puntaje  = @Centro_Puntaje
                    WHERE Centro_HSE_Id = @Centro_HSE_Id";

                await connection.ExecuteAsync(actualizarEstadoSql, parametrosEstado, transaction: transaction);
            }

            transaction.Commit();
            return Ok(new { Success = true, Message = "Puntaje de Centro de Monitoreo HSE actualizado correctamente." });
        }
        catch (Exception ex)
        {
            try { transaction.Rollback(); } catch { }
            return BadRequest(new { Success = false, Message = "No se pudo actualizar el Puntaje de Centro de Monitoreo HSE.", Detail = ex.Message });
        }
    }

    [HttpGet]
    [Route("getReportePdfCentroMonitoreoHse")]
    public async Task<IActionResult> GetReportePdfCentroMonitoreoHse([FromQuery] int Centro_HSE_Id)
    {
        if (Centro_HSE_Id <= 0)
        {
            return BadRequest(new { Success = false, Message = "El identificador del Centro de Monitoreo HSE es obligatorio." });
        }

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Centro_HSE_Id", Centro_HSE_Id);

        var fila = (await connection.QueryAsync("[dbo].[SP_ReportePDF_Centro_HSE]", parametros, commandType: CommandType.StoredProcedure))
            .FirstOrDefault();

        if (fila is null)
        {
            return NotFound(new { Success = false, Message = "No se encontró información para generar el PDF." });
        }

        var dict = (IDictionary<string, object>)fila;
        var reporte = new CentroMonitoreoHsePdfData
        {
            Codigo = ObtenerTexto(dict, "Centro_HSE_Cod", "Centro_HSE_COD", "Codigo_Centro_HSE") ?? string.Empty,
            Supervisor = ObtenerTexto(dict, "Supervisor", "Usr_Supervisor", "Supervisor_Nom") ?? string.Empty,
            Inspector = ObtenerTexto(dict, "Inspector", "Usr_Inspector", "Inspector_Nom") ?? string.Empty,
            Fecha = ObtenerTexto(dict, "Fecha") ?? string.Empty,
            Hora = ObtenerTexto(dict, "Hora") ?? string.Empty,
            Estado = ObtenerTexto(dict, "Centro_Revision", "Revision") ?? string.Empty,
            Ubicacion = ConstruirUrlMaps(ObtenerTexto(dict, "Centro_Ubicacion", "Ubicacion")),
            Puntaje = ObtenerTexto(dict, "Centro_Puntaje", "Puntaje") ?? string.Empty,
        };

        var pdfBytes = GenerarPdfReporteCentroHse(reporte);
        return File(pdfBytes, "application/pdf", $"Centro_HSE_{Centro_HSE_Id}.pdf");
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

    private static string NormalizarRevision(string? valor)
    {
        return string.Equals(valor?.Trim(), "ABIERTO", StringComparison.OrdinalIgnoreCase)
            ? "ABIERTO"
            : "CERRADO";
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



    private sealed class CentroMonitoreoHsePdfData
    {
        public string Codigo { get; set; } = string.Empty;
        public string Supervisor { get; set; } = string.Empty;
        public string Inspector { get; set; } = string.Empty;
        public string Fecha { get; set; } = string.Empty;
        public string Hora { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public string Puntaje { get; set; } = string.Empty;
    }

    private static byte[] GenerarPdfReporteCentroHse(CentroMonitoreoHsePdfData reporte)
    {
        var encoding = Encoding.GetEncoding(1252);
        var logoPath = ResolverRutaLogoArce();
        var logoBytes = !string.IsNullOrWhiteSpace(logoPath) && System.IO.File.Exists(logoPath)
            ? System.IO.File.ReadAllBytes(logoPath)
            : null;

        var logoInfo = logoBytes is null ? null : ObtenerDimensionesJpeg(logoBytes);
        var incluirLogo = logoBytes is not null && logoInfo is not null;
        var content = ConstruirContenidoPdfCentroHse(reporte, incluirLogo);
        var contentObjectNumber = incluirLogo ? 7 : 6;

        var objetos = new List<string>
        {
            "<< /Type /Catalog /Pages 2 0 R >>",
            "<< /Type /Pages /Kids [3 0 R] /Count 1 >>",
            incluirLogo
                ? $"<< /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] /Resources << /Font << /F1 4 0 R /F2 5 0 R >> /XObject << /Im1 6 0 R >> >> /Contents {contentObjectNumber} 0 R >>"
                : $"<< /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] /Resources << /Font << /F1 4 0 R /F2 5 0 R >> >> /Contents {contentObjectNumber} 0 R >>",
            "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>",
            "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica-Bold >>"
        };

        if (incluirLogo)
        {
            objetos.Add(ConstruirObjetoImagenJpeg(logoBytes!, logoInfo!.Value.Width, logoInfo!.Value.Height));
        }

        objetos.Add(ConstruirObjetoStream(content));

        var pdf = new StringBuilder();
        pdf.AppendLine("%PDF-1.4");
        pdf.AppendLine("%âãÏÓ");

        var offsets = new List<int> { 0 };
        var currentOffset = encoding.GetByteCount(pdf.ToString());

        for (int i = 0; i < objetos.Count; i++)
        {
            offsets.Add(currentOffset);
            var objeto = $@"{i + 1} 0 obj{Environment.NewLine}{objetos[i]}{Environment.NewLine}endobj{Environment.NewLine}";
            currentOffset += encoding.GetByteCount(objeto);
            pdf.Append(objeto);
        }

        var startXref = currentOffset;
        pdf.AppendLine("xref");
        pdf.AppendLine($"0 {objetos.Count + 1}");
        pdf.AppendLine("0000000000 65535 f ");
        foreach (var offset in offsets.Skip(1))
        {
            pdf.AppendLine($"{offset:0000000000} 00000 n ");
        }

        pdf.AppendLine("trailer");
        pdf.AppendLine($"<< /Size {objetos.Count + 1} /Root 1 0 R >>");
        pdf.AppendLine("startxref");
        pdf.AppendLine($"{startXref}");
        pdf.Append("%%EOF");

        return encoding.GetBytes(pdf.ToString());
    }

    private static string ConstruirContenidoPdfCentroHse(CentroMonitoreoHsePdfData reporte, bool incluirLogo)
    {
        var sb = new StringBuilder();

        if (incluirLogo)
        {
            AppendImagen(sb, "Im1", 22f, 780f, 115f, 48f);
        }

        AppendCenteredText(sb, "F2", 20, 775, "REPORTE CENTRO DE MONITOREO HSE");
        AppendCenteredText(sb, "F2", 16, 748, $"Nro.{reporte.Codigo}");
        AppendCenteredText(sb, "F2", 14, 695, "DATOS GENERALES");

        var labelX = 28f;
        var valueX = 285f;
        var lineHeight = 20f;
        var y = 650f;

        AppendLabelValue(sb, "F2", "F1", 11, labelX, valueX, y, "Subido por:", reporte.Supervisor);
        y -= lineHeight;
        AppendLabelValue(sb, "F2", "F1", 11, labelX, valueX, y, "Revisado por:", reporte.Inspector);
        y -= lineHeight;
        AppendLabelValue(sb, "F2", "F1", 11, labelX, valueX, y, "Fecha:", reporte.Fecha);
        y -= lineHeight;
        AppendLabelValue(sb, "F2", "F1", 11, labelX, valueX, y, "Hora:", reporte.Hora);
        y -= lineHeight;
        AppendLabelValue(sb, "F2", "F1", 11, labelX, valueX, y, "Estado:", reporte.Estado);

        y -= lineHeight;
        AppendLabelWrappedValue(sb, "F2", "F1", 11, labelX, valueX, y, "Ubicación:", reporte.Ubicacion, 260f, true);

        AppendCenteredText(sb, "F2", 16, 470, "Puntuacion");

        var tableX = 22f;
        var tableY = 420f;
        var tableWidth = 550f;
        var rowH = 22f;
        var boxH = rowH * 4;

        DrawRect(sb, tableX, tableY - boxH, tableWidth, boxH);
        DrawLine(sb, tableX, tableY - rowH, tableX + tableWidth, tableY - rowH);
        DrawLine(sb, tableX, tableY - rowH * 2, tableX + tableWidth, tableY - rowH * 2);
        DrawLine(sb, tableX, tableY - rowH * 3, tableX + tableWidth, tableY - rowH * 3);

        AppendCenteredTextInBox(sb, "F2", 12, tableX, tableY - rowH, tableWidth, rowH, "Puntuacion");
        AppendCenteredTextInBox(sb, "F2", 12, tableX, tableY - rowH * 2, tableWidth, rowH, FormatearPuntaje(reporte.Puntaje));
        AppendCenteredTextInBox(sb, "F2", 12, tableX, tableY - rowH * 3, tableWidth, rowH, "Notas");

        return sb.ToString();
    }

    private static void AppendImagen(StringBuilder sb, string nombreObjeto, float x, float y, float width, float height)
    {
        sb.AppendLine("q");
        sb.AppendLine($"{width} 0 0 {height} {x} {y} cm");
        sb.AppendLine($"/{nombreObjeto} Do");
        sb.AppendLine("Q");
    }

    private static string ConstruirObjetoStream(string contenido)
    {
        var encoding = Encoding.GetEncoding(1252);
        var length = encoding.GetByteCount(contenido);
        return $@"<< /Length {length} >>{Environment.NewLine}stream{Environment.NewLine}{contenido}{Environment.NewLine}endstream";
    }

    private static string ConstruirObjetoImagenJpeg(byte[] bytes, int width, int height)
    {
        var hex = ToAsciiHex(bytes);
        return $@"<< /Type /XObject /Subtype /Image /Width {width} /Height {height} /ColorSpace /DeviceRGB /BitsPerComponent 8 /Filter [/ASCIIHexDecode /DCTDecode] /Length {hex.Length} >>{Environment.NewLine}stream{Environment.NewLine}{hex}{Environment.NewLine}endstream";
    }

    private static string ToAsciiHex(byte[] bytes)
    {
        var sb = new StringBuilder(bytes.Length * 2 + 1);
        foreach (var b in bytes)
        {
            sb.Append(b.ToString("X2"));
        }

        sb.Append('>');
        return sb.ToString();
    }

    private static (int Width, int Height)? ObtenerDimensionesJpeg(byte[] bytes)
    {
        if (bytes.Length < 4 || bytes[0] != 0xFF || bytes[1] != 0xD8)
        {
            return null;
        }

        var i = 2;
        while (i + 1 < bytes.Length)
        {
            while (i < bytes.Length && bytes[i] != 0xFF)
            {
                i++;
            }

            while (i < bytes.Length && bytes[i] == 0xFF)
            {
                i++;
            }

            if (i >= bytes.Length)
            {
                break;
            }

            var marker = bytes[i++];
            if (marker is 0xD9 or 0xDA)
            {
                break;
            }

            if (i + 1 >= bytes.Length)
            {
                break;
            }

            var length = (bytes[i] << 8) + bytes[i + 1];
            if (length < 2 || i + length - 1 > bytes.Length)
            {
                break;
            }

            if (marker is 0xC0 or 0xC1 or 0xC2 or 0xC3 or 0xC5 or 0xC6 or 0xC7 or 0xC9 or 0xCA or 0xCB or 0xCD or 0xCE or 0xCF)
            {
                if (i + 7 >= bytes.Length)
                {
                    break;
                }

                var height = (bytes[i + 3] << 8) + bytes[i + 4];
                var width = (bytes[i + 5] << 8) + bytes[i + 6];
                return (width, height);
            }

            i += length;
        }

        return null;
    }

    private static string? ResolverRutaLogoArce()
    {
        var candidatos = new[]
        {
            Path.Combine(Directory.GetCurrentDirectory(), "src", "assets", "ArceLogo.jpg"),
            Path.Combine(Directory.GetCurrentDirectory(), "..", "src", "assets", "ArceLogo.jpg"),
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "src", "assets", "ArceLogo.jpg"),
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "src", "assets", "ArceLogo.jpg")
        };

        foreach (var candidato in candidatos)
        {
            var ruta = Path.GetFullPath(candidato);
            if (System.IO.File.Exists(ruta))
            {
                return ruta;
            }
        }

        return null;
    }

    private static string NormalizarUbicacionMaps(string? ubicacion)
    {
        if (string.IsNullOrWhiteSpace(ubicacion))
        {
            return string.Empty;
        }

        var texto = ubicacion.Trim();
        if (texto.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            texto.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return texto;
        }

        var coordenadas = texto.Replace(" ", string.Empty);
        return $"https://www.google.com/maps/search/?api=1&query={Uri.EscapeDataString(coordenadas)}";
    }

    private static string ConstruirUrlMaps(string? ubicacion)
    {
        if (string.IsNullOrWhiteSpace(ubicacion))
        {
            return "-";
        }

        var texto = ubicacion.Trim();
        if (texto.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            texto.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return texto;
        }

        var coordenadas = texto.Replace(" ", string.Empty);
        return $"https://www.google.com/maps/search/?api=1&query={Uri.EscapeDataString(coordenadas)}";
    }

    private static void AppendCenteredText(StringBuilder sb, string font, float size, float y, string text)
    {
        const float pageWidth = 595f;
        var x = (pageWidth - EstimarAnchoTexto(text, size, font)) / 2f;
        x = Math.Max(18f, x);
        AppendText(sb, font, size, x, y, text);
    }

    private static void AppendCenteredTextInBox(StringBuilder sb, string font, float size, float boxX, float boxY, float boxWidth, float boxHeight, string text)
    {
        var textWidth = EstimarAnchoTexto(text, size, font);
        var x = boxX + (boxWidth - textWidth) / 2f;
        var y = boxY + (boxHeight - size) / 2f + 2f;
        AppendText(sb, font, size, x, y, text);
    }

    private static void AppendLabelValue(StringBuilder sb, string labelFont, string valueFont, float size, float labelX, float valueX, float y, string label, string value)
    {
        AppendText(sb, labelFont, size, labelX, y, label);
        AppendText(sb, valueFont, size, valueX, y, string.IsNullOrWhiteSpace(value) ? "-" : value.Trim());
    }

    private static void AppendLabelWrappedValue(StringBuilder sb, string labelFont, string valueFont, float size, float labelX, float valueX, float y, string label, string value, float maxWidth, bool useBlue)
    {
        AppendText(sb, labelFont, size, labelX, y, label);
        var wrapped = WrapText(value, size, maxWidth, valueFont);
        var lineY = y;
        foreach (var line in wrapped)
        {
            if (useBlue)
            {
                sb.AppendLine("0 0 1 rg");
                AppendText(sb, valueFont, size, valueX, lineY, line);
                sb.AppendLine("0 0 0 rg");
            }
            else
            {
                AppendText(sb, valueFont, size, valueX, lineY, line);
            }

            lineY -= size + 2f;
        }
    }

    private static void AppendText(StringBuilder sb, string font, float size, float x, float y, string text)
    {
        sb.AppendLine($@"BT /{font} {size:0.##} Tf 1 0 0 1 {x:0.##} {y:0.##} Tm ({EscapePdfText(text)}) Tj ET");
    }

    private static void DrawRect(StringBuilder sb, float x, float y, float width, float height)
    {
        sb.AppendLine($@"{x:0.##} {y:0.##} {width:0.##} {height:0.##} re S");
    }

    private static void DrawLine(StringBuilder sb, float x1, float y1, float x2, float y2)
    {
        sb.AppendLine($@"{x1:0.##} {y1:0.##} m {x2:0.##} {y2:0.##} l S");
    }

    private static float EstimarAnchoTexto(string text, float size, string font)
    {
        var factor = string.Equals(font, "F2", StringComparison.OrdinalIgnoreCase) ? 0.57f : 0.52f;
        return Math.Max(0f, text?.Length ?? 0) * size * factor;
    }

    private static List<string> WrapText(string text, float size, float maxWidth, string font)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return new List<string> { "-" };
        }

        var palabras = text.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var resultado = new List<string>();
        var actual = new StringBuilder();

        foreach (var palabra in palabras)
        {
            var candidate = actual.Length == 0 ? palabra : actual + " " + palabra;
            if (EstimarAnchoTexto(candidate, size, font) <= maxWidth)
            {
                actual.Clear();
                actual.Append(candidate);
            }
            else
            {
                if (actual.Length > 0)
                {
                    resultado.Add(actual.ToString());
                    actual.Clear();
                }

                if (EstimarAnchoTexto(palabra, size, font) <= maxWidth)
                {
                    actual.Append(palabra);
                }
                else
                {
                    foreach (var chunk in DividirPalabra(palabra, size, maxWidth, font))
                    {
                        if (!string.IsNullOrWhiteSpace(chunk))
                        {
                            resultado.Add(chunk);
                        }
                    }
                }
            }
        }

        if (actual.Length > 0)
        {
            resultado.Add(actual.ToString());
        }

        if (resultado.Count == 0)
        {
            resultado.Add(text.Trim());
        }

        return resultado;
    }

    private static IEnumerable<string> DividirPalabra(string word, float size, float maxWidth, string font)
    {
        var chunk = new StringBuilder();
        foreach (var ch in word)
        {
            var candidate = chunk.ToString() + ch;
            if (EstimarAnchoTexto(candidate, size, font) <= maxWidth)
            {
                chunk.Append(ch);
                continue;
            }

            if (chunk.Length > 0)
            {
                yield return chunk.ToString();
                chunk.Clear();
                chunk.Append(ch);
            }
            else
            {
                yield return ch.ToString();
            }
        }

        if (chunk.Length > 0)
        {
            yield return chunk.ToString();
        }
    }

    private static string FormatearPuntaje(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            return "-";
        }

        var limpio = valor.Trim();
        if (limpio.Contains("/"))
        {
            return limpio;
        }

        return $"{limpio} / 20";
    }

    private static string EscapePdfText(string text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        return text
            .Replace("\\", "\\\\")
            .Replace("(", "\\(")
            .Replace(")", "\\)")
            .Replace("\r", string.Empty)
            .Replace("\n", "\\n");
    }


}