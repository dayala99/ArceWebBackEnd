using System.Data;
using System.Data.SqlClient;
using System.Windows.Markup;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class OrdenCompraRepository: IOrdenCompraRepository
{
    private readonly string _connectionString;

    public OrdenCompraRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<OrdenCompraEntity>?> ListarOrdenCompraActivo(int? Ord_Com_Id, string? Ord_Com_Prv, string? Flg_Est)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Ord_Com_Id", Ord_Com_Id);
            parametros.Add("@Ord_Com_Prv", Ord_Com_Prv);
            parametros.Add("@Flg_Est", Flg_Est);
            var result = await connection.QueryAsync<OrdenCompraEntity>(
                    "[dbo].[PA_Lg_Orden_Compra_S0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
            );
            return result;
        }
    }

    public async Task<IEnumerable<OrdenCompraEntity>?> ListarOrdenCompraModificar(int? Ord_Com_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Ord_Com_Id", Ord_Com_Id);

            var result = await connection.QueryAsync<OrdenCompraEntity>(
                    "[dbo].[PA_Lg_Orden_Compra_S0002]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
            );
            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje, int Codigo_Orden_Compra)> RegistrarOrdenCompra(OrdenCompraEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            
            parametros.Add("@Ord_Com_Prv", valores.Ord_Com_Prv);
            parametros.Add("@Ord_Com_For_Pag", valores.Ord_Com_For_Pag);
            parametros.Add("@Ord_Com_Ref_Obr", valores.Ord_Com_Ref_Obr);
            parametros.Add("@Ord_Com_Obs", valores.Ord_Com_Obs);
            parametros.Add("@Ord_Com_Ref", valores.Ord_Com_Ref);
            parametros.Add("@Ord_Com_Sub_Tot", valores.Ord_Com_Sub_Tot);
            parametros.Add("@Ord_Com_Igv", valores.Ord_Com_Igv);
            parametros.Add("@Ord_Com_Tot", valores.Ord_Com_Tot);
            parametros.Add("@Ord_Com_Ped_Id", valores.Ord_Com_Ped_Id);
            parametros.Add("@Usr_Reg", valores.Usr_Reg);
            parametros.Add("@Ord_Com_Arc_Adj_Nom", valores.Ord_Com_Arc_Adj_Nom);
            parametros.Add("@Ord_Com_Arc_Adj_Rut", valores.Ord_Com_Arc_Adj_Rut);
            parametros.Add("@Ord_Com_Det_Id", valores.Ord_Com_Det_Id);
            parametros.Add("@Ord_Com_Det_Mon", valores.Ord_Com_Det_Mon);

            parametros.Add("@Ord_Com_Id", 0);
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Ord_Com_Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Orden_Compra_I0001]"
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
            var CodigoOrdenCompra = parametros.Get<int>("@Ord_Com_Id");
            return (Codigo, Mensaje, CodigoOrdenCompra);
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarOdenCompra(OrdenCompraEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();

            parametros.Add("@Ord_Com_Id", valores.Ord_Com_Id);
            parametros.Add("@Ord_Com_Prv", valores.Ord_Com_Prv);
            parametros.Add("@Ord_Com_For_Pag", valores.Ord_Com_For_Pag);
            parametros.Add("@Ord_Com_Ref_Obr", valores.Ord_Com_Ref_Obr);
            parametros.Add("@Ord_Com_Obs", valores.Ord_Com_Obs);
            parametros.Add("@Ord_Com_Ref", valores.Ord_Com_Ref);
            parametros.Add("@Ord_Com_Sub_Tot", valores.Ord_Com_Sub_Tot);
            parametros.Add("@Ord_Com_Igv", valores.Ord_Com_Igv);
            parametros.Add("@Ord_Com_Tot", valores.Ord_Com_Tot);
            parametros.Add("@Ord_Com_Ped_Id", valores.Ord_Com_Ped_Id);
            parametros.Add("@Flg_Est", valores.Flg_Est);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);
            parametros.Add("@Ord_Com_Arc_Adj_Nom", valores.Ord_Com_Arc_Adj_Nom);
            parametros.Add("@Ord_Com_Arc_Adj_Rut", valores.Ord_Com_Arc_Adj_Rut);
            parametros.Add("@Ord_Com_Det_Id", valores.Ord_Com_Det_Id);
            parametros.Add("@Ord_Com_Det_Mon", valores.Ord_Com_Det_Mon);
            
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Orden_Compra_U0001]"
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

    public async Task<IEnumerable<OrdenCompraEntity>?> ListarOrdenCompraPendienteAlmacen(int? Ord_Com_Id, string? Ord_Com_Prv, string? Flg_Est)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Ord_Com_Id", Ord_Com_Id);
            parametros.Add("@Ord_Com_Prv", Ord_Com_Prv);
            parametros.Add("@Flg_Est", Flg_Est);
            var result = await connection.QueryAsync<OrdenCompraEntity>(
                    "[dbo].[PA_Lg_Orden_Compra_S0003]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
            );
            return result;
        }
    }

    public async Task<IEnumerable<OrdenCompraEntity>?> ListarCabeceraIngresoAlmacen(int? Ord_Com_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var parametros = new DynamicParameters();
            parametros.Add("@Ord_Com_Id", Ord_Com_Id);

            var result = await connection.QueryAsync<OrdenCompraEntity>(
                    "[dbo].[PA_Lg_Orden_Compra_S0004]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
            );
            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> CambiarEstadoOrdenCompra(OrdenCompraEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();

            parametros.Add("@Ord_Com_Id", valores.Ord_Com_Id);
            parametros.Add("@Flg_Alm", valores.Ord_Com_Prv);
            
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Orden_Compra_U0002]"
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
