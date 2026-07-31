using Arce.Web.Data;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Arce.Web.Data.Inspecciones.SubContrata;

public class SubContrataRepository : ISubContrataRepository
{
    private readonly string _connectionString;

    public SubContrataRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<SubContrataEntity>?> ListarSubContrata(int? Id, string? Nombre, string? Estado)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@SubContrata_Id", Id ?? 0);
        parametros.Add("@SubContrata_Nombre", Nombre ?? string.Empty);
        parametros.Add("@Estado", NormalizarEstado(Estado) ?? "A");

        var filas = await connection.QueryAsync(
            "[dbo].[SP_Filtrar_SubContrata]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        return filas.Select(MapearSubContrataDesdeFila).ToList();
    }

    public async Task<IEnumerable<SubContrataEntity>?> ConsultarDatosSubContrata(int? SubContrata_Id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@SubContrata_Id", SubContrata_Id);

        var filas = await connection.QueryAsync(
            "[dbo].[SP_Mostrar_Actualizar_SubContrata]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        return filas.Select(fila =>
        {
            var dict = (IDictionary<string, object>)fila;
            return new SubContrataEntity
            {
                Id = ObtenerEntero(dict, "Id", "SubContrata_Id", "subcontrata_id"),
                Nombre = ObtenerTexto(dict, "SubContrata_Nombre", "Nombre", "subContrata_Nombre", "nombre"),
                Estado = ObtenerTexto(dict, "Estado", "estado", "Flg_Est", "flg_est")
            };
        });
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarSubContrata(SubContrataEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@SubContrata_Nombre", valores.Nombre);
        parametros.Add("@Usr_Reg", valores.Usr_Reg);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Insertar_SubContrata]",
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

    public async Task<(int Codigo, string Mensaje)> ActualizarSubContrata(SubContrataEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@SubContrata_Id", valores.Id);
        parametros.Add("@SubContrata_Nombre", valores.Nombre);
        parametros.Add("@Usr_Mod", valores.Usr_Mod);
        parametros.Add("@Estado", NormalizarEstado(valores.Estado));

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Actualizar_SubContrata]",
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

    public async Task<(int Codigo, string Mensaje)> EliminarSubContrata(int? Id, string? Usr_Mod)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@SubContrata_Id", Id);
        parametros.Add("@Usr_Mod", Usr_Mod);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Eliminar_SubContrata]",
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

    private static SubContrataEntity MapearSubContrataDesdeFila(object fila)
    {
        var dict = (IDictionary<string, object>)fila;

        return new SubContrataEntity
        {
            Id = ObtenerEntero(dict, "Id", "SubContrata_Id", "subcontrata_id"),
            Nombre = ObtenerTexto(dict, "Nombre", "SubContrata_Nombre", "subcontrata_nombre"),
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
