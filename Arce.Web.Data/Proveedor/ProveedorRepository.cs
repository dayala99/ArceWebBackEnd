using Dapper;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Arce.Web.Entity.ProveedorEntity;
namespace Arce.Web.Data;

public class ProveedorRepository
{
    private readonly string _connectionString;

    public ProveedorRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;

        public async Task<IEnumerable<ProveedorEntity>?> ListarUsuarioActivo(string Usr_Id, string Usr_Cod, string Usr_Nom, string Flg_Est)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Usr_Id", Usr_Id);
                parametros.Add("@Usr_Cod", Usr_Cod);
                parametros.Add("@Usr_Nom", Usr_Nom);
                parametros.Add("@Flg_Est", Flg_Est);

                var result = await connection.QueryAsync<ProveedorEntity>(
                        "[dbo].[PA_Sg_Usuario_S0001]"
                        , parametros
                        , commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }


    }

}
