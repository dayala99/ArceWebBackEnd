using System.Data;
using System.Data.SqlClient;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class CargoRepository: ICargoRepository
{
    private readonly string _connectionString;

    public CargoRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<CargoEntity>?> ListarCargo(int? Cargo_Id, string? Cargo_Nombre)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Cargo_Id", Cargo_Id);
            parametros.Add("@Cargo_Nombre", Cargo_Nombre);

            var result = await connection.QueryAsync<CargoEntity>(
                "[dbo].[SP_Consulta_Ins_Cargo]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }
}
