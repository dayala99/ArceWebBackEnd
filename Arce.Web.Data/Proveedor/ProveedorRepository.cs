using Dapper;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Arce.Web.Entity.Proveedor;
namespace Arce.Web.Data;

public class ProveedorRepository: IProveedorRepository
{
    private readonly string _connectionString;

    public ProveedorRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<ProveedorEntity>?> ListarProveedorActivo(int? Prv_Id, string? Prv_Nom, string? Prv_Ruc, string? Prv_Nom_Con, string? Flg_Est)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Prv_Id", Prv_Id);
            parametros.Add("@Prv_Nom", Prv_Nom);
            parametros.Add("@Prv_Ruc", Prv_Ruc);
            parametros.Add("@Prv_Nom_Con", Prv_Nom_Con);
            parametros.Add("@Flg_Est", Flg_Est);
            var result = await connection.QueryAsync<ProveedorEntity>(
                    "[dbo].[PA_Lg_Proveedor_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
            );
            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarProveedor(ProveedorEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            
            parametros.Add("@Prv_Nom", valores.Prv_Nom);
            parametros.Add("@Prv_Ruc", valores.Prv_Ruc);
            parametros.Add("@Prv_Tel", valores.Prv_Tel);
            parametros.Add("@Prv_Dir", valores.Prv_Dir);
            parametros.Add("@Prv_Nom_Con", valores.Prv_Nom_Con);
            parametros.Add("@Usr_Reg", valores.Usr_Reg);
            parametros.Add("@Prv_Email", valores.Prv_Email);
            parametros.Add("@Prv_Nro_Cue_Ban", valores.Prv_Nro_Cue_Ban);
            parametros.Add("@Prv_Nro_Cue_Ban_CCI", valores.Prv_Nro_Cue_Ban_CCI);
            parametros.Add("@Prv_Ban", valores.Prv_Ban);
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Proveedor_I0001]"
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

    public async Task<(int Codigo, string Mensaje)> ActualizarProveedor(ProveedorEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();

            parametros.Add("@Prv_Id", valores.Prv_Id);
            parametros.Add("@Prv_Nom", valores.Prv_Nom);
            parametros.Add("@Prv_Ruc", valores.Prv_Ruc);
            parametros.Add("@Prv_Tel", valores.Prv_Tel);
            parametros.Add("@Prv_Dir", valores.Prv_Dir);
            parametros.Add("@Prv_Nom_Con", valores.Prv_Nom_Con);
            parametros.Add("@Flg_Est", valores.Flg_Est);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);
            parametros.Add("@Prv_Email", valores.Prv_Email);
            parametros.Add("@Prv_Nro_Cue_Ban", valores.Prv_Nro_Cue_Ban);
            parametros.Add("@Prv_Nro_Cue_Ban_CCI", valores.Prv_Nro_Cue_Ban_CCI);
            parametros.Add("@Prv_Ban", valores.Prv_Ban);
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Proveedor_U0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, mensaje);
        }
    }

    public async Task<IEnumerable<ProveedorBancoEntity>?> ListarProveedorBanco(int? Prv_Ban_Id, int? Prv_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Prv_Ban_Id", Prv_Ban_Id);
            parametros.Add("@Prv_Id", Prv_Id);

            var result = await connection.QueryAsync<ProveedorBancoEntity>(
                    "[dbo].[PA_Lg_Proveedor_Banco_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
            );
            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarProveedorBanco(ProveedorBancoEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            
            parametros.Add("@Prv_Id", valores.Prv_Id);
            parametros.Add("@Ban_Id", valores.Ban_Id);
            parametros.Add("@Tip_Mon", valores.Tip_Mon);
            parametros.Add("@Prv_Ban_Nro_Cta", valores.Prv_Ban_Nro_Cta);
            parametros.Add("@Prv_Ban_Nro_Cta_CCI", valores.Prv_Ban_Nro_Cta_CCI);
            parametros.Add("Usr_Reg", valores.Usr_Reg);

            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Proveedor_Banco_I0001]"
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

    public async Task<(int Codigo, string Mensaje)> ActualizarProveedorBanco(ProveedorBancoEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();

            parametros.Add("@Prv_Ban_Id", valores.Prv_Ban_Id);
            parametros.Add("@Prv_Id", valores.Prv_Id);
            parametros.Add("@Ban_Id", valores.Ban_Id);
            parametros.Add("@Tip_Mon", valores.Tip_Mon);
            parametros.Add("@Prv_Ban_Nro_Cta", valores.Prv_Ban_Nro_Cta);
            parametros.Add("@Prv_Ban_Nro_Cta_CCI", valores.Prv_Ban_Nro_Cta_CCI);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);

            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Proveedor_Banco_U0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, mensaje);
        }
    }

    public async Task<(int Codigo, string Mensaje)> EliminarProveedorBanco(ProveedorBancoEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();

            parametros.Add("@Prv_Ban_Id", valores.Prv_Ban_Id);
            parametros.Add("@Prv_Id", valores.Prv_Id);

            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Proveedor_Banco_D0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, mensaje);
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarCuentaBancariaProveedor(ProveedorBancoEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();

            parametros.Add("@Prv_Ban_Id", valores.Prv_Ban_Id);
            parametros.Add("@Prv_Id", valores.Prv_Id);

            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Proveedor_U0003]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var mensaje = parametros.Get<string>("@sMsj");
            
            return (Codigo, mensaje);
        }
    }

}
