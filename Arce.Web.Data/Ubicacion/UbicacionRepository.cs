using System.Data;
using System.Data.SqlClient;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class UbicacionRepository: IUbicacionRepository
{
    private readonly string _connectionString;

    public UbicacionRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<UbicacionEntity>?> ListarUbicacionActivo(int? Ubi_Id, string? Ubi_Des, string? Flg_Est)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ubi_Id", Ubi_Id);
            parametros.Add("@Ubi_Des", Ubi_Des);
            parametros.Add("@Flg_Est", Flg_Est);

            var result = await connection.QueryAsync<UbicacionEntity>(
                "[dbo].[PA_Lg_Ubicacion_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }
}
