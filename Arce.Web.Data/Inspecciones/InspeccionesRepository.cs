using Arce.Web.Entity.Inspecciones;
using Arce.Web.Entity.Usuario;
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

    public async Task<IEnumerable<UsuarioEntity>?> ConsultarDatosUsuario(string? Usr_Cod)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Usr_Cod", Usr_Cod);
            var result = await connection.QueryAsync<UsuarioEntity>(
                "[dbo].[SP_Consulta_Datos_Usuario]",
                parametros,
                commandType: CommandType.StoredProcedure
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
                "[dbo].[SP_Ins_Jefe_ListarConArea]",
                commandType: CommandType.StoredProcedure
            );
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
    t3.Jef_Nombre,
    t6.Cen_Cos_Des,
    t4.Cliente_Nombre,
    t5.Subestacion_Nombre,
    t7.Motivo_Nombre,
    t1.Obs_Detalle
FROM Ins_Observacion_Planeada t1
JOIN Sg_Usuario t2
    ON (t1.Usr_Cod = t2.Usr_Cod)
JOIN Ins_Jefe t3
    ON (t1.Jefe_Id = t3.Jefe_Id)
JOIN Ins_Cliente t4
    ON (t1.Cliente_Id = t4.Cliente_Id)
JOIN Ins_SubEstacion t5
    ON (t1.Subestacion_Id = t5.Subestacion_Id)
JOIN Lg_Cen_Cos t6
    ON (t3.Cen_Cos_Id = t6.Cen_Cos_Id)
JOIN Ins_Motivo t7
    ON (t1.Motivo_Id = t7.Motivo_Id)
ORDER BY t1.Observacion_Id DESC",
                commandType: CommandType.Text
            );
        }
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarObservacionPlaneada(ObservacionPlaneadaEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Usr_Cod", valores.Usr_Cod);
            parametros.Add("@Cliente_Id", valores.Cliente_Id);
            parametros.Add("@Subestacion_Id", valores.Subestacion_Id);
            parametros.Add("@SubContrata_Id", valores.SubContrata_Id);
            parametros.Add("@Jefe_Id", valores.Jefe_Id);
            parametros.Add("@Motivo_Id", valores.Motivo_Id);
            parametros.Add("@Clima_Id", valores.Clima_Id);
            parametros.Add("@Tarea_Id", valores.Tarea_Id);
            parametros.Add("@Obs_Detalle", valores.Obs_Detalle);
            parametros.Add("@Obs_Actividad", valores.Obs_Actividad);
            parametros.Add("@Usr_Reg", valores.Usr_Reg);

            try
            {
                var rows = await connection.ExecuteAsync(
                    "SP_Ins_Observacion_Planeada_Insertar",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

                if (rows > 0)
                {
                    return (0, "Completado con éxito");
                }

                return (1, "No se pudo registrar la observación planeada");
            }
            catch (SqlException ex)
            {
                return (1, ex.Message);
            }
        }
    }
}