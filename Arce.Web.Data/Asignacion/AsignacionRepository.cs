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

    // public async Task<IEnumerable<AsignacionCabeceraEntity>?> ListarBanco(int? Ban_Id, string? Ban_Des, string? Flg_Est)
    // {
    //     using (var connection = new SqlConnection(_connectionString))
    //     {
    //         await connection.OpenAsync();

    //         var parametros = new DynamicParameters();
    //         parametros.Add("@Ban_Id", Ban_Id);
    //         parametros.Add("@Ban_Des", Ban_Des);
    //         parametros.Add("@Flg_Est", Flg_Est);

    //         var result = await connection.QueryAsync<BancoEntity>(
    //             "[dbo].[PA_Lg_Banco_S0001]"
    //             , parametros
    //             , commandType: CommandType.StoredProcedure
    //         );

    //         return result;
    //     }
    // }

    public async Task<(int Codigo, string Mensaje)> RegistrarAsignacion(AsignacionCabeceraEntity valores)
    {
        using(var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Asg_Fec", valores.Asg_Fec);
            parametros.Add("@Asg_Usr", valores.Asg_Usr);
            parametros.Add("@Asg_Usr_Cen_Cos",valores.Asg_Usr_Cen_Cos);
            parametros.Add("@Usr_Reg", valores.Usr_Reg);
            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

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

            var Codigo = parametros.Get<int>("@Codigo");
            var Mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, Mensaje);
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
}
