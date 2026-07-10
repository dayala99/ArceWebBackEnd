using Arce.Web.Data;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Arce.Web.Data.Inspecciones.TipoReporte;

public class TipoReporteRepository : ITipoReporteRepository
{
    private readonly string _connectionString;

    public TipoReporteRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<TipoReporteEntity>?> ListarTipoReporte(int? Reporte_Id, string? Reporte_Tipo, string? Estado)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Reporte_Id", Reporte_Id ?? 0);
        parametros.Add("@Reporte_Tipo", Reporte_Tipo ?? string.Empty);
        parametros.Add("@Estado", NormalizarEstado(Estado) ?? "A");

        var filas = await connection.QueryAsync<TipoReporteEntity>(
            "[dbo].[SP_Filtrar_Tipo_Reporte]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        return filas.ToList();
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarTipoReporte(TipoReporteEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Reporte_Tipo", valores.Reporte_Tipo);
        parametros.Add("@Usr_Reg", valores.Usr_Reg);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Insertar_Tipo_Reporte]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return (0, "Completado con éxito");
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
        catch (Exception ex)
        {
            return (1, ex.Message);
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarTipoReporte(TipoReporteEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        const string sql = @"
            UPDATE Ins_Tipo_Reporte
            SET
                Reporte_Tipo = @Reporte_Tipo,
                Estado       = @Estado,
                Usr_Mod      = @Usr_Mod,
                Fec_Mod      = GETDATE()
            WHERE Reporte_Id = @Reporte_Id;";

        var parametros = new DynamicParameters();
        parametros.Add("@Reporte_Id", valores.Reporte_Id);
        parametros.Add("@Reporte_Tipo", valores.Reporte_Tipo);
        parametros.Add("@Estado", NormalizarEstado(valores.Estado) ?? "A");
        parametros.Add("@Usr_Mod", valores.Usr_Mod);

        try
        {
            await connection.ExecuteAsync(sql, parametros, commandType: CommandType.Text);
            return (0, "Completado con éxito");
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
        catch (Exception ex)
        {
            return (1, ex.Message);
        }
    }

    public async Task<(int Codigo, string Mensaje)> EliminarTipoReporte(int? Tipo_Reporte_Id, string? Usr_Mod)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Tipo_Reporte_Id", Tipo_Reporte_Id ?? 0);
        parametros.Add("@Usr_Mod", Usr_Mod);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Eliminar_Tipo_Reporte]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return (0, "Completado con éxito");
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
        catch (Exception ex)
        {
            return (1, ex.Message);
        }
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
