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

    public async Task<IEnumerable<JefeEntity>?> ListarJefe(int? Id, string? Nombre, string? Dni, string? Estado)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var estadoNormalizado = NormalizarEstado(Estado);

        if (!string.IsNullOrWhiteSpace(estadoNormalizado))
        {
            var parametrosEstado = new DynamicParameters();
            parametrosEstado.Add("@Estado", estadoNormalizado);

            var filasEstado = await connection.QueryAsync(
                "[dbo].[SP_Filtrar_Estado_Jefe]",
                parametrosEstado,
                commandType: CommandType.StoredProcedure
            );

            var resultadoEstado = filasEstado
                .Select(MapearJefeDesdeFila)
                .Where(jefe =>
                    CoincideId(jefe.Id, Id) &&
                    CoincideTexto(jefe.Nombre, Nombre) &&
                    CoincideTexto(jefe.Dni, Dni))
                .OrderByDescending(jefe => jefe.Id ?? 0)
                .ToList();

            return resultadoEstado;
        }

        var parametros = new DynamicParameters();
        parametros.Add("@Id", Id);
        parametros.Add("@Nombre", Nombre);
        parametros.Add("@Dni", Dni);
        parametros.Add("@Estado", estadoNormalizado);

        var sql = @"
SELECT
    t1.Jefe_Id AS Id,
    t1.Jef_Nombre AS Nombre,
    t1.Jef_DNI AS Dni,
    t2.Cen_Cos_Des AS Area,
    t1.Estado AS Estado,
    t1.Cen_Cos_Id AS Cen_Cos_Id,
    t1.Usr_Reg AS Usr_Reg,
    t1.Fec_Reg AS Fec_Reg
FROM Ins_Jefe t1
INNER JOIN Lg_Cen_Cos t2
    ON t1.Cen_Cos_Id = t2.Cen_Cos_Id
WHERE
    (@Id IS NULL OR t1.Jefe_Id = @Id)
    AND (@Nombre IS NULL OR LTRIM(RTRIM(@Nombre)) = '' OR t1.Jef_Nombre LIKE '%' + @Nombre + '%')
    AND (@Dni IS NULL OR LTRIM(RTRIM(@Dni)) = '' OR t1.Jef_DNI LIKE '%' + @Dni + '%')
    AND (@Estado IS NULL OR LTRIM(RTRIM(@Estado)) = '' OR t1.Estado = @Estado)
ORDER BY t1.Jefe_Id DESC;";

        var result = await connection.QueryAsync<JefeEntity>(sql, parametros, commandType: CommandType.Text);
        return result;
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

    private static bool CoincideId(int? valor, int? filtro)
    {
        return !filtro.HasValue || valor == filtro;
    }

    private static bool CoincideTexto(string? valor, string? filtro)
    {
        if (string.IsNullOrWhiteSpace(filtro))
        {
            return true;
        }

        return (valor ?? string.Empty).Contains(filtro.Trim(), StringComparison.OrdinalIgnoreCase);
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