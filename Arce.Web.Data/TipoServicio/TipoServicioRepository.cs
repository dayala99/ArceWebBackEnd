using System.Data;
using System.Data.SqlClient;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class TipoServicioRepository: ITipoServicioRepository
{
    private readonly string _connectionString;

    public TipoServicioRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<TipoServicioEntity>?> ListarTipoServicioActivo(int? Tip_Ser_Id, string? Tip_Ser_Des, string? Flg_Est)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Tip_Ser_Id", Tip_Ser_Id);
            parametros.Add("@Tip_Ser_Des", Tip_Ser_Des);
            parametros.Add("@Flg_Est", Flg_Est);

            var result = await connection.QueryAsync<TipoServicioEntity>(
                "[dbo].[PA_Lg_Tip_Ser_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarTipoServicio(TipoServicioEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Tip_Ser_Des", valores.Tip_Ser_Des);
            parametros.Add("@Usr_Reg", valores.Usr_Reg);

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Tip_Ser_I0001]"
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

    public async Task<(int Codigo, string Mensaje)> ActualizarTipoServicio(TipoServicioEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Tip_Ser_Id", valores.Tip_Ser_Id);
            parametros.Add("@Tip_Ser_Des", valores.Tip_Ser_Des);
            parametros.Add("@Flg_Est", valores.Flg_Est);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);
            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Tip_Ser_U0001]"
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
