using System.Data;
using System.Data.SqlClient;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class AlmacenRepository: IAlmacenRepository
{
    private readonly string _connectionString;

    public AlmacenRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    /*INICIO CABECERA*/
    public async Task<IEnumerable<AlmacenEntity>?> ListarIngresoAlmacen(int? Alm_Mov_Id, string? Alm_Tip_Ing, string? Flg_Est, string? Flg_Est_Apr)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Alm_Mov_Id", Alm_Mov_Id);
            parametros.Add("@Alm_Tip_Ing", Alm_Tip_Ing);
            parametros.Add("@Flg_Est", Flg_Est);
            parametros.Add("@Flg_Est_Apr", Flg_Est_Apr);

            var result = await connection.QueryAsync<AlmacenEntity>(
                "[dbo].[PA_Lg_Almacen_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<IEnumerable<AlmacenEntity>?> ListarIngresoAlmacenModificar(int? Alm_Mov_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Alm_Mov_Id", Alm_Mov_Id);

            var result = await connection.QueryAsync<AlmacenEntity>(
                "[dbo].[PA_Lg_Almacen_S0002]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje, int Alm_Mov_Id)> RegistrarIngresoAlmacen(AlmacenEntity valores)
    {
        using(var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Alm_Ubi", valores.Alm_Ubi);
            parametros.Add("@Alm_Sol_Dni", valores.Alm_Sol_Dni);
            parametros.Add("@Alm_Cen_Cos",valores.Alm_Cen_Cos);
            parametros.Add("@Alm_Tip_Ing", valores.Alm_Tip_Ing);
            parametros.Add("@Usr_Reg",valores.Usr_Reg);
            parametros.Add("@Alm_Mov_Id",0);
            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Alm_Mov_Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Almacen_I0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }catch(SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
            var Alm_Mov_Id = parametros.Get<int>("@Alm_Mov_Id");
            var Codigo = parametros.Get<int>("@Codigo");
            var Mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, Mensaje, Alm_Mov_Id);
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarIngresoAlmacen(AlmacenEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();

            parametros.Add("@Alm_Mov_Id", valores.Alm_Mov_Id);
            parametros.Add("@Alm_Ubi", valores.Alm_Ubi);
            parametros.Add("@Alm_Sol_Dni", valores.Alm_Sol_Dni);
            parametros.Add("@Alm_Cen_Cos",valores.Alm_Cen_Cos);
            parametros.Add("@Flg_Est",valores.Flg_Est);
            parametros.Add("@Usr_Mod",valores.Usr_Mod);
            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Almacen_U0001]"
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

    /*FIN CABECERA*/

    /*INICIO DETALLE*/
    //CARGAR DATOS DETALLE AL MODIFICAR
    public async Task<IEnumerable<AlmacenDetalleEntity>?> ListarIngresoAlmacenDetalleModificar(int? Alm_Mov_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Alm_Mov_Id", Alm_Mov_Id);

            var result = await connection.QueryAsync<AlmacenDetalleEntity>(
                "[dbo].[PA_Lg_Almacen_Det_Ing_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarIngresoAlmacenDetalle(AlmacenDetalleEntity valores)
    {
        using(var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Alm_Mov_Id", valores.Alm_Mov_Id);
            parametros.Add("@Alm_Det_Itm_Id", valores.Alm_Det_Itm_Id);
            parametros.Add("@Alm_Det_Uni_Med_Id", valores.Alm_Det_Uni_Med_Id);
            parametros.Add("@Alm_Det_Can", valores.Alm_Det_Can);
            parametros.Add("@Alm_Det_Doc_Nro", valores.Alm_Det_Doc_Nro);
            parametros.Add("@Alm_Det_Fec", valores.Alm_Det_Fec);
            parametros.Add("@Alm_Det_Cen_Cos_Id", valores.Alm_Det_Cen_Cos_Id);
            parametros.Add("@Usr_Reg",valores.Usr_Reg);
            parametros.Add("@Alm_Det_Prv_Id",valores.Alm_Det_Prv_Id);
            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Almacen_Det_Ing_I0001]"
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

    public async Task<(int Codigo, string Mensaje)> ActualizarIngresoAlmacenDetalle(AlmacenDetalleEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();

            parametros.Add("@Alm_Det_Id", valores.Alm_Det_Id);
            parametros.Add("@Alm_Det_Itm_Id", valores.Alm_Det_Itm_Id);
            parametros.Add("@Alm_Det_Uni_Med_Id", valores.Alm_Det_Uni_Med_Id);
            parametros.Add("@Alm_Det_Can", valores.Alm_Det_Can);
            parametros.Add("@Alm_Det_Doc_Nro", valores.Alm_Det_Doc_Nro);
            parametros.Add("@Alm_Det_Fec", valores.Alm_Det_Fec);
            parametros.Add("@Alm_Det_Cen_Cos_Id", valores.Alm_Det_Cen_Cos_Id);
            parametros.Add("@Usr_Mod",valores.Usr_Mod);
            parametros.Add("@Alm_Det_Prv_Id",valores.Alm_Det_Prv_Id);
            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Almacen_Det_Ing_U0001]"
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

    /*FIN DETALLE*/

    public async Task<(int Codigo, string Mensaje, int Alm_Mov_Id)> RegistrarIngresoAlmacenOrdenCompra(AlmacenEntity valores)
    {
        using(var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Alm_Ubi", valores.Alm_Ubi);
            parametros.Add("@Alm_Tip_Ing", valores.Alm_Tip_Ing);
            parametros.Add("@Usr_Reg",valores.Usr_Reg);
            parametros.Add("@Ord_Com_Id", valores.Ord_Com_Id);
            parametros.Add("@Ped_Id",valores.Ped_Id);
            parametros.Add("@Alm_Mov_Id",0);
            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Alm_Mov_Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Almacen_I0002]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }catch(SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
            var Alm_Mov_Id = parametros.Get<int>("@Alm_Mov_Id");
            var Codigo = parametros.Get<int>("@Codigo");
            var Mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, Mensaje, Alm_Mov_Id);
        }
    }
}
