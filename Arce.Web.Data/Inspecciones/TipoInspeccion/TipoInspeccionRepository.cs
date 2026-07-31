using Arce.Web.Entity.Inspecciones;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Arce.Web.Data.Inspecciones.TipoInspeccion;

public class TipoInspeccionRepository : ITipoInspeccionRepository
{
    private readonly string _connectionString;

    public TipoInspeccionRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<TipoInspeccionEntity>?> ListarTipoInspeccion(int? Id, string? Nombre, string? Estado)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Tipo_Id", Id ?? 0);
        parametros.Add("@Tipo_Nombre", Nombre ?? string.Empty);
        parametros.Add("@Estado", NormalizarEstado(Estado) ?? "A");

        var filas = await connection.QueryAsync(
            "[dbo].[SP_Filtrar_Tipo_Inspeccion]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        return filas
            .Select(MapearTipoInspeccionDesdeFila)
            .ToList();
    }

    public async Task<IEnumerable<TipoInspeccionEntity>?> ConsultarDatosTipoInspeccion(int? Tipo_Id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Tipo_Id", Tipo_Id);

        var filas = await connection.QueryAsync(
            "[dbo].[SP_Mostrar_Actualizar_Tipo_Inspeccion]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        return filas.Select(fila =>
        {
            var dict = (IDictionary<string, object>)fila;
            return new TipoInspeccionEntity
            {
                Id = Tipo_Id,
                Nombre = ObtenerTexto(dict, "Tipo_Nombre", "Nombre", "tipo_Nombre", "nombre"),
                Estado = ObtenerTexto(dict, "Estado", "estado", "Flg_Est", "flg_est")
            };
        });
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarTipoInspeccion(TipoInspeccionEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Tipo_Nombre", valores.Nombre);
        parametros.Add("@Usr_Reg", valores.Usr_Reg);

        try
        {
            var afectados = await connection.ExecuteAsync(
                "[dbo].[SP_Insertar_Tipo_Inspeccion]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            if (afectados <= 0)
            {
                return (1, "No se pudo registrar el tipo de inspección.");
            }

            return (0, string.Empty);
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarTipoInspeccion(TipoInspeccionEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Tipo_Id", valores.Id);
        parametros.Add("@Tipo_Nombre", valores.Nombre);
        parametros.Add("@Usr_Reg", valores.Usr_Mod ?? valores.Usr_Reg);
        parametros.Add("@Estado", NormalizarEstado(valores.Estado));

        try
        {
            var afectados = await connection.ExecuteAsync(
                "[dbo].[SP_Actualizar_Tipo_Inspeccion]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            if (afectados <= 0)
            {
                return (1, "No se encontró el tipo de inspección para actualizar.");
            }

            return (0, string.Empty);
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
    }

    public async Task<(int Codigo, string Mensaje)> EliminarTipoInspeccion(int? Id, string? Usr_Mod)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Tipo_Id", Id);
        parametros.Add("@Usr_Mod", Usr_Mod);

        try
        {
            var afectados = await connection.ExecuteAsync(
                "[dbo].[SP_Eliminar_Tipo_Inspeccion]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            if (afectados <= 0)
            {
                return (1, "No se encontró el tipo de inspección para eliminar.");
            }

            return (0, string.Empty);
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
    }

    private static TipoInspeccionEntity MapearTipoInspeccionDesdeFila(object fila)
    {
        var dict = (IDictionary<string, object>)fila;

        return new TipoInspeccionEntity
        {
            Id = ObtenerEntero(dict, "Tipo_Id", "Id", "tipo_id"),
            Nombre = ObtenerTexto(dict, "Tipo_Nombre", "Nombre", "tipo_Nombre", "nombre"),
            Estado = ObtenerTexto(dict, "Estado", "estado", "Flg_Est", "flg_est")
        };
    }

    private static int? ObtenerEntero(IDictionary<string, object> fila, params string[] claves)
    {
        foreach (var clave in claves)
        {
            if (!fila.TryGetValue(clave, out var valor) || valor is null || valor is DBNull)
            {
                continue;
            }

            if (int.TryParse(valor.ToString(), out var numero))
            {
                return numero;
            }
        }

        return null;
    }

    private static string? ObtenerTexto(IDictionary<string, object> fila, params string[] claves)
    {
        foreach (var clave in claves)
        {
            if (!fila.TryGetValue(clave, out var valor) || valor is null || valor is DBNull)
            {
                continue;
            }

            var texto = valor.ToString()?.Trim();
            if (!string.IsNullOrWhiteSpace(texto))
            {
                return texto;
            }
        }

        return null;
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
}
