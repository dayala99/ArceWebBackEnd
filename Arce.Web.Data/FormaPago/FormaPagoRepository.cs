using System.Data;
using System.Data.SqlClient;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class FormaPagoRepository: IFormaPagoRepository
{
    private readonly string _connectionString;

    public FormaPagoRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<FormaPagoEntity>?> ListarFormaPagoActivo(int? For_Pag_Id, string? For_Pag_Des, string? Flg_Est)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@For_Pag_Id", For_Pag_Id);
            parametros.Add("@For_Pag_Des", For_Pag_Des);
            parametros.Add("@Flg_Est", Flg_Est);

            var result = await connection.QueryAsync<FormaPagoEntity>(
                "[dbo].[PA_Lg_For_Pag_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarFormaPago(FormaPagoEntity valores)
    {
        using(var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@For_Pag_Des", valores.For_Pag_Des);
            parametros.Add("@Usr_Reg",valores.Usr_Reg);
            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_For_Pag_I0001]"
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

    public async Task<(int Codigo, string Mensaje)> ActualizarFormaPago(FormaPagoEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();

            parametros.Add("@For_Pag_Id", valores.For_Pag_Id);
            parametros.Add("@For_Pag_Des", valores.For_Pag_Des);
            parametros.Add("@Flg_Est", valores.Flg_Est);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_For_Pag_U0001]"
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
