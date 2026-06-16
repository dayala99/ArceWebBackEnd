using System.Data;
using System.Data.SqlClient;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class PerfilRepository: IPerfilRepository
{
    private readonly string _connectionString;

    public PerfilRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }
    public async Task<IEnumerable<PerfilEntity>?> ListarPerfil(string? Prf_Cod, string? Prf_Des, string? Flg_Est)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("Prf_Cod", Prf_Cod);
            parametros.Add("@Prf_Des", Prf_Des);
            parametros.Add("@Flg_Est", Flg_Est);
            

            var result = await connection.QueryAsync<PerfilEntity>(
                "[dbo].[PA_Sg_Perfil_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }
}
