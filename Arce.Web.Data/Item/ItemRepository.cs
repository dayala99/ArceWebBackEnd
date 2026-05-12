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

    public async Task<IEnumerable<ItemEntity>?> ListarItem(int? Itm_Id, string? Itm_Des, int? Itm_Grp, string? Flg_Est)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Itm_Id", Itm_Id ?? 0);
            parametros.Add("@Itm_Des", string.IsNullOrWhiteSpace(Itm_Des) ? "" : Itm_Des.Trim());
            parametros.Add("@Itm_Grp", Itm_Grp.HasValue && Itm_Grp.Value > 0 ? Itm_Grp.Value.ToString() : "");
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
            parametros.Add("@Flg_Est", valores.Flg_Est);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);

            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Proveedor_U0001]"
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
}
