using Arce.Web.Entity.Inspecciones;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Arce.Web.Data.Inspecciones.TipoRiesgo;

public class TipoRiesgoRepository : ITipoRiesgoRepository
{
    private readonly string _connectionString;

    public TipoRiesgoRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<TipoRiesgoEntity>?> ListarTipoRiesgo(int? Id, string? Nombre, string? Estado)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Tipo_Riesgo_Id", Id ?? 0);
        parametros.Add("@Tipo_Riesgo", Nombre ?? string.Empty);
        parametros.Add("@Estado", NormalizarEstado(Estado) ?? "A");

        var filas = await connection.QueryAsync(
            "[dbo].[SP_Filtrar_Tipo_Riesgo]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        return filas
            .Select(MapearTipoRiesgoDesdeFila)
            .ToList();
    }

    public async Task<IEnumerable<TipoRiesgoEntity>?> ConsultarDatosTipoRiesgo(int? Tipo_Riesgo_Id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Tipo_Riesgo_Id", Tipo_Riesgo_Id);

        var filas = await connection.QueryAsync(
            "[dbo].[SP_Mostrar_Actualizar_Tipo_Riesgo]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        return filas.Select(fila =>
        {
            var dict = (IDictionary<string, object>)fila;
            return new TipoRiesgoEntity
            {
                Id = Tipo_Riesgo_Id,
                Nombre = ObtenerTexto(dict, "Tipo_Riesgo", "Nombre", "tipo_Nombre", "nombre"),
                Estado = ObtenerTexto(dict, "Estado", "estado", "Flg_Est", "flg_est")
            };
        });
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarTipoRiesgo(TipoRiesgoEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Tipo_Riesgo", valores.Nombre);
        parametros.Add("@Usr_Reg", valores.Usr_Reg);

        try
        {
            var afectados = await connection.ExecuteAsync(
                "[dbo].[SP_Insertar_Tipo_Riesgo]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            if (afectados <= 0)
            {
                return (1, "No se pudo registrar el tipo de riesgo.");
            }

            return (0, string.Empty);
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarTipoRiesgo(TipoRiesgoEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Tipo_Riesgo_Id", valores.Id);
        parametros.Add("@Tipo_Riesgo", valores.Nombre);
        parametros.Add("@Usr_Reg", valores.Usr_Mod ?? valores.Usr_Reg);
        parametros.Add("@Estado", NormalizarEstado(valores.Estado));

        try
        {
            var afectados = await connection.ExecuteAsync(
                "[dbo].[SP_Actualizar_Tipo_Riesgo]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            if (afectados <= 0)
            {
                return (1, "No se encontró el tipo de riesgo para actualizar.");
            }

            return (0, string.Empty);
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
    }

    public async Task<(int Codigo, string Mensaje)> EliminarTipoRiesgo(int? Id, string? Usr_Mod)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Tipo_Riesgo_Id", Id);
        parametros.Add("@Usr_Mod", Usr_Mod);

        try
        {
            var afectados = await connection.ExecuteAsync(
                "[dbo].[SP_Eliminar_Tipo_Riesgo]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            if (afectados <= 0)
            {
                return (1, "No se encontró el tipo de riesgo para eliminar.");
            }

            return (0, string.Empty);
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
    }

    private static TipoRiesgoEntity MapearTipoRiesgoDesdeFila(object fila)
    {
        var dict = (IDictionary<string, object>)fila;

        return new TipoRiesgoEntity
        {
            Id = ObtenerEntero(dict, "Tipo_Riesgo_Id", "Id", "tipo_id"),
            Nombre = ObtenerTexto(dict, "Tipo_Riesgo", "Nombre", "tipo_Nombre", "nombre"),
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
