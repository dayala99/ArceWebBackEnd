using System.Data;
using System.Data.SqlClient;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Arce.Web.Data;

public class PedidoRepository: IPedidoRepository
{
    private readonly string _connectionString;

    public PedidoRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    //LISTAR TOTAL DE PEDIDOS
    public async Task<IEnumerable<PedidoCabeceraEntity>?> ListarPedido(int? Ped_Id, string? Prv_Nom, string? Flg_Est, string? Ped_Tip_Com)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Id", Ped_Id);
            parametros.Add("@Prv_Nom", Prv_Nom);
            parametros.Add("@Flg_Est", Flg_Est);
            parametros.Add("@Ped_Tip_Com", Ped_Tip_Com);

            var result = await connection.QueryAsync<PedidoCabeceraEntity>(
                "[dbo].[PA_Lg_Pedido_Cab_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    //LISTAR ULTIMO PEDIDO Y GENERAR CORRELATIVO NUEVO
    public async Task<IEnumerable<PedidoCabeceraEntity>?> ListarPedidoCorrelativoNuevo()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var result = await connection.QueryAsync<PedidoCabeceraEntity>(
                "[dbo].[PA_Lg_Pedido_Cab_S0001]"
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    //REGISTRAR PEDIDO PRINCIPAL
    public async Task<(int Codigo, string Mensaje)> RegistrarPedido(PedidoCabeceraEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Id", valores.Ped_Id);
            parametros.Add("@Ped_Usr_Apr", valores.Ped_Usr_Apr);
            parametros.Add("@Ped_Lug_Ent", valores.Ped_Lug_Ent);
            parametros.Add("@Ped_Ref", valores.Ped_Ref);
            parametros.Add("@Ped_Tip_Com", valores.Ped_Tip_Com);
            parametros.Add("@Ped_Tip_Mon", valores.Ped_Tip_Mon);
            parametros.Add("@Ped_Fec_Ent", valores.Ped_Fec_Ent);
            parametros.Add("@Ped_Sus", valores.Ped_Sus);
            parametros.Add("@Ped_Arc_Adj_Nom", valores.Ped_Arc_Adj_Nom);
            parametros.Add("@Ped_Arc_Adj_Rut", valores.Ped_Arc_Adj_Rut);
            parametros.Add("@Ped_Prv_Cod", valores.Ped_Prv_Cod);
            parametros.Add("@Ped_For_Pag_Cod", valores.Ped_For_Pag_Cod);
            parametros.Add("@Usr_Reg", valores.Usr_Reg);
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size:255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Pedido_Cab_I0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var Mensaje = parametros.Get<string>("@sMsj");

            return (Codigo, Mensaje);
        }
    }

    //EDITAR PEDIDO PRINCIPAL
    public async Task<(int Codigo, string Mensaje)> ActualizarPedido(PedidoCabeceraEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Id", valores.Ped_Id);
            parametros.Add("@Ped_Usr_Apr", valores.Ped_Usr_Apr);
            parametros.Add("@Ped_Lug_Ent", valores.Ped_Lug_Ent);
            parametros.Add("@Ped_Ref", valores.Ped_Ref);
            parametros.Add("@Ped_Tip_Com", valores.Ped_Tip_Com);
            parametros.Add("@Ped_Tip_Mon", valores.Ped_Tip_Mon);
            parametros.Add("@Ped_Fec_Ent", valores.Ped_Fec_Ent);
            parametros.Add("@Ped_Sus", valores.Ped_Sus);
            parametros.Add("@Ped_Arc_Adj_Nom", valores.Ped_Arc_Adj_Nom);
            parametros.Add("@Ped_Arc_Adj_Rut", valores.Ped_Arc_Adj_Rut);
            parametros.Add("@Ped_Prv_Cod", valores.Ped_Prv_Cod);
            parametros.Add("@Ped_For_Pag_Cod", valores.Ped_For_Pag_Cod);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size:255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Pedido_Cab_U0001]"
                    , parametros
                    , commandType: CommandType.StoredProcedure
                );
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var Codigo = parametros.Get<int>("@Codigo");
            var Mensaje = parametros.Get<string>("@sMsj");

            return (Codigo, Mensaje);
        }
    }

    //CAMBIAR ESTADO DE PEDIDO PRINCIPAL
    public async Task<(int Codigo, string Mensaje)> ActualizarPedidoEstado(PedidoCabeceraEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Id", valores.Ped_Id);
            parametros.Add("@Flg_Est", valores.Flg_Est);

            parametros.Add("@Codigo", 0);
            parametros.Add("@sMjs", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Pedido_Cab_U0002]"
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

    //LISTAR CENTROS DE COSTOS REGISTRADOS EN CADA PEDIDO
    public async Task<IEnumerable<PedidoCabeceraCentroCostoEntity>?> ListarPedidoRegistradoCentroCosto(int? Ped_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Id", Ped_Id);

            var result = await connection.QueryAsync<PedidoCabeceraCentroCostoEntity>(
                "[dbo].[PA_Lg_Pedido_Cab_Cen_Cos_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    //REGISTRAR CENTROS DE COSTOS INGRESADOS EN LA GRILLA DE NUEVO PEDIDO
    public async Task<(int Codigo, string Mensaje)> RegistrarCentroCostoPedidoRegistrado(PedidoCabeceraCentroCostoEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Id", valores.Ped_Id);
            parametros.Add("@Ped_Cen_Cos", valores.Ped_Cen_Cos);
            parametros.Add("@Ped_Can", valores.Ped_Can);
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Pedido_Cab_Cen_Cos_I0001]"
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
