using System.Data;
using System.Data.SqlClient;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class ItemRepository: IItemRepository
{
    private readonly string _connectionString;

    public ItemRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<ItemEntity>?> ListarItem(string? Itm_Cod, string? Itm_Des, int? Itm_Grp, int? Itm_Sub_Grp, int? Itm_Det_Mat_Id,string? Flg_Est)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Itm_Cod", string.IsNullOrWhiteSpace(Itm_Cod) ? "" : Itm_Cod.Trim());
            parametros.Add("@Itm_Des", string.IsNullOrWhiteSpace(Itm_Des) ? "" : Itm_Des.Trim());
            parametros.Add("@Itm_Grp", Itm_Grp.HasValue && Itm_Grp.Value > 0 ? Itm_Grp.Value.ToString() : "");
            parametros.Add("@Itm_Sub_Grp", Itm_Sub_Grp.HasValue && Itm_Sub_Grp.Value > 0 ? Itm_Sub_Grp.Value.ToString() : "");
            parametros.Add("@Itm_Det_Mat_Id", Itm_Det_Mat_Id.HasValue && Itm_Det_Mat_Id.Value > 0 ? Itm_Det_Mat_Id.Value.ToString() : "");
            parametros.Add("@Flg_Est", string.IsNullOrWhiteSpace(Flg_Est) ? "" : Flg_Est.Trim());
            
            var result = await connection.QueryAsync<ItemEntity>(
                    "[dbo].[PA_Lg_Item_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
            );
            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarItem(ItemEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            
            parametros.Add("@Itm_Des", valores.Itm_Des);
            parametros.Add("@Itm_Grp", valores.Itm_Grp);
            parametros.Add("@Itm_Sub_Grp", valores.Itm_Sub_Grp);
            parametros.Add("@Itm_Det_Mat_Id", valores.Itm_Det_Mat_Id);
            parametros.Add("@Uni_Med_Id", valores.Uni_Med_Id);
            parametros.Add("@Usr_Reg", valores.Usr_Reg);
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Item_I0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var mensaje = parametros.Get<string>("@sMsj");
            return (Codigo, mensaje);
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarItem(ItemEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();

            parametros.Add("@Itm_Id", valores.Itm_Id);
            parametros.Add("@Itm_Des", valores.Itm_Des);
            parametros.Add("@Itm_Grp", valores.Itm_Grp);
            parametros.Add("@Itm_Sub_Grp", valores.Itm_Sub_Grp);
            parametros.Add("@Itm_Det_Mat_Id", valores.Itm_Det_Mat_Id);
            parametros.Add("@Flg_Est", valores.Flg_Est);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);
            parametros.Add("@Uni_Med_Id", valores.Uni_Med_Id);
            
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Item_U0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, mensaje);
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarStockItem(ItemEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();

            parametros.Add("@Itm_Id", valores.Itm_Id);
            parametros.Add("@Ord_Com_Id", valores.Ord_Com_Id);
            parametros.Add("@Can_Ing", valores.Can_Ing);
            
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Item_U0002]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, mensaje);
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarStockItemIngresoDirecto(ItemEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();

            parametros.Add("@Alm_Mov_Id", valores.Alm_Mov_Id);
            parametros.Add("@Alm_Det_Itm_Id", valores.Alm_Det_Itm_Id);
            parametros.Add("@Can_Ing", valores.Can_Ing);
            
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Item_U0003]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, mensaje);
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarStockItemSalida(ItemEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();

            parametros.Add("@Alm_Mov_Id", valores.Alm_Mov_Id);
            parametros.Add("@Alm_Det_Itm_Id", valores.Alm_Det_Itm_Id);
            parametros.Add("@Can_Ing", valores.Can_Ing);
            
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Item_U0004]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, mensaje);
        }
    }

    public async Task<IEnumerable<ItemEntity>?> ListarStocksItems(int? Usr_Cen_Cos_Id, int? Alm_Det_Itm_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Usr_Cen_Cos_Id", Usr_Cen_Cos_Id);
            parametros.Add("@Alm_Det_Itm_Id", Alm_Det_Itm_Id);

            var result = await connection.QueryAsync<ItemEntity>(
                "[dbo].[PA_Lg_Almacen_Det_Ing_S0002]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarStockItemSalidaAnulacion(ItemEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();

            parametros.Add("@Alm_Mov_Id", valores.Alm_Mov_Id);
            parametros.Add("@Alm_Det_Itm_Id", valores.Alm_Det_Itm_Id);
            parametros.Add("@Can_Ing", valores.Can_Ing);
            
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Item_U0005]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, mensaje);
        }
    }

    public async Task<IEnumerable<ReporteItemOcos>?> ReporteOCOS(
        DateTime? Fec_Ini, DateTime? Fec_Fin, int? Ped_Id, int? Ord_Com_Id, int? Ped_Tip_Com,
        int? Mon_Id, string? Usr_Reg, int? Ped_Cen_Cos_Asg, int? Ord_Com_For_Pag, int? Ord_Com_Prv 
    )
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Fec_Ini", Fec_Ini);
            parametros.Add("@Fec_Fin", Fec_Fin);
            parametros.Add("@Ped_Id", Ped_Id);
            parametros.Add("@Ord_Com_Id", Ord_Com_Id);
            parametros.Add("@Ped_Tip_Com", Ped_Tip_Com);
            parametros.Add("@Mon_Id", Mon_Id);
            parametros.Add("@Usr_Reg", Usr_Reg);
            parametros.Add("@Ped_Cen_Cos_Asg", Ped_Cen_Cos_Asg);
            parametros.Add("@Ord_Com_For_Pag", Ord_Com_For_Pag);
            parametros.Add("@Ord_Com_Prv", Ord_Com_Prv);

            var result = await connection.QueryAsync<ReporteItemOcos>(
                "[dbo].[PA_Lg_Item_S0002]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }
    
}
