using System.Data;
using System.Data.SqlClient;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class ObraRepository: IObraRepository
{
    private readonly string _connectionString;

    public ObraRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<ObraEntity>?> ListarObra(
        int? Obr_Id, int? Obr_Cen_Cos, string? Obr_Nom, string? Obr_Ubi, string? Obr_Are,
        int? Obr_Cli_Id, string? Flg_Est, string? Obr_Rsp
        )
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Obr_Id", Obr_Id);
            parametros.Add("@Obr_Cen_Cos", Obr_Cen_Cos);
            parametros.Add("@Obr_Nom", Obr_Nom);
            parametros.Add("@Obr_Ubi", Obr_Ubi);
            parametros.Add("@Obr_Are", Obr_Are);
            parametros.Add("@Obr_Cli_Id", Obr_Cli_Id);
            parametros.Add("@Flg_Est", Flg_Est);
            parametros.Add("@Obr_Rsp", Obr_Rsp);

            var result = await connection.QueryAsync<ObraEntity>(
                "[dbo].[PA_Py_Obra_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarObra(ObraEntity valores)
    {
        using(var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Obr_Cen_Cos", valores.Obr_Cen_Cos);
            parametros.Add("@Obr_Nom", valores.Obr_Nom);
            parametros.Add("@Obr_Ubi",valores.Obr_Ubi);
            parametros.Add("@Obr_Are",valores.Obr_Are);
            parametros.Add("@Obr_Cli_Id",valores.Obr_Cli_Id);
            parametros.Add("@Obr_Cod_Con",valores.Obr_Cod_Con);
            parametros.Add("@Flg_Est",valores.Flg_Est);
            parametros.Add("@Obr_Fec_Ape",valores.Obr_Fec_Ape);
            parametros.Add("@Obr_Rsp",valores.Obr_Rsp);
            parametros.Add("@Usr_Reg",valores.Usr_Reg);

            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Py_Obra_I0001]"
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

    public async Task<(int Codigo, string Mensaje)> ActualizarObra(ObraEntity valores)
    {
        using(var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Obr_Id", valores.Obr_Id);
            parametros.Add("@Obr_Cen_Cos", valores.Obr_Cen_Cos);
            parametros.Add("@Obr_Nom", valores.Obr_Nom);
            parametros.Add("@Obr_Ubi",valores.Obr_Ubi);
            parametros.Add("@Obr_Are",valores.Obr_Are);
            parametros.Add("@Obr_Cli_Id",valores.Obr_Cli_Id);
            parametros.Add("@Obr_Cod_Con",valores.Obr_Cod_Con);
            parametros.Add("@Obr_Fec_Ape",valores.Obr_Fec_Ape);
            parametros.Add("@Obr_Rsp",valores.Obr_Rsp);
            parametros.Add("@Usr_Mod",valores.Usr_Mod);

            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Py_Obra_U0001]"
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

    public async Task<IEnumerable<ObraEntity>?> CargarObraModificar(int? Obr_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Obr_Id", Obr_Id);

            var result = await connection.QueryAsync<ObraEntity>(
                "[dbo].[PA_Py_Obra_S0002]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarFechaInicioObra(ObraEntity valores)
    {
        using(var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Obr_Id", valores.Obr_Id);
            parametros.Add("@Obr_Fec_Ini", valores.Obr_Fec_Ini);

            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Py_Obra_U0002]"
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

    public async Task<(int Codigo, string Mensaje)> ActualizarFechaFinObra(ObraEntity valores)
    {
        using(var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Obr_Id", valores.Obr_Id);
            parametros.Add("@Obr_Fec_Fin", valores.Obr_Fec_Fin);

            parametros.Add("@Codigo",0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Py_Obra_U0003]"
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

    public async Task<(int Codigo, string Mensaje)> ActualizarFechaCierreObra(ObraEntity valores)
    {
        using(var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Obr_Id", valores.Obr_Id);
            parametros.Add("@Obr_Fec_Cie", valores.Obr_Fec_Cie);

            parametros.Add("@Codigo", 0); 
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Py_Obra_U0004]"
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
}
