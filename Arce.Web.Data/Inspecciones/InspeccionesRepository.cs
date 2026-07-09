using Arce.Web.Entity.Inspecciones;
using Arce.Web.Entity.Usuario;
using System;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Arce.Web.Data;

public class InspeccionesRepository : IInspeccionesRepository
{
    public readonly string _connectionString;
    private readonly ILogger<InspeccionesRepository> _logger;

    public InspeccionesRepository(IConfiguration configuration, ILogger<InspeccionesRepository> logger)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
        _logger = logger;
    }


    private async Task<bool> ExisteWeReportAsync(SqlConnection connection, int? weReportId)
    {
        if (!weReportId.HasValue || weReportId.Value <= 0)
        {
            return false;
        }

        var existe = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(1) FROM Ins_We_Report WHERE We_Report_Id = @We_Report_Id",
            new { We_Report_Id = weReportId.Value },
            commandType: CommandType.Text
        );

        return existe > 0;
    }

    public Task<IEnumerable<InspeccionEntity>?> ListarInspeccionesAsync()
    {
        IEnumerable<InspeccionEntity> result = Array.Empty<InspeccionEntity>();
        return Task.FromResult<IEnumerable<InspeccionEntity>?>(result);
    }

    // FIX: Se reemplazó el SP (que usa INNER JOIN en Ins_Cargo) por SQL inline con LEFT JOIN
    // para que siempre devuelva el registro aunque el usuario no tenga cargo asignado.
    public async Task<IEnumerable<UsuarioEntity>?> ConsultarDatosUsuario(string? Usr_Cod)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Usr_Cod", Usr_Cod);
            var result = await connection.QueryAsync<UsuarioEntity>(
                @"SELECT
                    t1.Usr_Nom,
                    t1.Usr_Doc_Nro,
                    t3.Cargo_Nombre,
                    t2.Cen_Cos_Des,
                    t1.Usr_Cen_Cos_Id,
                    t1.Usr_Corr
                FROM Sg_Usuario t1
                LEFT JOIN Lg_Cen_Cos t2
                    ON (t1.Usr_Cen_Cos_Id = t2.Cen_Cos_Id)
                LEFT JOIN Ins_Cargo t3
                    ON (t1.Usr_Crg = t3.Cargo_Id)
                WHERE t1.Usr_Cod = @Usr_Cod",
                parametros,
                commandType: CommandType.Text
            );
            return result;
        }
    }

    public async Task<IEnumerable<SubEstacionEntity>?> ListarSubEstacionesPorCliente(int Cliente_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Cliente_Id", Cliente_Id);

            var result = await connection.QueryAsync<SubEstacionEntity>(
                "SP_Consutar_Subestacion",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<IEnumerable<SubEstacionEntity>?> ListarSubEstaciones(int? Id, string? Nombre, int? Cliente_Id, string? Estado)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Subestacion_Id", Id ?? 0);
            parametros.Add("@Subestacion_Nombre", string.IsNullOrWhiteSpace(Nombre) ? string.Empty : Nombre.Trim());
            parametros.Add("@Cliente_Id", Cliente_Id ?? 0);
            parametros.Add("@Estado", NormalizarEstado(Estado));

            const string sql = @"
                SELECT
                    t1.Subestacion_Id,
                    t1.Subestacion_Nombre,
                    t1.Cliente_Id,
                    t2.Cliente_Nombre,
                    t1.Estado
                FROM Ins_SubEstacion t1
                LEFT JOIN Ins_Cliente t2
                    ON t1.Cliente_Id = t2.Cliente_Id
                WHERE (@Subestacion_Id = 0 OR t1.Subestacion_Id = @Subestacion_Id)
                  AND (@Cliente_Id = 0 OR t1.Cliente_Id = @Cliente_Id)
                  AND (LTRIM(RTRIM(@Subestacion_Nombre)) = '' OR t1.Subestacion_Nombre LIKE '%' + @Subestacion_Nombre + '%')
                  AND (@Estado = '' OR t1.Estado = @Estado)
                ORDER BY t1.Subestacion_Nombre";

            var result = await connection.QueryAsync<SubEstacionEntity>(
                sql,
                parametros,
                commandType: CommandType.Text
            );

            return result;
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

    // NUEVO: listado simple (sin filtros) de subestaciones, usado para llenar el combo de We Report
    public async Task<IEnumerable<SubEstacionEntity>?> ListarSubEstacionesReporte()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryAsync<SubEstacionEntity>(
                "SELECT t1.Subestacion_Id, t1.Subestacion_Nombre FROM Ins_SubEstacion t1 ORDER BY t1.Subestacion_Nombre",
                commandType: CommandType.Text
            );
            return result;
        }
    }

    public async Task<IEnumerable<InsClienteEntity>?> ListarClientes()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<InsClienteEntity>(
                "SELECT Cliente_Id, Cliente_Nombre FROM Ins_Cliente ORDER BY Cliente_Id",
                commandType: CommandType.Text
            );
        }
    }

    public async Task<IEnumerable<InsMotivoEntity>?> ListarMotivos()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<InsMotivoEntity>(
                "SELECT Motivo_Id, Motivo_Nombre FROM Ins_Motivo ORDER BY Motivo_Id",
                commandType: CommandType.Text
            );
        }
    }

    public async Task<IEnumerable<InsClimaEntity>?> ListarClimas()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<InsClimaEntity>(
                "SELECT Clima_Id, Clima_Nombre FROM Ins_Clima ORDER BY Clima_Id",
                commandType: CommandType.Text
            );
        }
    }

    public async Task<IEnumerable<InsTareaEntity>?> ListarTareas()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<InsTareaEntity>(
                "SELECT Tarea_Id, Tarea_Nombre FROM Ins_Tarea ORDER BY Tarea_Id",
                commandType: CommandType.Text
            );
        }
    }

    public async Task<IEnumerable<InsSubContrataEntity>?> ListarSubContratas()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<InsSubContrataEntity>(
                "SELECT SubContrata_Id, SubContrata_Nombre FROM Ins_SubContrata ORDER BY SubContrata_Nombre",
                commandType: CommandType.Text
            );
        }
    }

    public async Task<IEnumerable<InsJefeAreaEntity>?> ListarJefesArea()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<InsJefeAreaEntity>(
                @"SELECT t1.Usr_Nom, t1.Usr_Cod
                  FROM Sg_Usuario t1
                  JOIN Ins_Cargo t2 ON (t1.Usr_Crg = t2.Cargo_Id)
                  WHERE t2.Cargo_Nombre LIKE 'JEFE%'
                  ORDER BY t1.Usr_Nom",
                commandType: CommandType.Text
            );
        }
    }

    // Devuelve Cen_Cos_Des y Usr_Doc_Nro del jefe a partir de su Usr_Cod
    public async Task<InsJefeDatosEntity?> MostrarJefe(string Jefe_Cod)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Jefe_Cod", Jefe_Cod);

            var resultado = await connection.QueryFirstOrDefaultAsync<InsJefeDatosEntity>(
                "[dbo].[SP_Mostrar_Jefe]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return resultado;
        }
    }

    public async Task<IEnumerable<ObservacionPlaneadaListadoEntity>?> ListarObservacionesPlaneadas()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            return await connection.QueryAsync<ObservacionPlaneadaListadoEntity>(
                @"
SELECT
    t1.Observacion_Id,
    t1.Codigo_Obs,
    t2.Usr_Nom,
    t3.Usr_Nom       AS Jef_Nombre,
    t6.Cen_Cos_Des,
    t4.Cliente_Nombre,
    t5.Subestacion_Nombre,
    t7.Motivo_Nombre,
    t1.Obs_Detalle
FROM Ins_Observacion_Planeada t1
JOIN Sg_Usuario t2
    ON (t1.Usr_Cod = t2.Usr_Cod)
JOIN Sg_Usuario t3
    ON (t1.Jefe_Cod = t3.Usr_Cod)
JOIN Ins_Cliente t4
    ON (t1.Cliente_Id = t4.Cliente_Id)
JOIN Ins_SubEstacion t5
    ON (t1.Subestacion_Id = t5.Subestacion_Id)
JOIN Lg_Cen_Cos t6
    ON (t3.Usr_Cen_Cos_Id = t6.Cen_Cos_Id)
JOIN Ins_Motivo t7
    ON (t1.Motivo_Id = t7.Motivo_Id)
ORDER BY t1.Observacion_Id DESC",
                commandType: CommandType.Text
            );
        }
    }

    public async Task<IEnumerable<ObservacionPlaneadaListadoEntity>?> ConsultarEstadoObservaciones(string Estado)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Estado", Estado);

            return await connection.QueryAsync<ObservacionPlaneadaListadoEntity>(
                "[dbo].[SP_Consultar_Estado_Observaciones]",
                parametros,
                commandType: CommandType.StoredProcedure
            );
        }
    }

    // FIX: SP_Filtrar_Observaciones devuelve dos columnas "Usr_Nom" sin alias (observador y supervisor),
    // lo que hace que Dapper solo mapee el primer valor. Se usa SQL inline con aliases explícitos.
    public async Task<IEnumerable<ObservacionPlaneadaListadoEntity>?> FiltrarObservaciones(DateTime? Fecha_Desde, DateTime? Fecha_Hasta, string? Estado)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Fecha_Desde", Fecha_Desde ?? DateTime.Today);
            parametros.Add("@Fecha_Hasta", Fecha_Hasta ?? DateTime.Today);
            parametros.Add("@Estado", string.IsNullOrWhiteSpace(Estado) ? "A" : Estado.Trim());

            return await connection.QueryAsync<ObservacionPlaneadaListadoEntity>(
                @"SELECT
                    t1.Codigo_Obs,
                    t2.Usr_Nom,
                    t3.Usr_Nom        AS Jef_Nombre,
                    t4.Cen_Cos_Des,
                    t5.Cliente_Nombre,
                    t6.Subestacion_Nombre,
                    t7.Motivo_Nombre,
                    t1.Obs_Detalle,
                    t1.Fec_Reg,
                    t1.Estado
                FROM Ins_Observacion_Planeada t1
                JOIN Sg_Usuario t2
                    ON (t1.Usr_Cod = t2.Usr_Cod)
                JOIN Sg_Usuario t3
                    ON (t1.Jefe_Cod = t3.Usr_Cod)
                JOIN Lg_Cen_Cos t4
                    ON (t3.Usr_Cen_Cos_Id = t4.Cen_Cos_Id)
                JOIN Ins_Cliente t5
                    ON (t1.Cliente_Id = t5.Cliente_Id)
                JOIN Ins_SubEstacion t6
                    ON (t1.Subestacion_Id = t6.Subestacion_Id)
                JOIN Ins_Motivo t7
                    ON (t1.Motivo_Id = t7.Motivo_Id)
                WHERE
                    t1.Fec_Reg >= @Fecha_Desde
                    AND t1.Fec_Reg < DATEADD(DAY, 1, @Fecha_Hasta)
                    AND t1.Estado = @Estado",
                parametros,
                commandType: CommandType.Text
            );
        }
    }

    // FIX: SP_Mostrar_Observaciones tiene columnas duplicadas sin alias — se usa SQL inline con aliases correctos
    public async Task<IEnumerable<ObservacionPlaneadaDetalleEntity>?> MostrarObservacionPlaneada(string Codigo_Obs)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Codigo_Obs", Codigo_Obs);

            return await connection.QueryAsync<ObservacionPlaneadaDetalleEntity>(
                @"SELECT
                    t2.Usr_Nom,
                    t3.Cen_Cos_Des,
                    t4.Cargo_Nombre,
                    t2.Usr_Doc_Nro,
                    t5.Cliente_Id,
                    t5.Cliente_Nombre,
                    t6.Subestacion_Id,
                    t6.Subestacion_Nombre,
                    t7.SubContrata_Id,
                    t7.SubContrata_Nombre,
                    t1.Jefe_Cod,
                    t8.Usr_Nom       AS Jef_Nombre,
                    t9.Cen_Cos_Des   AS Jef_Area,
                    t8.Usr_Doc_Nro   AS Jef_DNI,
                    t10.Motivo_Id,
                    t10.Motivo_Nombre,
                    t1.Obs_Detalle,
                    t11.Clima_Id,
                    t11.Clima_Nombre,
                    t12.Tarea_Id,
                    t12.Tarea_Nombre,
                    t1.Obs_Actividad,
                    t1.Estado
                FROM Ins_Observacion_Planeada t1
                JOIN Sg_Usuario t2
                    ON (t1.Usr_Cod = t2.Usr_Cod)
                JOIN Lg_Cen_Cos t3
                    ON (t2.Usr_Cen_Cos_Id = t3.Cen_Cos_Id)
                JOIN Ins_Cargo t4
                    ON (t2.Usr_Crg = t4.Cargo_Id)
                JOIN Ins_Cliente t5
                    ON (t1.Cliente_Id = t5.Cliente_Id)
                JOIN Ins_SubEstacion t6
                    ON (t1.Subestacion_Id = t6.Subestacion_Id)
                JOIN Ins_SubContrata t7
                    ON (t1.SubContrata_Id = t7.SubContrata_Id)
                JOIN Sg_Usuario t8
                    ON (t1.Jefe_Cod = t8.Usr_Cod)
                JOIN Lg_Cen_Cos t9
                    ON (t8.Usr_Cen_Cos_Id = t9.Cen_Cos_Id)
                JOIN Ins_Motivo t10
                    ON (t1.Motivo_Id = t10.Motivo_Id)
                JOIN Ins_Clima t11
                    ON (t1.Clima_Id = t11.Clima_Id)
                JOIN Ins_Tarea t12
                    ON (t1.Tarea_Id = t12.Tarea_Id)
                WHERE t1.Codigo_Obs = @Codigo_Obs",
                parametros,
                commandType: CommandType.Text
            );
        }
    }

    // FIX: Nombre correcto del SP — SP_Ins_Obseracion_Planeada_Insertar (sin 'v' en Obseracion, tal como está en SQL)
    public async Task<(int Codigo, string Mensaje)> RegistrarObservacionPlaneada(ObservacionPlaneadaEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Usr_Cod",        valores.Usr_Cod);
            parametros.Add("@Cliente_Id",     valores.Cliente_Id);
            parametros.Add("@Subestacion_Id", valores.Subestacion_Id);
            parametros.Add("@SubContrata_Id", valores.SubContrata_Id);
            parametros.Add("@Jefe_cod",       valores.Jefe_Cod);
            parametros.Add("@Motivo_Id",      valores.Motivo_Id);
            parametros.Add("@Clima_Id",       valores.Clima_Id);
            parametros.Add("@Tarea_Id",       valores.Tarea_Id);
            parametros.Add("@Obs_Detalle",    valores.Obs_Detalle);
            parametros.Add("@Obs_Actividad",  valores.Obs_Actividad);
            parametros.Add("@Usr_Reg",        valores.Usr_Reg);

            try
            {
                var rows = await connection.ExecuteAsync(
                    // CORRECTO: nombre exacto del SP en SQL (typo intencional sin 'v')
                    "SP_Ins_Obseracion_Planeada_Insertar",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                return (0, "Completado con éxito");
            }
            catch (SqlException ex)
            {
                return (1, ex.Message);
            }
        }
    }

    // FIX: parámetro @Jefe_Cod (antes era @Jefe_Id) para coincidir con SP_Actualizar_Observacion
    public async Task<(int Codigo, string Mensaje)> ActualizarObservacionPlaneada(ActualizarObservacionPlaneadaEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Codigo_Obs",     valores.Codigo_Obs);
            parametros.Add("@Cliente_Id",     valores.Cliente_Id);
            parametros.Add("@Subestacion_Id", valores.Subestacion_Id);
            parametros.Add("@SubContrata_Id", valores.SubContrata_Id);
            parametros.Add("@Jefe_Cod",       valores.Jefe_Cod);   // FIX: era @Jefe_Id
            parametros.Add("@Motivo_Id",      valores.Motivo_Id);
            parametros.Add("@Clima_Id",       valores.Clima_Id);
            parametros.Add("@Tarea_Id",       valores.Tarea_Id);
            parametros.Add("@Estado",         valores.Estado);
            parametros.Add("@Obs_Detalle",    valores.Obs_Detalle);
            parametros.Add("@Obs_Actividad",  valores.Obs_Actividad);
            parametros.Add("@Usr_Mod",        valores.Usr_Mod);

            try
            {
                var rows = await connection.ExecuteAsync(
                    "SP_Actualizar_Observacion",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                if (rows > 0)
                {
                    return (0, "Completado con éxito");
                }

                return (1, "No se pudo actualizar la observación planeada");
            }
            catch (SqlException ex)
            {
                return (1, ex.Message);
            }
        }
    }

    public async Task<(int Codigo, string Mensaje)> EliminarObservacionPlaneada(EliminarObservacionPlaneadaEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Codigo_Obs", valores.Codigo_Obs);
            parametros.Add("@Usr_Mod",    valores.Usr_Mod);

            try
            {
                var rows = await connection.ExecuteAsync(
                    "SP_Eliminar_Observacion",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                if (rows > 0)
                {
                    return (0, "Completado con éxito");
                }

                return (1, "No se pudo eliminar la observación planeada");
            }
            catch (SqlException ex)
            {
                return (1, ex.Message);
            }
        }
    }

    // ─── Tipos de Inspección ─────────────────────────────────────────
    public async Task<IEnumerable<InsTipoInspeccionEntity>?> ListarTiposInspeccion()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryAsync<InsTipoInspeccionEntity>(
                "SELECT t1.Tipo_Id, t1.Tipo_Nombre FROM Ins_Tipo_Inspeccion t1",
                commandType: CommandType.Text
            );
            return result;
        }
    }

    // ─── Tipos de Reporte ────────────────────────────────────────────
    public async Task<IEnumerable<InsTipoReporteEntity>?> ListarTiposReporte()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var result = await connection.QueryAsync<InsTipoReporteEntity>(
                "SELECT t1.Reporte_Id, t1.Reporte_Tipo FROM Ins_Tipo_Reporte t1 ORDER BY t1.Reporte_Tipo",
                commandType: CommandType.Text
            );
            return result;
        }
    }

    // ─── Medio Ambiente ──────────────────────────────────────────────
    public async Task<IEnumerable<WeReportListadoEntity>?> FiltrarWeReport(DateTime? Fecha_Desde, DateTime? Fecha_Hasta, string? Estado)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Fecha_Desde", Fecha_Desde ?? DateTime.Today);
            parametros.Add("@Fecha_Hasta", Fecha_Hasta ?? DateTime.Today);
            parametros.Add("@Estado", string.IsNullOrWhiteSpace(Estado) ? "A" : Estado.Trim());

            return await connection.QueryAsync<WeReportListadoEntity>(
                "SP_Filtrar_We_Report",
                parametros,
                commandType: CommandType.StoredProcedure
            );
        }
    }

    public async Task<(int Codigo, string Mensaje)> InsertarMedioAmbiente(InsMedioAmbienteEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Usr_Cod",               valores.Usr_Cod);
            parametros.Add("@Cliente_Id",            valores.Cliente_Id);
            parametros.Add("@Subestacion_Id",        valores.Subestacion_Id);
            parametros.Add("@SubContrata_Id",        valores.SubContrata_Id);
            parametros.Add("@Jefe_Cod",              valores.Jefe_Cod);
            parametros.Add("@Actividad",             valores.Actividad);
            parametros.Add("@Orden_Trabajo",         valores.Orden_Trabajo);
            parametros.Add("@Procedimiento_Trabajo", valores.Procedimiento_Trabajo);
            parametros.Add("@Tipo_Id",               valores.Tipo_Id);
            parametros.Add("@Usr_Reg",               valores.Usr_Reg);

            await connection.ExecuteAsync(
                "SP_Insertar_Medio_Ambiente",
                parametros,
                commandType: CommandType.StoredProcedure
            );
            return (0, "Inspección de Medio Ambiente registrada correctamente.");
        }
    }

    // ─── Prevención ────────────────────────────────────────────────
    // FIX: SP_Filtrar_Prevencion devuelve dos columnas Usr_Nom sin alias (supervisor y jefe),
    // lo que impide el mapeo automático de Dapper. Se usa SQL inline con aliases explícitos.
    public async Task<IEnumerable<PrevencionListadoEntity>?> FiltrarPrevencion(DateTime? Fecha_Desde, DateTime? Fecha_Hasta, string? Estado)
    {
        const string sql = @"
            SELECT
                t1.Prevencion_Id,
                t1.Prevencion_Cod,
                t2.Usr_Nom        AS Usr_Nom,
                t3.Usr_Nom        AS Jef_Nombre,
                t4.Cen_Cos_Des    AS Cen_Cos_Des,
                t5.Cliente_Nombre AS Cliente_Nombre,
                t6.Subestacion_Nombre AS Subestacion_Nombre,
                t1.Actividad,
                t1.Orden_Trabajo,
                t7.Tipo_Nombre    AS Tipo_Nombre
            FROM Ins_Inspecciones_Prevencion t1
            JOIN Sg_Usuario          t2 ON t1.Usr_Cod        = t2.Usr_Cod
            JOIN Sg_Usuario          t3 ON t1.Jefe_Cod       = t3.Usr_Cod
            JOIN Lg_Cen_Cos          t4 ON t3.Usr_Cen_Cos_Id = t4.Cen_Cos_Id
            JOIN Ins_Cliente         t5 ON t1.Cliente_Id     = t5.Cliente_Id
            JOIN Ins_SubEstacion     t6 ON t1.Subestacion_Id = t6.Subestacion_Id
            JOIN Ins_Tipo_Inspeccion t7 ON t1.Tipo_Id        = t7.Tipo_Id
            WHERE
                t1.Fec_Reg >= @Fecha_Desde
                AND t1.Fec_Reg < DATEADD(DAY, 1, @Fecha_Hasta)
                AND t1.Estado = @Estado";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Fecha_Desde", Fecha_Desde ?? DateTime.MinValue);
            parametros.Add("@Fecha_Hasta", Fecha_Hasta ?? DateTime.MinValue);
            parametros.Add("@Estado", string.IsNullOrWhiteSpace(Estado) ? "A" : Estado);

            return await connection.QueryAsync<PrevencionListadoEntity>(
                sql,
                parametros,
                commandType: CommandType.Text
            );
        }
    }

    public async Task<IEnumerable<PrevencionDetalleEntity>?> MostrarPrevencion(int Prevencion_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using var cmd = new SqlCommand("[dbo].[SP_Mostrar_Actualizar_Prevencion]", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Prevencion_Id", Prevencion_Id);

            var resultados = new List<PrevencionDetalleEntity>();

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                resultados.Add(new PrevencionDetalleEntity
                {
                    Usr_Nom              = reader.IsDBNull(0)  ? null : reader.GetString(0),
                    Cen_Cos_Des          = reader.IsDBNull(1)  ? null : reader.GetString(1),
                    Usr_Doc_Nro          = reader.IsDBNull(2)  ? null : reader.GetString(2),
                    Cliente_Nombre       = reader.IsDBNull(3)  ? null : reader.GetString(3),
                    Subestacion_Nombre   = reader.IsDBNull(4)  ? null : reader.GetString(4),
                    SubContrata_Nombre   = reader.IsDBNull(5)  ? null : reader.GetString(5),
                    Jef_Nombre           = reader.IsDBNull(6)  ? null : reader.GetString(6),
                    Jef_DNI              = reader.IsDBNull(7)  ? null : reader.GetString(7),
                    Cen_Cos_Des_Jefe     = reader.IsDBNull(8)  ? null : reader.GetString(8),
                    Actividad            = reader.IsDBNull(9)  ? null : reader.GetString(9),
                    Orden_Trabajo        = reader.IsDBNull(10) ? null : reader.GetString(10),
                    Procedimiento_Trabajo= reader.IsDBNull(11) ? null : reader.GetString(11),
                    Tipo_Nombre          = reader.IsDBNull(12) ? null : reader.GetString(12),
                    Estado               = reader.IsDBNull(13) ? null : reader.GetString(13),
                });
            }

            return resultados;
        }
    }

    public async Task<(int Codigo, string Mensaje)> InsertarPrevencion(InsPrevencionEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                parametros.Add("@Usr_Cod", valores.Usr_Cod);
                parametros.Add("@Cliente_Id", valores.Cliente_Id);
                parametros.Add("@Subestacion_Id", valores.Subestacion_Id);
                parametros.Add("@SubContrata_Id", valores.SubContrata_Id);
                parametros.Add("@Jefe_Cod", valores.Jefe_Cod);
                parametros.Add("@Actividad", valores.Actividad);
                parametros.Add("@Orden_Trabajo", valores.Orden_Trabajo);
                parametros.Add("@Procedimiento_Trabajo", valores.Procedimiento_Trabajo);
                parametros.Add("@Tipo_Id", valores.Tipo_Id);
                parametros.Add("@Usr_Reg", valores.Usr_Reg);

                await connection.ExecuteAsync(
                    "SP_Insertar_Inspeccion_Prevencion",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );
                return (0, "Inspección de Prevención registrada correctamente.");
            }
            catch (SqlException ex)
            {
                return (1, ex.Message);
            }
        }
    }

    public async Task<(int Codigo, string Mensaje)> InsertarWeReport(WeReportEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                await connection.OpenAsync();

                _logger.LogInformation(
                    "Repo InsertarWeReport: Usr_Cod={UsrCod}, Reporte_Id={ReporteId}, Cen_Cos_Id={CenCosId}, Cliente_Id={ClienteId}, Subestacion_Id={SubestacionId}, Estado={Estado}, Foto1={Foto1}, Foto2={Foto2}",
                    valores.Usr_Cod,
                    valores.Reporte_Id,
                    valores.Cen_Cos_Id,
                    valores.Cliente_Id,
                    valores.Subestacion_Id,
                    valores.Estado,
                    valores.Report_Foto1_Ubicacion,
                    valores.Report_Foto2_Ubicacion
                );

                var parametros = new DynamicParameters();
                parametros.Add("@Usr_Cod", valores.Usr_Cod);
                parametros.Add("@Report_Anonimo", valores.Report_Anonimo);
                parametros.Add("@Reporte_Id", valores.Reporte_Id);
                parametros.Add("@Cen_Cos_Id", valores.Cen_Cos_Id);
                parametros.Add("@Cliente_Id", valores.Cliente_Id);
                parametros.Add("@Subestacion_Id", valores.Subestacion_Id);
                parametros.Add("@Report_Descripcion", valores.Report_Descripcion);
                parametros.Add("@Report_Foto1_Ubicacion", valores.Report_Foto1_Ubicacion);
                parametros.Add("@Report_Acciones_Inmediata", valores.Report_Acciones_Inmediata);
                parametros.Add("@Report_Foto2_Ubicacion", valores.Report_Foto2_Ubicacion);
                parametros.Add("@Report_Acciones_Propuestas", valores.Report_Acciones_Propuestas);
                parametros.Add("@Report_Potencial", valores.Report_Potencial);
                parametros.Add("@Report_Aplica", valores.Report_Aplica);
                parametros.Add("@Usr_Reg", valores.Usr_Reg);
                parametros.Add("@Estado", string.IsNullOrWhiteSpace(valores.Estado) ? "A" : valores.Estado);
                var rows = await connection.ExecuteAsync(
                    "SP_Insertar_We_Report",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                _logger.LogInformation("Repo InsertarWeReport filas afectadas={Rows}", rows);

                if (rows > 0)
                {
                    return (0, "We Report registrado correctamente.");
                }

                return (1, "No se pudo registrar We Report");
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error SQL en InsertarWeReport");
                return (1, ex.Message);
            }
        }
    }



    // FIX: SP_Mostrar_Actualizar_We_Report devuelve dos columnas Cen_Cos_Des sin alias
    // (t4: centro de costos del cargo del usuario, t6: centro de costos del reporte),
    // lo que impide el mapeo automático de Dapper. Se usa SQL inline con aliases explícitos,
    // y se agregan las columnas que la entidad necesita y el SP original no seleccionaba.
    // FIX 2 (causa real del error "No se pudo cargar la información del We Report para editar"):
    // el JOIN contra Ins_Cargo comparaba t2.Usr_Crg (que almacena el Cargo_Id) contra
    // t3.Cargo_Nombre (texto). Al no coincidir nunca, el INNER JOIN descartaba la fila completa
    // y la consulta devolvía 0 registros. Se corrige la condición a Usr_Crg = Cargo_Id.
    // FIX 3: se cambian los INNER JOIN por LEFT JOIN en las tablas relacionadas (Cargo, Cen_Cos,
    // Tipo_Reporte, Cliente, SubEstacion) para que el registro se siga mostrando aunque alguna
    // referencia sea NULL o quede huérfana (por ejemplo, reportes anónimos sin Cliente_Id /
    // Subestacion_Id, o catálogos editados después de creado el reporte).
    public async Task<IEnumerable<WeReportActualizarEntity>?> MostrarActualizarWeReport(int We_Report_Id)
    {
        const string sql = @"
            SELECT
                t1.We_Report_Id,
                t1.Codigo_We_Report,
                t1.Usr_Cod,
                CASE WHEN t1.Report_Anonimo = 'S' THEN 'ANONIMO' ELSE t2.Usr_Nom END AS Usr_Nom,
                CASE WHEN t1.Report_Anonimo = 'S' THEN 'ANONIMO' ELSE t3.Cargo_Nombre END AS Cargo_Nombre,
                CASE WHEN t1.Report_Anonimo = 'S' THEN 'ANONIMO' ELSE t4.Cen_Cos_Des END AS Cen_Cos_Des_Usr,
                t2.Usr_Corr,
                t1.Report_Anonimo,
                t1.Reporte_Id,
                t5.Reporte_Tipo   AS Reporte_Tipo,
                t1.Cen_Cos_Id,
                t6.Cen_Cos_Des    AS Cen_Cos_Des,
                t1.Cliente_Id,
                t7.Cliente_Nombre AS Cliente_Nombre,
                t1.Subestacion_Id,
                t8.Subestacion_Nombre AS Subestacion_Nombre,
                t1.Report_Descripcion,
                t1.Report_Foto1_Ubicacion,
                t1.Report_Acciones_Inmediata,
                t1.Report_Foto2_Ubicacion,
                t1.Report_Acciones_Propuestas,
                t1.Report_Potencial,
                t1.Report_Aplica,
                t1.Usr_Reg,
                t1.Fec_Reg,
                t1.Usr_Mod,
                t1.Fec_Mod,
                t1.Estado
            FROM Ins_We_Report t1
            JOIN Sg_Usuario t2
                ON t1.Usr_Cod = t2.Usr_Cod
            LEFT JOIN Ins_Cargo t3
                ON t2.Usr_Crg = t3.Cargo_Id
            LEFT JOIN Lg_Cen_Cos t4
                ON t2.Usr_Cen_Cos_Id = t4.Cen_Cos_Id
            LEFT JOIN Ins_Tipo_Reporte t5
                ON t1.Reporte_Id = t5.Reporte_Id
            LEFT JOIN Lg_Cen_Cos t6
                ON t1.Cen_Cos_Id = t6.Cen_Cos_Id
            LEFT JOIN Ins_Cliente t7
                ON t1.Cliente_Id = t7.Cliente_Id
            LEFT JOIN Ins_SubEstacion t8
                ON t1.Subestacion_Id = t8.Subestacion_Id
            WHERE t1.We_Report_Id = @We_Report_Id";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@We_Report_Id", We_Report_Id);

            var result = await connection.QueryAsync<WeReportActualizarEntity>(sql, parametros);

            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarWeReport(WeReportActualizarEntity valores)
{
    using (var connection = new SqlConnection(_connectionString))
    {
        await connection.OpenAsync();

        Console.WriteLine("[WeReport][Repository] SP_Actualizar_We_Report -> datos a enviar:");
        Console.WriteLine($"  We_Report_Id: {valores.We_Report_Id}");
        Console.WriteLine($"  Report_Anonimo: {valores.Report_Anonimo}");
        Console.WriteLine($"  Reporte_Id: {valores.Reporte_Id}");
        Console.WriteLine($"  Cen_Cos_Id: {valores.Cen_Cos_Id}");
        Console.WriteLine($"  Cliente_Id: {valores.Cliente_Id}");
        Console.WriteLine($"  Subestacion_Id: {valores.Subestacion_Id}");
        Console.WriteLine($"  Report_Descripcion: {valores.Report_Descripcion}");
        Console.WriteLine($"  Report_Foto1_Ubicacion: {valores.Report_Foto1_Ubicacion}");
        Console.WriteLine($"  Report_Acciones_Inmediata: {valores.Report_Acciones_Inmediata}");
        Console.WriteLine($"  Report_Foto2_Ubicacion: {valores.Report_Foto2_Ubicacion}");
        Console.WriteLine($"  Report_Acciones_Propuestas: {valores.Report_Acciones_Propuestas}");
        Console.WriteLine($"  Report_Potencial: {valores.Report_Potencial}");
        Console.WriteLine($"  Report_Aplica: {valores.Report_Aplica}");
        Console.WriteLine($"  Usr_Mod: {valores.Usr_Mod}");
        Console.WriteLine($"  Estado: {valores.Estado}");

        var parametros = new DynamicParameters();
        parametros.Add("@We_Report_Id", valores.We_Report_Id);
        parametros.Add("@Report_Anonimo", valores.Report_Anonimo);
        parametros.Add("@Reporte_Id", valores.Reporte_Id);
        parametros.Add("@Cen_Cos_Id", valores.Cen_Cos_Id);
        parametros.Add("@Cliente_Id", valores.Cliente_Id);
        parametros.Add("@Subestacion_Id", valores.Subestacion_Id);
        parametros.Add("@Report_Descripcion", valores.Report_Descripcion);
        parametros.Add("@Report_Foto1_Ubicacion", valores.Report_Foto1_Ubicacion);
        parametros.Add("@Report_Acciones_Inmediata", valores.Report_Acciones_Inmediata);
        parametros.Add("@Report_Foto2_Ubicacion", valores.Report_Foto2_Ubicacion);
        parametros.Add("@Report_Acciones_Propuestas", valores.Report_Acciones_Propuestas);
        parametros.Add("@Report_Potencial", valores.Report_Potencial);
        parametros.Add("@Report_Aplica", valores.Report_Aplica);
        parametros.Add("@Usr_Mod", valores.Usr_Mod);
        parametros.Add("@Estado", string.IsNullOrWhiteSpace(valores.Estado) ? "A" : valores.Estado);

        try
        {
            var rows = await connection.ExecuteAsync(
                "SP_Actualizar_We_Report",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            Console.WriteLine($"[WeReport][Repository] Filas afectadas: {rows}");

            if (rows > 0)
            {
                return (0, "Completado con éxito");
            }

            return (1, "No se pudo actualizar We Report");
        }
        catch (SqlException ex)
        {
            Console.WriteLine("[WeReport][Repository] Error SQL al actualizar We Report:");
            Console.WriteLine(ex.ToString());
            return (1, ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("[WeReport][Repository] Error inesperado al actualizar We Report:");
            Console.WriteLine(ex.ToString());
            return (1, ex.Message);
        }
    }
}

    public async Task<(int Codigo, string Mensaje)> EliminarWeReport(EliminarWeReportEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@We_Report_Id", valores.We_Report_Id);

            try
            {
                var rows = await connection.ExecuteAsync(
                    "SP_Eliminar_We_Report",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                if (rows > 0)
                {
                    return (0, "Completado con éxito");
                }

                return (1, "No se pudo eliminar We Report");
            }
            catch (SqlException ex)
            {
                return (1, ex.Message);
            }
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarPrevencion(ActualizarPrevencionEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Prevencion_Id", valores.Prevencion_Id);
            parametros.Add("@Usr_Cod", valores.Usr_Cod);
            parametros.Add("@Cliente_Id", valores.Cliente_Id);
            parametros.Add("@Subestacion_Id", valores.Subestacion_Id);
            parametros.Add("@SubContrata_Id", valores.SubContrata_Id);
            parametros.Add("@Jefe_Cod", valores.Jefe_Cod);
            parametros.Add("@Actividad", valores.Actividad);
            parametros.Add("@Orden_Trabajo", valores.Orden_Trabajo);
            parametros.Add("@Procedimiento_Trabajo", valores.Procedimiento_Trabajo);
            parametros.Add("@Tipo_Id", valores.Tipo_Id);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);
            parametros.Add("@Estado", valores.Estado);

            await connection.ExecuteAsync(
                "SP_Actualizar_Prevencion",
                parametros,
                commandType: CommandType.StoredProcedure
            );
            return (0, "Inspección de Prevención actualizada correctamente.");
        }
    }

    public async Task<(int Codigo, string Mensaje)> EliminarPrevencion(EliminarPrevencionEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Prevencion_Id", valores.Prevencion_Id);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);

            await connection.ExecuteAsync(
                "SP_Eliminar_Prevencion",
                parametros,
                commandType: CommandType.StoredProcedure
            );
            return (0, "Inspección de Prevención eliminada correctamente.");
        }
    }

    private static string NormalizarEstadoMedioAmbiente(string? estado)
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

    public async Task<IEnumerable<MedioAmbienteListadoEntity>?> FiltrarMedioAmbiente(DateTime? Fecha_Desde, DateTime? Fecha_Hasta, string? Estado)
    {
        // Se usa SQL inline con aliases explícitos porque el SP devuelve columnas duplicadas
        // (Usr_Nom y Cen_Cos_Des aparecen dos veces), lo que impide el mapeo automático de Dapper.
        const string sql = @"
            SELECT
                t1.Medio_Ambiente_Id,
                t1.Medio_Ambiente_Cod,
                t7.Usr_Nom        AS Supervisor_Nom,
                t2.Usr_Nom        AS Jef_Nombre,
                t3.Cen_Cos_Des    AS Cen_Cos_Des,
                t4.Cliente_Nombre AS Cliente_Nombre,
                t5.Subestacion_Nombre AS Subestacion_Nombre,
                t1.Actividad,
                t1.Orden_Trabajo,
                t6.Tipo_Nombre    AS Tipo_Nombre
            FROM Ins_Inspecciones_Medio_Ambiente t1
            JOIN Sg_Usuario          t2 ON t1.Jefe_Cod        = t2.Usr_Cod
            JOIN Lg_Cen_Cos          t3 ON t2.Usr_Cen_Cos_Id  = t3.Cen_Cos_Id
            JOIN Ins_Cliente         t4 ON t1.Cliente_Id       = t4.Cliente_Id
            JOIN Ins_SubEstacion     t5 ON t1.Subestacion_Id   = t5.Subestacion_Id
            JOIN Ins_Tipo_Inspeccion t6 ON t1.Tipo_Id          = t6.Tipo_Id
            JOIN Sg_Usuario          t7 ON t1.Usr_Cod          = t7.Usr_Cod
            WHERE
                t1.Fec_Reg >= @Fecha_Desde
                AND t1.Fec_Reg < DATEADD(DAY, 1, @Fecha_Hasta)
                AND t1.Estado = @Estado";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Fecha_Desde", Fecha_Desde ?? DateTime.MinValue);
            parametros.Add("@Fecha_Hasta", Fecha_Hasta ?? DateTime.MinValue);
            parametros.Add("@Estado", NormalizarEstadoMedioAmbiente(Estado));

            return await connection.QueryAsync<MedioAmbienteListadoEntity>(
                sql,
                parametros,
                commandType: CommandType.Text
            );
        }
    }

    public async Task<IEnumerable<MedioAmbienteDetalleEntity>?> MostrarMedioAmbiente(int Medio_Ambiente_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using var cmd = new SqlCommand("[dbo].[SP_Mostrar_Medio_Ambiente]", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Medio_Ambiente_Id", Medio_Ambiente_Id);

            var resultados = new List<MedioAmbienteDetalleEntity>();

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                // El SP devuelve columnas duplicadas sin alias, se lee por posicion:
                // 0: Usr_Nom (supervisor)   1: Cen_Cos_Des (sup area)   2: Usr_Doc_Nro (sup dni)
                // 3: Cliente_Nombre         4: Subestacion_Nombre        5: SubContrata_Nombre
                // 6: Usr_Nom (jefe)         7: Usr_Doc_Nro (jefe dni)   8: Cen_Cos_Des (jefe area)
                // 9: Actividad              10: Orden_Trabajo            11: Procedimiento_Trabajo
                // 12: Tipo_Nombre           13: Estado
                resultados.Add(new MedioAmbienteDetalleEntity
                {
                    Usr_Cod              = reader.IsDBNull(0)  ? null : reader.GetString(0),
                    Cen_Cos_Des          = reader.IsDBNull(1)  ? null : reader.GetString(1),
                    Usr_Doc_Nro          = reader.IsDBNull(2)  ? null : reader.GetString(2),
                    Cliente_Nombre       = reader.IsDBNull(3)  ? null : reader.GetString(3),
                    Subestacion_Nombre   = reader.IsDBNull(4)  ? null : reader.GetString(4),
                    SubContrata_Nombre   = reader.IsDBNull(5)  ? null : reader.GetString(5),
                    Jef_Nombre           = reader.IsDBNull(6)  ? null : reader.GetString(6),
                    Jef_DNI              = reader.IsDBNull(7)  ? null : reader.GetString(7),
                    Cen_Cos_Des_Jefe     = reader.IsDBNull(8)  ? null : reader.GetString(8),
                    Actividad            = reader.IsDBNull(9)  ? null : reader.GetString(9),
                    Orden_Trabajo        = reader.IsDBNull(10) ? null : reader.GetString(10),
                    Procedimiento_Trabajo= reader.IsDBNull(11) ? null : reader.GetString(11),
                    Tipo_Nombre          = reader.IsDBNull(12) ? null : reader.GetString(12),
                    Estado               = reader.IsDBNull(13) ? null : reader.GetString(13),
                });
            }

            return resultados;
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarMedioAmbiente(ActualizarMedioAmbienteEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Medio_Ambiente_Id", valores.Medio_Ambiente_Id);
            parametros.Add("@Usr_Cod", valores.Usr_Cod);
            parametros.Add("@Cliente_Id", valores.Cliente_Id);
            parametros.Add("@Subestacion_Id", valores.Subestacion_Id);
            parametros.Add("@SubContrata_Id", valores.SubContrata_Id);
            parametros.Add("@Jefe_Cod", valores.Jefe_Cod);
            parametros.Add("@Actividad", valores.Actividad);
            parametros.Add("@Orden_Trabajo", valores.Orden_Trabajo);
            parametros.Add("@Procedimiento_Trabajo", valores.Procedimiento_Trabajo);
            parametros.Add("@Tipo_Id", valores.Tipo_Id);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);
            parametros.Add("@Estado", valores.Estado);

            try
            {
                var rows = await connection.ExecuteAsync(
                    "SP_Actualizar_Medio_Ambiente",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                if (rows > 0)
                {
                    return (0, string.Empty);
                }

                return (1, "No se pudo actualizar la inspección de medio ambiente");
            }
            catch (SqlException ex)
            {
                return (1, ex.Message);
            }
        }
    }

    public async Task<(int Codigo, string Mensaje)> EliminarMedioAmbiente(EliminarMedioAmbienteEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Medio_Ambiente_Id", valores.Medio_Ambiente_Id);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);

            try
            {
                var rows = await connection.ExecuteAsync(
                    "SP_Eliminar_Medio_Ambiente",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                if (rows > 0)
                {
                    return (0, string.Empty);
                }

                return (1, "No se pudo eliminar la inspección de medio ambiente");
            }
            catch (SqlException ex)
            {
                return (1, ex.Message);
            }
        }
    }
}
