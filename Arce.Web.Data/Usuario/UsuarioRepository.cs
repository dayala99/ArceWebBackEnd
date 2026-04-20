using Arce.Web.Entity;
using Arce.Web.Entity.Usuario;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace Arce.Web.Data;
public class UsuarioRepository: IUsuarioRepository
{
    public readonly string _connectionString;
    public UsuarioRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<UsuarioEntity>?> ListarUsuarioActivo(string Usr_Id, string Usr_Cod, string Usr_Nom, string Flg_Est)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Usr_Id", Usr_Id);
            parametros.Add("@Usr_Cod", Usr_Cod);
            parametros.Add("@Usr_Nom", Usr_Nom);
            parametros.Add("@Flg_Est", Flg_Est);

            var result = await connection.QueryAsync<UsuarioEntity>(
                    "[dbo].[PA_Sg_Usuario_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
            );
            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarUsuario(UsuarioEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            //PARAMETROS ENTRADA
            parametros.Add("@Usr_Cod", valores.Usr_Cod);
            parametros.Add("@Usr_Nom", valores.Usr_Nom);
            parametros.Add("@Flg_Est", valores.Flg_Est);
            parametros.Add("@Usr_Reg", valores.Usr_Reg);
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");
            //PARAMETROS SALIDA
            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Sg_Usuario_I0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
            }
            var Codigo = parametros.Get<int>("@Codigo");
            var mensaje = parametros.Get<string>("@sMsj");
            return (Codigo, mensaje);
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarUsuario(UsuarioEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();

            parametros.Add("@Usr_Id", valores.Usr_Id);
            parametros.Add("@Usr_Cod", valores.Usr_Cod);
            parametros.Add("@Usr_Nom", valores.Usr_Nom);
            parametros.Add("@Flg_Est", valores.Flg_Est);
            parametros.Add("@Usr_Reg", valores.Usr_Reg);
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                //EJECUTAR EL STORED PROCEDURE
                connection.Execute(
                    "[dbo].[PA_Sg_Usuario_U0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
            }
            var Codigo = parametros.Get<int>("@Codigo");
            var mensaje = parametros.Get<string>("@sMsj");
            return (Codigo, mensaje);
        }
    }

}   
