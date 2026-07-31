using System.Data;
using System.Data.SqlClient;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class AsignacionRepository: IAsignacionRepository
{
    private readonly string _connectionString;

    public AsignacionRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<AsignacionCabeceraEntity>?> ListarAsignacion()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            // parametros.Add("@Ban_Id", Ban_Id);
            // parametros.Add("@Ban_Des", Ban_Des);
            // parametros.Add("@Flg_Est", Flg_Est);

            var result = await connection.QueryAsync<AsignacionCabeceraEntity>(
                "[dbo].[PA_Lg_Asignacion_Cab_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje, int AsignacionId)> RegistrarAsignacion(AsignacionCabeceraEntity valores)
    {
        using(var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Asg_Fec", valores.Asg_Fec);
            parametros.Add("@Asg_Usr", valores.Asg_Usr);
            parametros.Add("@Asg_Usr_Cen_Cos",valores.Asg_Usr_Cen_Cos);
            parametros.Add("@Usr_Reg", valores.Usr_Reg);

            parametros.Add("@Asg_Id", 0);
            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Asg_Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Asignacion_Cab_I0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }catch(SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            var AsignacionId = parametros.Get<int>("@Asg_Id");
            var Codigo = parametros.Get<int>("@Codigo");
            var Mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, Mensaje, AsignacionId);
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarAsignacion(AsignacionCabeceraEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Asg_Id", valores.Asg_Id);
            parametros.Add("@Asg_Fec", valores.Asg_Fec);
            parametros.Add("@Asg_Usr", valores.Asg_Usr);
            parametros.Add("@Asg_Usr_Cen_Cos",valores.Asg_Usr_Cen_Cos);
            parametros.Add("@Usr_Mod",valores.Usr_Mod);
            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Asignacion_Cab_U0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var Mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, Mensaje);
        }
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarAsignacionDetalle(AsignacionDetalleEntity valores)
    {
        using(var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Asg_Id", valores.Asg_Id);
            parametros.Add("@Asg_Det_Itm_Id", valores.Asg_Det_Itm_Id);
            parametros.Add("@Asg_Det_Can",valores.Asg_Det_Can);
            parametros.Add("@Asg_Det_Ser", valores.Asg_Det_Ser);
            parametros.Add("@Asg_Det_Obs", valores.Asg_Det_Obs);

            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Asignacion_Det_I0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }catch(SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var Mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, Mensaje);
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarAsignacionDetalle(AsignacionDetalleEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Asg_Det_Id", valores.Asg_Det_Id);
            parametros.Add("@Asg_Det_Itm_Id", valores.Asg_Det_Itm_Id);
            parametros.Add("@Asg_Det_Can",valores.Asg_Det_Can);
            parametros.Add("@Asg_Det_Ser", valores.Asg_Det_Ser);
            parametros.Add("@Asg_Det_Obs", valores.Asg_Det_Obs);

            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Asignacion_Det_U0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var Mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, Mensaje);
        }
    }

    public async Task<IEnumerable<AsignacionDetalleEntity>?> ListarDetallesXAsignacion(int? Asg_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Asg_Id", Asg_Id);

            var result = await connection.QueryAsync<AsignacionDetalleEntity>(
                "[dbo].[PA_Lg_Asignacion_Det_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<IEnumerable<AsignacionDetalleEntity>?> ListarAsignacionDetalleModificar(int? Asg_Det_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Asg_Det_Id", Asg_Det_Id);

            var result = await connection.QueryAsync<AsignacionDetalleEntity>(
                "[dbo].[PA_Lg_Asignacion_Det_S0002]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<IEnumerable<AsignacionCabeceraEntity>?> ListarAsignacionModificar(int? Asg_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Asg_Id", Asg_Id);

            var result = await connection.QueryAsync<AsignacionCabeceraEntity>(
                "[dbo].[PA_Lg_Asignacion_Cab_S0002]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<IEnumerable<AsignacionCabeceraEntity>?> ObtenerStockReservadoAsignacion(int? Asg_Usr_Cen_Cos, int? Asg_Det_Itm_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Asg_Usr_Cen_Cos", Asg_Usr_Cen_Cos);
            parametros.Add("@Asg_Det_Itm_Id", Asg_Det_Itm_Id);

            var result = await connection.QueryAsync<AsignacionCabeceraEntity>(
                "[dbo].[PA_Lg_Asignacion_Cab_S0003]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> EliminarAsignacionDetalle(AsignacionDetalleEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Asg_Det_Id", valores.Asg_Det_Id);

            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Asignacion_Det_U0002]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var Mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, Mensaje);
        }
    }
}
