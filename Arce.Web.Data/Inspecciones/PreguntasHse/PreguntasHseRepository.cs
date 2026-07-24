using Arce.Web.Entity.Inspecciones;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Arce.Web.Data.Inspecciones.PreguntasHse;

public class PreguntasHseRepository : IPreguntasHseRepository
{
    private readonly string _connectionString;

    public PreguntasHseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<PreguntasHseEntity>?> ListarPreguntasHse(int? Pregunta_Id, string? Pregunta_Nombre, string? Estado)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Pregunta_Id", Pregunta_Id);
        parametros.Add("@Pregunta_Nombre", Pregunta_Nombre ?? string.Empty);
        parametros.Add("@Estado", NormalizarEstado(Estado) ?? "A");

        var filas = await connection.QueryAsync(
            "[dbo].[SP_Filtrar_Pregunta_Centro_HSE]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        return filas.Select(MapearDesdeFila).ToList();
    }

    public async Task<IEnumerable<PreguntasHseEntity>?> ConsultarDatosPreguntasHse(int? Pregunta_Id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Pregunta_Id", Pregunta_Id);

        var filas = await connection.QueryAsync(
            "[dbo].[SP_Mostrar_Actualizar_Pregunta_Centro_HSE]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        return filas.Select(fila =>
        {
            var dict = (IDictionary<string, object>)fila;
            return new PreguntasHseEntity
            {
                Id = Pregunta_Id,
                Pregunta_Nombre = ObtenerTexto(dict, "Pregunta_Nombre", "Nombre", "pregunta_Nombre", "nombre"),
                Estado = ObtenerTexto(dict, "Estado", "estado", "Flg_Est", "flg_est")
            };
        });
    }

    public async Task<IEnumerable<PreguntasHseEntity>?> ListarPreguntasHseSinEstado()
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        const string sql = @"
SELECT t1.Pregunta_Id,
       t1.Pregunta_Nombre
FROM Ins_Preguntas_Centro_HSE t1
ORDER BY t1.Pregunta_Id;";

        var filas = await connection.QueryAsync(sql);
        return filas.Select(fila =>
        {
            var dict = (IDictionary<string, object>)fila;
            return new PreguntasHseEntity
            {
                Id = ObtenerEntero(dict, "Pregunta_Id", "Id", "pregunta_id"),
                Pregunta_Nombre = ObtenerTexto(dict, "Pregunta_Nombre", "Nombre", "pregunta_Nombre", "nombre")
            };
        }).ToList();
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarPreguntasHse(PreguntasHseEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Pregunta_Nombre", valores.Pregunta_Nombre ?? valores.Nombre);
        parametros.Add("@Usr_Reg", valores.Usr_Reg);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Insertar_Pregunta_Centro_HSE]",
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

    public async Task<(int Codigo, string Mensaje)> ActualizarPreguntasHse(PreguntasHseEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Pregunta_Id", valores.Id);
        parametros.Add("@Pregunta_Nombre", valores.Pregunta_Nombre ?? valores.Nombre);
        parametros.Add("@Usr_Mod", valores.Usr_Mod ?? valores.Usr_Reg);
        parametros.Add("@Estado", NormalizarEstado(valores.Estado));

        try
        {
            var afectados = await connection.ExecuteAsync(
                "[dbo].[SP_Actualizar_Pregunta_Centro_HSE]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            if (afectados <= 0)
            {
                return (1, "No se encontró la Pregunta HSE para actualizar.");
            }

            return (0, string.Empty);
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
    }

    public async Task<(int Codigo, string Mensaje)> EliminarPreguntasHse(int? Pregunta_Id, string? Usr_Mod)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Pregunta_Id", Pregunta_Id);
        parametros.Add("@Usr_Mod", Usr_Mod);

        try
        {
            var afectados = await connection.ExecuteAsync(
                "[dbo].[SP_Eliminar_Pregunta_Centro_HSE]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            if (afectados <= 0)
            {
                return (1, "No se encontró la Pregunta HSE para eliminar.");
            }

            return (0, string.Empty);
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
    }

    private static PreguntasHseEntity MapearDesdeFila(object fila)
    {
        var dict = (IDictionary<string, object>)fila;

        return new PreguntasHseEntity
        {
            Id = ObtenerEntero(dict, "Pregunta_Id", "Id", "pregunta_id"),
            Pregunta_Nombre = ObtenerTexto(dict, "Pregunta_Nombre", "Nombre", "pregunta_Nombre", "nombre"),
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
