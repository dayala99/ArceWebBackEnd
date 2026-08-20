using Arce.Web.Data;
using Arce.Web.Entity.TJH2B;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Arce.Web.Data.TJH2B;

public class CotizacionTjh2bRepository : ICotizacionTjh2bRepository
{
    private readonly string _connectionString;

    public CotizacionTjh2bRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<CotizacionTjh2bEntity>?> ListarCotizacionTjh2b(string? Numero, string? ClienteNombre, string? Servicio, string? Estado, string? CotizacionEstado, DateTime? FechaInicio, DateTime? FechaFin)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Fecha_Inicio", FechaInicio);
        parametros.Add("@Fecha_Fin", FechaFin);
        parametros.Add("@Estado", NormalizarEstado(Estado) ?? "A");
        parametros.Add("@Cotizacion_Estado", NormalizarCotizacionEstado(CotizacionEstado) ?? string.Empty);
        parametros.Add("@Cotizacion_Numero", Numero ?? string.Empty);
        parametros.Add("@Cliente_Nombre", ClienteNombre ?? string.Empty);
        parametros.Add("@Cotizacion_Servicio", Servicio ?? string.Empty);

        var result = await connection.QueryAsync(
            "[dbo].[SP_Filtrar_TJH2B_Cotizacion]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        return result.OfType<IDictionary<string, object>>().Select(MapCotizacion);
    }

    public async Task<IEnumerable<CotizacionTjh2bEntity>?> ConsultarDatosCotizacionTjh2b(int? Cotizacion_Id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Cotizacion_Id", Cotizacion_Id);

        var result = await connection.QueryAsync(
            "[dbo].[SP_Mostrar_Actualizar_TJH2B_Cotizacion]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        return result.OfType<IDictionary<string, object>>().Select(MapCotizacion);
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarCotizacionTjh2b(CotizacionTjh2bEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Cotizacion_Numero", valores.Numero);
        parametros.Add("@Cliente_Id", valores.Cliente_Id);
        parametros.Add("@Cotizacion_Servicio", valores.Servicio);
        parametros.Add("@Cotizacion_FechaIni", valores.FechaIni);
        parametros.Add("@Cotizacion_FechaFin", valores.FechaFin);
        parametros.Add("@Cotizacion_DocumentoPDF", valores.DocumentoPdf ?? string.Empty);
        parametros.Add("@Usr_Reg", valores.Usr_Reg);
        parametros.Add("@Cotizacion_Alerta", valores.Cotizacion_Alerta ?? valores.Cotizacion_UsuariosCorreo ?? string.Empty);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Insertar_TJH2B_Cotizacion]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return (0, string.Empty);
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarCotizacionTjh2b(CotizacionTjh2bEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Cotizacion_Id", valores.Id);
        parametros.Add("@Cotizacion_Numero", valores.Numero);
        parametros.Add("@Cliente_Id", valores.Cliente_Id);
        parametros.Add("@Cotizacion_Servicio", valores.Servicio);
        parametros.Add("@Cotizacion_FechaIni", valores.FechaIni);
        parametros.Add("@Cotizacion_FechaFin", valores.FechaFin);
        parametros.Add("@Cotizacion_Estado", NormalizarCotizacionEstado(valores.Cotizacion_Estado));
        parametros.Add("@Estado", NormalizarEstado(valores.Estado));
        parametros.Add("@Cotizacion_DocumentoPDF", valores.DocumentoPdf ?? string.Empty);
        parametros.Add("@Cotizacion_Alerta", valores.Cotizacion_Alerta ?? valores.Cotizacion_UsuariosCorreo ?? string.Empty);
        parametros.Add("@Usr_Mod", valores.Usr_Mod);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Actualizar_TJH2B_Cotizacion]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return (0, string.Empty);
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
    }

    public async Task<(int Codigo, string Mensaje)> EliminarCotizacionTjh2b(int? Cotizacion_Id, string? Usr_Mod)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Cotizacion_Id", Cotizacion_Id);
        parametros.Add("@Usr_Mod", Usr_Mod);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Eliminar_TJH2B_Cotizacion]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return (0, string.Empty);
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
    }

    private static CotizacionTjh2bEntity MapCotizacion(IDictionary<string, object> row)
    {
        return new CotizacionTjh2bEntity
        {
            Id = GetInt32(row, "Cotizacion_Id"),
            Numero = GetString(row, "Cotizacion_Numero"),
            Cliente_Id = GetInt32(row, "Cliente_Id"),
            Cliente_Nombre = GetString(row, "Cliente_Nombre"),
            Servicio = GetString(row, "Cotizacion_Servicio"),
            FechaIni = GetDateTime(row, "Cotizacion_FechaIni"),
            FechaFin = GetDateTime(row, "Cotizacion_FechaFin"),
            DocumentoPdf = GetString(row, "Cotizacion_DocumentoPDF"),
            Cotizacion_Alerta = GetString(row, "Cotizacion_Alerta") ?? GetString(row, "Cotizacion_UsuariosCorreo"),
            Cotizacion_UsuariosCorreo = GetString(row, "Cotizacion_UsuariosCorreo"),
            Cotizacion_Estado = GetString(row, "Cotizacion_Estado"),
            Estado = GetString(row, "Estado"),
            Usr_Reg = GetString(row, "Usr_Reg"),
            Fec_Reg = GetDateTime(row, "Fec_Reg"),
            Usr_Mod = GetString(row, "Usr_Mod"),
            Fec_Mod = GetDateTime(row, "Fec_Mod")
        };
    }

    private static int? GetInt32(IDictionary<string, object> row, string key)
    {
        if (!row.TryGetValue(key, out var value) || value is null || value is DBNull)
        {
            return null;
        }

        return value switch
        {
            int i => i,
            long l => (int)l,
            short s => s,
            decimal d => (int)d,
            double db => (int)db,
            float f => (int)f,
            _ => int.TryParse(value.ToString(), out var parsed) ? parsed : null
        };
    }

    private static string? GetString(IDictionary<string, object> row, string key)
    {
        if (!row.TryGetValue(key, out var value) || value is null || value is DBNull)
        {
            return null;
        }

        var text = value.ToString()?.Trim();
        return string.IsNullOrWhiteSpace(text) ? null : text;
    }

    private static DateTime? GetDateTime(IDictionary<string, object> row, string key)
    {
        if (!row.TryGetValue(key, out var value) || value is null || value is DBNull)
        {
            return null;
        }

        return value switch
        {
            DateTime dt => dt,
            DateTimeOffset dto => dto.DateTime,
            _ => DateTime.TryParse(value.ToString(), out var parsed) ? parsed : null
        };
    }

    private static string? NormalizarEstado(string? estado)
    {
        if (string.IsNullOrWhiteSpace(estado))
        {
            return null;
        }

        var limpio = estado.Trim().ToUpperInvariant();

        return limpio switch
        {
            "A" => "A",
            "ACTIVO" => "A",
            "I" => "I",
            "INACTIVO" => "I",
            _ => limpio.Length > 0 ? limpio[..1] : null
        };
    }

    private static string? NormalizarCotizacionEstado(string? estado)
    {
        if (string.IsNullOrWhiteSpace(estado))
        {
            return string.Empty;
        }

        var limpio = estado.Trim().ToUpperInvariant();

        return limpio switch
        {
            "P" => "P",
            "PENDIENTE" => "P",
            "F" => "F",
            "FINALIZADO" => "F",
            _ => limpio.Length > 0 ? limpio[..1] : string.Empty
        };
    }
}
