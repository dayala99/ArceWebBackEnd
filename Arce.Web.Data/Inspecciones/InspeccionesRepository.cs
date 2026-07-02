using Arce.Web.Entity.Inspecciones;
using Arce.Web.Entity.Usuario;
using System;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Arce.Web.Data;

public class InspeccionesRepository : IInspeccionesRepository
{
    public readonly string _connectionString;

    public InspeccionesRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
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
                    t2.Cen_Cos_Des
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
                "[dbo].[SP_Ins_SubEstacion_ListarPorCliente]",
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

            var result = await connection.QueryAsync<SubEstacionEntity>(
                "[dbo].[SP_Filtrar_Subestaciones]",
                parametros,
                commandType: CommandType.StoredProcedure
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

    // ─── Medio Ambiente ──────────────────────────────────────────────
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

    // FIX: SP_Mostrar_Actualizar_Prevencion devuelve columnas duplicadas sin alias
    // (Usr_Nom, Usr_Doc_Nro, Cen_Cos_Des aparecen dos veces para supervisor y jefe).
    // Se usa SqlDataReader con lectura por posición para evitar el conflicto de Dapper.
    // Columnas por posición:
    //  0: Usr_Nom (supervisor)       1: Cen_Cos_Des (sup área)    2: Usr_Doc_Nro (sup DNI)
    //  3: Cliente_Nombre             4: Subestacion_Nombre         5: SubContrata_Nombre
    //  6: Usr_Nom (jefe)             7: Usr_Doc_Nro (jefe DNI)    8: Cen_Cos_Des (jefe área)
    //  9: Actividad                  10: Orden_Trabajo             11: Procedimiento_Trabajo
    // 12: Tipo_Nombre               13: Estado
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
