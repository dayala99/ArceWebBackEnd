using Arce.Web.Data;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Arce.Web.Data.Inspecciones.Jefe;

public class JefeRepository : IJefeRepository
{
    private readonly string _connectionString;

    public JefeRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<JefeEntity>?> ListarJefe(int? Id, string? Reporte_Tipo, string? Estado)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Reporte_Id", Id ?? 0);
        parametros.Add("@Reporte_Tipo", Reporte_Tipo ?? string.Empty);
        parametros.Add("@Estado", NormalizarEstado(Estado) ?? "A");

        Console.WriteLine("[TipoReporte][Repository] SP_Filtrar_Tipo_Reporte -> filtros:");
        Console.WriteLine($"  Reporte_Id: {Id ?? 0}");
        Console.WriteLine($"  Reporte_Tipo: {Reporte_Tipo ?? string.Empty}");
        Console.WriteLine($"  Estado: {NormalizarEstado(Estado) ?? "A"}");

        var filas = await connection.QueryAsync(
            "[dbo].[SP_Filtrar_Tipo_Reporte]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        var resultado = filas
            .Select(MapearJefeDesdeFila)
            .ToList();

        return resultado;
    }

    public async Task<IEnumerable<JefeEntity>?> ConsultarDatosJefe(int? Reporte_Id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Reporte_Id", Reporte_Id);

        Console.WriteLine("[TipoReporte][Repository] SP_Mostrar_Actualizar_Tipo_Reporte -> id:");
        Console.WriteLine($"  Reporte_Id: {Reporte_Id}");

        var filas = await connection.QueryAsync(
            "[dbo].[SP_Mostrar_Actualizar_Tipo_Reporte]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        var result = filas.Select(fila =>
        {
            var dict = (IDictionary<string, object>)fila;
            return new JefeEntity
            {
                Reporte_Id = Reporte_Id,
                Reporte_Tipo = ObtenerTexto(dict, "Reporte_Tipo", "reporte_tipo", "Tipo_Reporte", "tipo_reporte"),
                Estado = ObtenerTexto(dict, "Estado", "estado")
            };
        });

        return result;
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarJefe(JefeEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Reporte_Tipo", valores.Reporte_Tipo);
        parametros.Add("@Usr_Reg", valores.Usr_Reg);

        Console.WriteLine("[TipoReporte][Repository] SP_Insertar_Tipo_Reporte -> datos:");
        Console.WriteLine($"  Reporte_Tipo: {valores.Reporte_Tipo}");
        Console.WriteLine($"  Usr_Reg: {valores.Usr_Reg}");

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Insertar_Tipo_Reporte]",
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

    public async Task<(int Codigo, string Mensaje)> ActualizarJefe(JefeEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Reporte_Id", valores.Reporte_Id);
        parametros.Add("@Reporte_Tipo", valores.Reporte_Tipo);
        parametros.Add("@Usr_Mod", valores.Usr_Mod);
        parametros.Add("@Estado", NormalizarEstado(valores.Estado) ?? "A");

        Console.WriteLine("[TipoReporte][Repository] SP_Actualizar_Tipo_Reporte -> datos:");
        Console.WriteLine($"  Reporte_Id: {valores.Reporte_Id}");
        Console.WriteLine($"  Reporte_Tipo: {valores.Reporte_Tipo}");
        Console.WriteLine($"  Estado: {NormalizarEstado(valores.Estado) ?? "A"}");
        Console.WriteLine($"  Usr_Mod: {valores.Usr_Mod}");

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Actualizar_Tipo_Reporte]",
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

    public async Task<(int Codigo, string Mensaje)> EliminarJefe(int? Id, string? Usr_Mod)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Tipo_Reporte_Id", Id);
        parametros.Add("@Usr_Mod", Usr_Mod);

        Console.WriteLine("[TipoReporte][Repository] SP_Eliminar_Tipo_Reporte -> datos:");
        Console.WriteLine($"  Tipo_Reporte_Id: {Id}");
        Console.WriteLine($"  Usr_Mod: {Usr_Mod}");

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Eliminar_Tipo_Reporte]",
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

    private static JefeEntity MapearJefeDesdeFila(object fila)
    {
        var dict = (IDictionary<string, object>)fila;

        return new JefeEntity
        {
            Reporte_Id = ObtenerEntero(dict, "Reporte_Id", "Id", "reporte_id"),
            Reporte_Tipo = ObtenerTexto(dict, "Reporte_Tipo", "reporte_tipo", "Tipo_Reporte", "tipo_reporte"),
            Estado = ObtenerTexto(dict, "Estado", "estado", "Flg_Est", "flg_est")
        };
    }

    private static int? ObtenerEntero(IDictionary<string, object> fila, params string[] claves)
    {
        foreach (var clave in claves)
        {
            if (!fila.TryGetValue(clave, out var valor) || valor is null)
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
            if (!fila.TryGetValue(clave, out var valor) || valor is null)
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
