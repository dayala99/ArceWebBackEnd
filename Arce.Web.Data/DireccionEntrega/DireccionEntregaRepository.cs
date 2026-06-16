using System.Data;
using System.Data.SqlClient;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class DireccionEntregaRepository: IDireccionEntregaRepository
{
    private readonly string _connectionString;

    public DireccionEntregaRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<DireccionEntregaEntity>?> ListarDireccionEntregaActivo(int? Dir_Id, string? Dir_Des, string? Flg_Est)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Dir_Id", Dir_Id);
            parametros.Add("@Dir_Des", Dir_Des);
            parametros.Add("@Flg_Est", Flg_Est);
            
            var result = await connection.QueryAsync<DireccionEntregaEntity>(
                "[dbo].[PA_Lg_Direccion_Entrega_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarDireccionEntrega(DireccionEntregaEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Dir_Des", valores.Dir_Des);
            parametros.Add("@Usr_Reg", valores.Usr_Reg);
            parametros.Add("@Dir_Ubi", valores.Dir_Ubi);
            
            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size:255, direction:ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Direccion_Entrega_I0001]"
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

    public async Task<(int Codigo, string Mensaje)> ActualizarDireccionEntrega(DireccionEntregaEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Dir_Id", valores.Dir_Id);
            parametros.Add("@Dir_Des", valores.Dir_Des);
            parametros.Add("@Flg_Est", valores.Flg_Est);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);
            parametros.Add("@Dir_Ubi", valores.Dir_Ubi);
            
            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size:255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Direccion_Entrega_U0001]"
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
