using System.Data;
using System.Data.SqlClient;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class SubGrupoItemRepository: ISubGrupoItemRepository
{
    private readonly string _connectionString;

    public SubGrupoItemRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<SubGrupoItemEntity>?> ListarSubGrupoItem(string? Sub_Grp_Cod, string? Sub_Grp_Des, string? Flg_Est)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Sub_Grp_Cod", Sub_Grp_Cod);
            parametros.Add("@Sub_Grp_Des", Sub_Grp_Des);
            parametros.Add("@Flg_Est", Flg_Est);

            var result = await connection.QueryAsync<SubGrupoItemEntity>(
                "[dbo].[PA_Lg_SubGrupo_Item_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarSubGrupoItem(SubGrupoItemEntity valores)
    {
        using(var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Sub_Grp_Des", valores.Sub_Grp_Des);
            parametros.Add("@Grp_Id", valores.Grp_Id);
            parametros.Add("@Usr_Reg",valores.Usr_Reg);
            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_SubGrupo_Item_I0001]"
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

    public async Task<(int Codigo, string Mensaje)> ActualizarSubGrupoItem(SubGrupoItemEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();

            parametros.Add("@Sub_Grp_Id", valores.Sub_Grp_Id);
            parametros.Add("@Sub_Grp_Des", valores.Sub_Grp_Des);
            parametros.Add("@Grp_Id", valores.Grp_Id);
            parametros.Add("@Flg_Est", valores.Flg_Est);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_SubGrupo_Item_U0001]"
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

    public async Task<IEnumerable<SubGrupoItemEntity>?> ListarSubGrupoItemPorGrpId(int? Grp_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Grp_Id", Grp_Id);

            var result = await connection.QueryAsync<SubGrupoItemEntity>(
                "[dbo].[PA_Lg_SubGrupo_Item_S0002]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }
}
