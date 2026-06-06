using System.Data;
using System.Data.SqlClient;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class DetraccionRepository: IDetraccionRepository
{
    private readonly string _connectionString;

    public DetraccionRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<DetraccionEntity>?> ListarDetraccion(int? Det_Id, string? Det_Des, string? Flg_Est)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Det_Id", Det_Id);
            parametros.Add("@Det_Des", Det_Des);
            parametros.Add("@Flg_Est", Flg_Est);

            var result = await connection.QueryAsync<DetraccionEntity>(
                "[dbo].[PA_Lg_Detraccion_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarDetraccion(DetraccionEntity valores)
    {
        using(var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Det_Des", valores.Det_Des);
            parametros.Add("@Det_Por", valores.Det_Por);
            parametros.Add("@Usr_Reg",valores.Usr_Reg);

            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Detraccion_I0001]"
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

    public async Task<(int Codigo, string Mensaje)> ActualizarDetraccion(DetraccionEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();

            parametros.Add("@Det_Id", valores.Det_Id);
            parametros.Add("@Det_Des", valores.Det_Des);
            parametros.Add("@Det_Por", valores.Det_Por);
            parametros.Add("@Flg_Est",valores.Flg_Est);
            parametros.Add("@Usr_Mod",valores.Usr_Mod);

            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Detraccion_U0001]"
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
