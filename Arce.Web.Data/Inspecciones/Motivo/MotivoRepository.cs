using Arce.Web.Data;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Arce.Web.Data.Inspecciones.Motivo;

public class MotivoRepository : IMotivoRepository
{
    private readonly string _connectionString;

    public MotivoRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<MotivoEntity>?> ListarMotivo(int? Id, string? Nombre, string? Estado)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Motivo_Id", Id ?? 0);
        parametros.Add("@Motivo_Nombre", Nombre ?? string.Empty);
        parametros.Add("@Estado", NormalizarEstado(Estado) ?? "A");

        var filas = await connection.QueryAsync(
            "[dbo].[SP_Filtrar_Motivo]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        return filas
            .Select(MapearMotivoDesdeFila)
            .ToList();
    }

    public async Task<IEnumerable<MotivoEntity>?> ConsultarDatosMotivo(int? Motivo_Id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Motivo_Id", Motivo_Id);

        var filas = await connection.QueryAsync(
            "[dbo].[Mostrar_Actualizar_Motivo]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        return filas.Select(fila =>
        {
            var dict = (IDictionary<string, object>)fila;
            return new MotivoEntity
            {
                Id = Motivo_Id,
                Nombre = ObtenerTexto(dict, "Motivo_Nombre", "Nombre", "motivo_Nombre", "nombre"),
                Estado = ObtenerTexto(dict, "Estado", "estado", "Flg_Est", "flg_est")
            };
        });
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarMotivo(MotivoEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Motivo_Nombre", valores.Nombre);
        parametros.Add("@Usr_Reg", valores.Usr_Reg);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Insertar_Motivo]",
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

    public async Task<(int Codigo, string Mensaje)> ActualizarMotivo(MotivoEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Motivo_Id", valores.Id);
        parametros.Add("@Motivo_Nombre", valores.Nombre);
        parametros.Add("@Estado", NormalizarEstado(valores.Estado));
        parametros.Add("@Usr_Mod", valores.Usr_Mod);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Actualizar_Motivo]",
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

    public async Task<(int Codigo, string Mensaje)> EliminarMotivo(int? Id, string? Usr_Mod)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Motivo_Id", Id);
        parametros.Add("@Usr_Mod", Usr_Mod);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Eliminar_Motivo]",
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

    private static MotivoEntity MapearMotivoDesdeFila(object fila)
    {
        var dict = (IDictionary<string, object>)fila;

        return new MotivoEntity
        {
            Id = ObtenerEntero(dict, "Id", "Motivo_Id", "motivo_id"),
            Nombre = ObtenerTexto(dict, "Nombre", "Motivo_Nombre", "motivo_nombre"),
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
        return limpio == "A" ? "A" : limpio == "I" ? "I" : estado.Trim();
    }
}
