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

    public async Task<IEnumerable<JefeEntity>?> ListarJefe(int? Id, string? Nombre, string? Dni, string? Estado, int? Cen_Cos_Id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Jefe_Id", Id ?? 0);
        parametros.Add("@Jef_Nombre", Nombre ?? string.Empty);
        parametros.Add("@Jef_DNI", Dni ?? string.Empty);
        parametros.Add("@Estado", NormalizarEstado(Estado) ?? "A");
        parametros.Add("@Cen_Cos_Id", Cen_Cos_Id ?? 0);

        var filas = await connection.QueryAsync(
            "[dbo].[SP_Filtrar_Jefe]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        var resultado = filas
            .Select(MapearJefeDesdeFila)
            .ToList();

        return resultado;
    }

    public async Task<IEnumerable<JefeEntity>?> ConsultarDatosJefe(int? Jefe_Id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Jefe_Id", Jefe_Id);

        // El SP devuelve las columnas Jef_Nombre, Jef_DNI, Cen_Cos_Des.
        // JefeEntity tiene las propiedades Nombre, Dni, Cen_Cos_Des, por lo que
        // se mapea manualmente para evitar que Dapper deje Nombre/Dni en null
        // (Cen_Cos_Des sí mapea directo porque el nombre coincide exacto).
        var filas = await connection.QueryAsync(
            "[dbo].[SP_Consultar_Datos_Jefe_Actualizar]",
            parametros,
            commandType: CommandType.StoredProcedure
        );

        var result = filas.Select(fila =>
        {
            var dict = (IDictionary<string, object>)fila;
            return new JefeEntity
            {
                Id = Jefe_Id,
                Nombre = ObtenerTexto(dict, "Jef_Nombre", "Nombre", "jef_Nombre", "nombre"),
                Dni = ObtenerTexto(dict, "Jef_DNI", "Dni", "dni"),
                Cen_Cos_Des = ObtenerTexto(dict, "Cen_Cos_Des", "Area", "area"),
                Area = ObtenerTexto(dict, "Cen_Cos_Des", "Area", "area"),
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
        parametros.Add("@Jef_Nombre", valores.Nombre);
        parametros.Add("@Jef_DNI", valores.Dni);
        parametros.Add("@Cen_Cos_Id", valores.Cen_Cos_Id);
        parametros.Add("@Usr_Reg", valores.Usr_Reg);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Insertar_Jefe]",
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
        parametros.Add("@Jefe_Id", valores.Id);
        parametros.Add("@Jef_Nombre", valores.Nombre);
        parametros.Add("@Cen_Cos_Id", valores.Cen_Cos_Id);
        parametros.Add("@Estado", valores.Estado);
        parametros.Add("@Jef_DNI", valores.Dni);
        parametros.Add("@Usr_Mod", valores.Usr_Mod);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Actualizar_Jefe]",
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
        parametros.Add("@Jefe_Id", Id);
        parametros.Add("@Usr_Mod", Usr_Mod);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Eliminar_Jefe]",
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
            Id = ObtenerEntero(dict, "Id", "Jefe_Id", "jefe_id"),
            Nombre = ObtenerTexto(dict, "Nombre", "Jef_Nombre", "jef_nombre"),
            Dni = ObtenerTexto(dict, "Dni", "Jef_DNI", "jef_dni"),
            Area = ObtenerTexto(dict, "Area", "Cen_Cos_Des", "cen_cos_des"),
            Cen_Cos_Des = ObtenerTexto(dict, "Area", "Cen_Cos_Des", "cen_cos_des"),
            Estado = ObtenerTexto(dict, "Estado", "estado", "Flg_Est", "flg_est"),
            Cen_Cos_Id = ObtenerEntero(dict, "Cen_Cos_Id", "cen_cos_id")
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
