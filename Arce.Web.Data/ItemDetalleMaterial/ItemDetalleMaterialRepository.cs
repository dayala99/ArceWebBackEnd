using System.Data;
using System.Data.SqlClient;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class ItemDetalleMaterialRepository: IItemDetalleMaterialRepository
{
    private readonly string _connectionString;

    public ItemDetalleMaterialRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<ItemDetalleMaterialEntity>?> ListarItemDetalleMaterial(string? Det_Mat_Cod, string? Det_Mat_Des, int? Grp_Id, int? Sub_Grp_Id, string? Flg_Est)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Det_Mat_Cod", Det_Mat_Cod);
            parametros.Add("@Det_Mat_Des", Det_Mat_Des);
            parametros.Add("@Grp_Id", Grp_Id);
            parametros.Add("@Sub_Grp_Id", Sub_Grp_Id);
            parametros.Add("@Flg_Est", Flg_Est);
            var result = await connection.QueryAsync<ItemDetalleMaterialEntity>(
                    "[dbo].[PA_Lg_Detalle_Material_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
            );
            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarItemDetalleMaterial(ItemDetalleMaterialEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            
            parametros.Add("@Grp_Id", valores.Grp_Id);
            parametros.Add("@Sub_Grp_Id", valores.Sub_Grp_Id);
            parametros.Add("@Det_Mat_Des", valores.Det_Mat_Des);
            parametros.Add("@Usr_Reg", valores.Usr_Reg);
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Detalle_Material_I0001]"
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

    public async Task<(int Codigo, string Mensaje)> ActualizarItemDetalleMaterial(ItemDetalleMaterialEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();

            parametros.Add("@Det_Mat_Id", valores.Det_Mat_Id);
            parametros.Add("@Grp_Id", valores.Grp_Id);
            parametros.Add("@Sub_Grp_Id", valores.Sub_Grp_Id);
            parametros.Add("@Det_Mat_Des", valores.Det_Mat_Des);
            parametros.Add("@Flg_Est", valores.Flg_Est);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);

            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Detalle_Material_U0001]"
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

    public async Task<IEnumerable<ItemDetalleMaterialEntity>?> ListarItemDetalleMaterialPorGrupoySubgrupo(int? Grp_Id, int? Sub_Grp_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Grp_Id", Grp_Id);
            parametros.Add("@Sub_Grp_Id", Sub_Grp_Id);

            var result = await connection.QueryAsync<ItemDetalleMaterialEntity>(
                    "[dbo].[PA_Lg_Detalle_Material_S0002]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
            );
            return result;
        }
    }
}
