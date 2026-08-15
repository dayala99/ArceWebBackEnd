using System.Data;
using System.Data.SqlClient;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class ClienteWbRepository: IClienteWbRepository
{
    private readonly string _connectionString;

    public ClienteWbRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<ClienteEntityWb>?> ListarCliente(int? Cli_Id, string? Cli_Nom, string? Cli_Ruc, string? Flg_Est)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Cli_Id", Cli_Id);
            parametros.Add("@Cli_Nom", Cli_Nom);
            parametros.Add("@Cli_Ruc", Cli_Ruc);
            parametros.Add("@Flg_Est", Flg_Est);
            
            var result = await connection.QueryAsync<ClienteEntityWb>(
                "[dbo].[PA_Lg_Cliente_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }
}
