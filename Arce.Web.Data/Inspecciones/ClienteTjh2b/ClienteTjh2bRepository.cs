using Arce.Web.Data;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Arce.Web.Data.Inspecciones.ClienteTjh2b;

public class ClienteTjh2bRepository : IClienteTjh2bRepository
{
    private readonly string _connectionString;

    public ClienteTjh2bRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<ClienteTjh2bEntity>?> ListarClienteTjh2b(int? Id, string? Nombre, string? Estado)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var sql = @"
SELECT
    t1.Cliente_Id,
    t1.Cliente_Nombre,
    t1.Estado
FROM TJH2B_Cliente t1
WHERE
    (@Cliente_Id = 0 OR t1.Cliente_Id = @Cliente_Id)
    AND (@Cliente_Nombre = '' OR t1.Cliente_Nombre LIKE '%' + @Cliente_Nombre + '%')
    AND (@Estado = '' OR t1.Estado = @Estado)
ORDER BY t1.Cliente_Nombre;";

        var parametros = new DynamicParameters();
        parametros.Add("@Cliente_Id", Id ?? 0);
        parametros.Add("@Cliente_Nombre", Nombre ?? string.Empty);
        parametros.Add("@Estado", NormalizarEstado(Estado) ?? string.Empty);

        var result = await connection.QueryAsync(sql, parametros);
        return result
            .OfType<IDictionary<string, object>>()
            .Select(MapCliente);
    }

    public async Task<IEnumerable<ClienteTjh2bEntity>?> ConsultarDatosClienteTjh2b(int? Cliente_Id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Cliente_Id", Cliente_Id);

        var result = await connection.QueryAsync(
            "[dbo].[SP_Mostrar_Actualizar_TJH2B_Cliente]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        return result
            .OfType<IDictionary<string, object>>()
            .Select(MapCliente);
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarClienteTjh2b(ClienteTjh2bEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Cliente_Nombre", valores.Nombre);
        parametros.Add("@Usr_Reg", valores.Usr_Reg);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Insertar_TJH2B_Cliente]",
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

    public async Task<(int Codigo, string Mensaje)> ActualizarClienteTjh2b(ClienteTjh2bEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Cliente_Id", valores.Id);
        parametros.Add("@Cliente_Nombre", valores.Nombre);
        parametros.Add("@Usr_Mod", valores.Usr_Mod);
        parametros.Add("@Estado", NormalizarEstado(valores.Estado));

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Actualizar_TJH2B_Cliente]",
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

    public async Task<(int Codigo, string Mensaje)> EliminarClienteTjh2b(int? Id, string? Usr_Mod)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Cliente_Id", Id);
        parametros.Add("@Usr_Mod", Usr_Mod);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Eliminar_TJH2B_Cliente]",
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

    private static ClienteTjh2bEntity MapCliente(IDictionary<string, object> row)
    {
        return new ClienteTjh2bEntity
        {
            Id = GetInt32(row, "Cliente_Id"),
            Nombre = GetString(row, "Cliente_Nombre") ?? GetString(row, "Nombre"),
            Estado = GetString(row, "Estado")
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
