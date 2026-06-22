using Arce.Web.Data;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Arce.Web.Data.Inspecciones.Cliente;

public class ClienteRepository : IClienteRepository
{
    private readonly string _connectionString;

    public ClienteRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<ClienteEntity>?> ListarCliente(int? Id, string? Nombre, string? Estado)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var estadoNormalizado = NormalizarEstado(Estado);

        var parametros = new DynamicParameters();
        parametros.Add("@Id", Id);
        parametros.Add("@Nombre", Nombre);
        parametros.Add("@Estado", estadoNormalizado);

        const string sql = @"
SELECT
    t1.Cliente_Id,
    t1.Cliente_Nombre,
    t1.Estado
FROM Ins_Cliente t1
WHERE
    (@Id IS NULL OR t1.Cliente_Id = @Id)
    AND (@Nombre IS NULL OR LTRIM(RTRIM(@Nombre)) = '' OR t1.Cliente_Nombre LIKE '%' + @Nombre + '%')
    AND (@Estado IS NULL OR LTRIM(RTRIM(@Estado)) = '' OR t1.Estado = @Estado)
ORDER BY t1.Cliente_Id DESC;";

        var result = await connection.QueryAsync(sql, parametros, commandType: CommandType.Text);

        return result
            .OfType<IDictionary<string, object>>()
            .Select(MapCliente);
    }

    public async Task<IEnumerable<ClienteEntity>?> ConsultarDatosCliente(int? Cliente_Id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Cliente_Id", Cliente_Id);

        const string sql = @"
SELECT
    t1.Cliente_Id,
    t1.Cliente_Nombre,
    t1.Estado
FROM Ins_Cliente t1
WHERE t1.Cliente_Id = @Cliente_Id;";

        var result = await connection.QueryAsync(sql, parametros, commandType: CommandType.Text);

        return result
            .OfType<IDictionary<string, object>>()
            .Select(MapCliente);
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarCliente(ClienteEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Cliente_Nombre", valores.Nombre);
        parametros.Add("@Usr_Reg", valores.Usr_Reg);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Insertar_Cliente]",
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

    public async Task<(int Codigo, string Mensaje)> ActualizarCliente(ClienteEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Cliente_Id", valores.Id);
        parametros.Add("@Cliente_Nombre", valores.Nombre);
        parametros.Add("@Estado", NormalizarEstado(valores.Estado));
        parametros.Add("@Usr_Mod", valores.Usr_Mod);

        const string sql = @"
UPDATE Ins_Cliente
SET Cliente_Nombre = @Cliente_Nombre,
    Estado = @Estado,
    Usr_Mod = @Usr_Mod,
    Fec_Mod = GETDATE()
WHERE Cliente_Id = @Cliente_Id;";

        try
        {
            await connection.ExecuteAsync(sql, parametros, commandType: CommandType.Text);
            return (0, string.Empty);
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
    }

    public async Task<(int Codigo, string Mensaje)> EliminarCliente(int? Id, string? Usr_Mod)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Cliente_Id", Id);
        parametros.Add("@Usr_Mod", Usr_Mod);

        const string sql = @"
UPDATE Ins_Cliente
SET Estado = 'I',
    Usr_Mod = @Usr_Mod,
    Fec_Mod = GETDATE()
WHERE Cliente_Id = @Cliente_Id;";

        try
        {
            await connection.ExecuteAsync(sql, parametros, commandType: CommandType.Text);
            return (0, string.Empty);
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
    }

    private static ClienteEntity MapCliente(IDictionary<string, object> row)
    {
        return new ClienteEntity
        {
            Id = GetInt32(row, "Cliente_Id"),
            Nombre = GetString(row, "Cliente_Nombre"),
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