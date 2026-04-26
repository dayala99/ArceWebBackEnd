using System.Data;
using System.Data.SqlClient;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class CentroCostoRepository: ICentroCostoRepository
{
    private readonly string _connectionString;

    public CentroCostoRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<CentroCostoEntity>?> ListarCentroCostoActivo(int? Cen_Cos_Id, string? Cen_Cos_Des, string? Flg_Est)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Cen_Cos_Id", Cen_Cos_Id);
            parametros.Add("@Cen_Cos_Des", Cen_Cos_Des);
            parametros.Add("@Flg_Est", Flg_Est);
            
            var result = await connection.QueryAsync<CentroCostoEntity>(
                "[dbo].[PA_Lg_Cen_Cos_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarCentroCosto(CentroCostoEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Cen_Cos_Des", valores.Cen_Cos_Des);
            parametros.Add("@Usr_Reg", valores.Usr_Reg);
            
            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size:255, direction:ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Cen_Cos_I0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var Mensaje = parametros.Get<string>("@sMsj");

            return (Codigo, Mensaje);
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarCentroCosto(CentroCostoEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Cen_Cos_Id", valores.Cen_Cos_Id);
            parametros.Add("@Cen_Cos_Des", valores.Cen_Cos_Des);
            parametros.Add("@Flg_Est", valores.Flg_Est);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size:255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Cen_Cos_U0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var Mensaje = parametros.Get<string>("@sMsj");

            return (Codigo, Mensaje);   
        }
    }
}
