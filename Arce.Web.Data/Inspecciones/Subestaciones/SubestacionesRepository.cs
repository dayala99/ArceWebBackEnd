using Arce.Web.Data;
using Arce.Web.Entity.Inspecciones;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Arce.Web.Data.Inspecciones.Subestaciones;

public class SubestacionesRepository : ISubestacionesRepository
{
    private readonly string _connectionString;

    public SubestacionesRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<SubEstacionEntity>?> ListarSubEstaciones(int? Id, string? Nombre, int? Cliente_Id, string? Estado)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Subestacion_Id", Id ?? 0);
        parametros.Add("@Subestacion_Nombre", string.IsNullOrWhiteSpace(Nombre) ? string.Empty : Nombre.Trim());
        parametros.Add("@Cliente_Id", Cliente_Id ?? 0);
        parametros.Add("@Estado", NormalizarEstado(Estado));

        var result = await connection.QueryAsync<SubEstacionEntity>(
            "[dbo].[SP_Filtrar_Subestaciones]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        return result;
    }

    public async Task<IEnumerable<SubEstacionEntity>?> ConsultarEditarSubEstaciones(int? Subestacion_Id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Subestacion_Id", Subestacion_Id);

        var result = await connection.QueryAsync<SubEstacionEntity>(
            "[dbo].[SP_Consultar_Editar_Subestaciones]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        return result;
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarSubEstacion(SubEstacionEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Subestacion_Nombre", valores.Subestacion_Nombre);
        parametros.Add("@Cliente_Id", valores.Cliente_Id);
        parametros.Add("@Usr_Reg", valores.Usr_Reg);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Insertar_Subestaciones]",
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

    public async Task<(int Codigo, string Mensaje)> ActualizarSubEstacion(SubEstacionEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Subestacion_Id", valores.Subestacion_Id);
        parametros.Add("@Subestacion_Nombre", valores.Subestacion_Nombre);
        parametros.Add("@Cliente_Id", valores.Cliente_Id);
        parametros.Add("@Usr_Mod", valores.Usr_Mod);
        parametros.Add("@Estado", NormalizarEstado(valores.Estado));

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Editar_Subestaciones]",
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

    private static string NormalizarEstado(string? estado)
    {
        if (string.IsNullOrWhiteSpace(estado))
        {
            return "A";
        }

        var limpio = estado.Trim().ToUpperInvariant();

        if (limpio.StartsWith("I"))
        {
            return "I";
        }

        return "A";
    }
}
