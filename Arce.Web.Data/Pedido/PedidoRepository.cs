using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
    public async Task<IEnumerable<PedidoCabeceraEntity>?> ListarPedido(int? Ped_Id, string? Flg_Est, int? Ped_Tip_Com, string? Usr_Cod)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Id", Ped_Id);
            //parametros.Add("@Prv_Nom", Prv_Nom);
            parametros.Add("@Flg_Est", Flg_Est);
            parametros.Add("@Ped_Tip_Com", Ped_Tip_Com);
            parametros.Add("@Usr_Cod", Usr_Cod);

            var result = await connection.QueryAsync<PedidoCabeceraEntity>(
                "[dbo].[PA_Lg_Pedido_Cab_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            // var fallbackResult = await connection.QueryAsync<PedidoCabeceraEntity>(
            //     @"
            //     SELECT
            //         ped.Ped_Id,
            //         COALESCE(prv.Prv_Nom, '') AS Prv_Nom,
            //         ped.Ped_Usr_Apr,
            //         ped.Ped_Lug_Ent,
            //         ped.Ped_Ref,
            //         ped.Ped_Tip_Com,
            //         ped.Ped_Tip_Mon,
            //         CASE
            //             WHEN ped.Ped_Tip_Mon = 1 THEN 'PEN'
            //             WHEN ped.Ped_Tip_Mon = 2 THEN 'USD'
            //             ELSE ''
            //         END AS Ped_Tip_Mon_Des,
            //         ped.Ped_Fec_Ent,
            //         ped.Ped_Sus,
            //         ped.Ped_Arc_Adj_Nom,
            //         ped.Ped_Arc_Adj_Rut,
            //         ped.Ped_Prv_Cod,
            //         ped.Ped_For_Pag_Cod,
            //         COALESCE(NULLIF(ped.Ped_Tot, 0), det.Ped_Tot_Calculado, 0) AS Ped_Tot,
            //         ped.Flg_Est,
            //         CASE
            //             WHEN ped.Flg_Est = 'A' THEN 'Aprobado'
            //             WHEN ped.Flg_Est = 'P' THEN 'Pendiente'
            //             WHEN ped.Flg_Est = 'O' THEN 'Observado'
            //             WHEN ped.Flg_Est = 'C' THEN 'Cerrado'
            //             ELSE COALESCE(ped.Flg_Est, '')
            //         END AS Ped_Est_Des,
            //         CASE
            //             WHEN ped.Flg_Est = 'A' THEN 'Aprobado'
            //             WHEN ped.Flg_Est = 'P' THEN 'Pendiente'
            //             WHEN ped.Flg_Est = 'O' THEN 'Observado'
            //             WHEN ped.Flg_Est = 'C' THEN 'Cerrado'
            //             ELSE COALESCE(ped.Flg_Est, '')
            //         END AS Estado,
            //         ped.Usr_Reg,
            //         ped.Fec_Reg,
            //         ped.Usr_Mod,
            //         ped.Fec_Mod,
            //         ped.Ped_Can_Tot
            //     FROM dbo.Lg_Pedido_Cab ped
            //     LEFT JOIN dbo.Lg_Proveedor prv ON prv.Prv_Id = ped.Ped_Prv_Cod
            //     OUTER APPLY (
            //         SELECT SUM(COALESCE(det.Ped_Cos_Tot, 0)) AS Ped_Tot_Calculado
            //         FROM dbo.Lg_Pedido_Det det
            //         WHERE det.Ped_Cab_Id = ped.Ped_Id
            //     ) det
            //     WHERE (@Ped_Id IS NULL OR @Ped_Id <= 0 OR ped.Ped_Id = @Ped_Id)
            //       AND (@Flg_Est IS NULL OR @Flg_Est = '' OR @Flg_Est = 'Todos' OR ped.Flg_Est = @Flg_Est)
            //       AND (@Ped_Tip_Com IS NULL OR @Ped_Tip_Com = '' OR @Ped_Tip_Com = 'Todos' OR ped.Ped_Tip_Com = @Ped_Tip_Com)
            //       AND (@Prv_Nom IS NULL OR @Prv_Nom = '' OR COALESCE(prv.Prv_Nom, '') LIKE '%' + @Prv_Nom + '%')
            //     ",
            //     parametros
            // );

            // if (!result.Any())
            // {
            //     return fallbackResult;
            // }

            // var resultList = result.ToList();
            // var fallbackMap = fallbackResult
            //     .Where(item => item.Ped_Id.HasValue)
            //     .ToDictionary(item => item.Ped_Id!.Value, item => item);
            // var existingIds = resultList
            //     .Where(item => item.Ped_Id.HasValue)
            //     .Select(item => item.Ped_Id!.Value)
            //     .ToHashSet();

            // foreach (var item in resultList.Where(item => item.Ped_Id.HasValue))
            // {
            //     if (fallbackMap.TryGetValue(item.Ped_Id!.Value, out var fallbackItem) && fallbackItem.Ped_Tot.HasValue)
            //     {
            //         item.Ped_Tot = fallbackItem.Ped_Tot;
            //     }
            // }

            // foreach (var item in fallbackResult)
            // {
            //     if (!item.Ped_Id.HasValue || existingIds.Contains(item.Ped_Id.Value))
            //     {
            //         continue;
            //     }

            //     resultList.Add(item);
            // }

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
                "[dbo].[PA_Lg_Pedido_Cab_S0002]"
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    //LISTAR DATOS PEDIDO PARA MODIFICAR
    public async Task<IEnumerable<PedidoCabeceraEntity>?> ListarPedidoModificar(int Ped_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Id", Ped_Id);

            var result = await connection.QueryAsync<PedidoCabeceraEntity>(
                "[dbo].[PA_Lg_Pedido_Cab_S0003]"
                , parametros
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
            parametros.Add("@Ped_Can_Tot", valores.Ped_Can_Tot);
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
            parametros.Add("@Ped_Can_Tot", valores.Ped_Can_Tot);
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
            parametros.Add("@sMsj", "");

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

    //LISTAR CENTROS DE COSTOS REGISTRADOS EN CADA PEDIDO PARA MODIFICAR
    public async Task<IEnumerable<PedidoCabeceraCentroCostoEntity>?> ListarPedidoRegistradoCentroCostoModificar(int? Ped_Cen_Cos_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Cen_Cos_Id", Ped_Cen_Cos_Id);

            var result = await connection.QueryAsync<PedidoCabeceraCentroCostoEntity>(
                "[dbo].[PA_Lg_Pedido_Cab_Cen_Cos_S0002]"
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

    //ELIMINAR CENTROS DE COSTOS INGRESADOS EN LA GRILLA DE NUEVO PEDIDO
    public async Task<(int Codigo, string Mensaje)> EliminarCentroCostoPedidoRegistrado(PedidoCabeceraCentroCostoEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            // parametros.Add("@Ped_Id", valores.Ped_Id);
            parametros.Add("@Ped_Cen_Cos_Id", valores.Ped_Cen_Cos_Id);
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Pedido_Cab_Cen_Cos_D0001]"
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

    //OBTENER SUMA TOTAL DE CANTIDADES REGISTRADAS EN GRILLA CENTRO DE COSTO
    public async Task<IEnumerable<PedidoCabeceraCentroCostoEntity>?> ObtenerTotalPedidoPorCenCos(int Ped_Id, string Ped_Cen_Cos)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Id", Ped_Id);
            parametros.Add("@Ped_Cen_Cos", Ped_Cen_Cos);

            var result = await connection.QueryAsync<PedidoCabeceraCentroCostoEntity>(
                "[dbo].[PA_Lg_Pedido_Cab_Cen_Cos_S0002]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    //OBTENER SUMA TOTAL DE CANTIDADES REGISTRADAS EN GRILLA CENTRO DE COSTO
    public async Task<IEnumerable<PedidoDetalleEntity>?> ListarDetallePedido(int Ped_Cab_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Cab_Id", Ped_Cab_Id);

            var result = await connection.QueryAsync<PedidoDetalleEntity>(
                "[dbo].[PA_Lg_Pedido_Det_S0001]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    //OBTENER SUMA TOTAL DE CANTIDADES REGISTRADAS EN GRILLA CENTRO DE COSTO
    public async Task<IEnumerable<PedidoDetalleEntity>?> ListarDetallePedidoModificar(int Ped_Det_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Det_Id", Ped_Det_Id);

            var result = await connection.QueryAsync<PedidoDetalleEntity>(
                "[dbo].[PA_Lg_Pedido_Det_S0002]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }
    

    //REGISTRAR DETALLE DE PEDIDO
    public async Task<(int Codigo, string Mensaje)> RegistrarDetallePedido(PedidoDetalleEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Cab_Id", valores.Ped_Cab_Id);
            parametros.Add("@Ped_Cod_Itm", valores.Ped_Cod_Itm);
            parametros.Add("@Ped_Uni_Med", valores.Ped_Uni_Med);
            parametros.Add("@Ped_Can", valores.Ped_Can);
            parametros.Add("@Ped_Cos_Uni", valores.Ped_Cos_Uni);
            parametros.Add("@Ped_Cos_Tot", valores.Ped_Cos_Tot);
            parametros.Add("@Ped_Cen_Cos_Asg", valores.Ped_Cen_Cos_Asg);
            parametros.Add("@Usr_Reg", valores.Usr_Reg);
            
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Pedido_Det_I0001]"
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

    //ACTUALIZAR DETALLE DE PEDIDO
    public async Task<(int Codigo, string Mensaje)> ActualizarDetallePedido(PedidoDetalleEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Det_Id", valores.Ped_Det_Id);
            parametros.Add("@Ped_Cod_Itm", valores.Ped_Cod_Itm);
            parametros.Add("@Ped_Uni_Med", valores.Ped_Uni_Med);
            parametros.Add("@Ped_Can", valores.Ped_Can);
            parametros.Add("@Ped_Cos_Uni", valores.Ped_Cos_Uni);
            parametros.Add("@Ped_Cos_Tot", valores.Ped_Cos_Tot);
            parametros.Add("@Usr_Mod", valores.Usr_Mod);
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Pedido_Det_U0001]"
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

    //ACTUALIZAR DETALLE DE PEDIDO
    public async Task<(int Codigo, string Mensaje)> EliminarDetallePedido(PedidoDetalleEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Det_Id", valores.Ped_Det_Id);
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Pedido_Det_D0001]"
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

    public async Task <(int Codigo, string Mensaje)> AsignarOrdenCompra(PedidoCabeceraCentroCostoEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Cen_Cos_Id", valores.Ped_Cen_Cos_Id);
            parametros.Add("@Ped_Ord_Com_Id", valores.Ped_Ord_Com_Id);
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Pedido_Cab_Cen_Cos_U0001]"
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

    public async Task <(int Codigo, string Mensaje)> AsignarOrdenCompraADetallePedido(PedidoDetalleEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Det_Id", valores.Ped_Det_Id);
            parametros.Add("@Ord_Com_Id", valores.Ord_Com_Id);
            parametros.Add("@Ped_Obs", valores.Ped_Obs);
            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Pedido_Det_U0002]"
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

    public async Task<IEnumerable<PedidoDetalleEntity>?> ListarItemsAsignadosPedidoCentroCosto(int Ped_Cab_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Cab_Id", Ped_Cab_Id);
            //parametros.Add("@Ped_Ord_Com_Id", Ped_Ord_Com_Id);

            var result = await connection.QueryAsync<PedidoDetalleEntity>(
                "[dbo].[PA_Lg_Pedido_Det_S0004]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<IEnumerable<PedidoDetalleEntity>?> ListarItemsAsignadosPedidoCentroCostoModificar(int Ord_Com_Id, int Ped_Cab_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ord_Com_Id", Ord_Com_Id);
            parametros.Add("@Ped_Cab_Id", Ped_Cab_Id);

            var result = await connection.QueryAsync<PedidoDetalleEntity>(
                "[dbo].[PA_Lg_Pedido_Det_S0003]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }

    public async Task<IEnumerable<PedidoCabeceraEntity>> CargarReportePedido(string Ped_Id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var parametros = new DynamicParameters();
        parametros.Add("@Ped_Id", Ped_Id);

        using var multi = await connection.QueryMultipleAsync(
            "[dbo].[PA_Lg_Pedido_Cab_S0004]"
            , parametros
            , commandType: CommandType.StoredProcedure
        );
        var cabeceraPedido = (await multi.ReadAsync<PedidoCabeceraEntity>()).ToList();
        var detallePedido = multi.IsConsumed ? Enumerable.Empty<PedidoDetalleEntityReporte>()
                                        : await multi.ReadAsync<PedidoDetalleEntityReporte>();
        foreach (var cabPedido in cabeceraPedido)
        {
            cabPedido.Detalle_Reporte = detallePedido
                .Where(r => r.Ped_Cab_Id == cabPedido.Ped_Id)
                .ToList();
        }
        return cabeceraPedido;
    }

    public async Task <(int Codigo, string Mensaje)> DesAsignarOrdenCompraADetallePedido(PedidoDetalleEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Det_Id", valores.Ped_Det_Id);
            parametros.Add("@Ord_Com_Id", valores.Ord_Com_Id);

            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Pedido_Det_U0003]"
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

    public async Task<IEnumerable<PedidoCabeceraEntity>?> ListarPedidoAprobadoParaOC(int? Ped_Id, string? Flg_Est, int? Ped_Tip_Com)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Id", Ped_Id);
            //parametros.Add("@Prv_Nom", Prv_Nom);
            parametros.Add("@Flg_Est", Flg_Est);
            parametros.Add("@Ped_Tip_Com", Ped_Tip_Com);

            var result = await connection.QueryAsync<PedidoCabeceraEntity>(
                "[dbo].[PA_Lg_Pedido_Cab_S0005]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );
            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarPedidoCuandoDetalleCompleto(PedidoCabeceraEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Id", valores.Ped_Id);

            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Pedido_Cab_U0003]"
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

    public async Task<IEnumerable<PedidoDetalleEntity>?> ListarDetalleIngresoAlmacen(int? Ped_Cab_Id, int? Ord_Com_Id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Cab_Id", Ped_Cab_Id);
            parametros.Add("@Ord_Com_Id", Ord_Com_Id);

            var result = await connection.QueryAsync<PedidoDetalleEntity>(
                "[dbo].[PA_Lg_Pedido_Det_S0005]"
                , parametros
                , commandType: CommandType.StoredProcedure
            );
            return result;
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarPedidoDetalleIngresoAlmacen(PedidoDetalleEntity valores)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            
            var parametros = new DynamicParameters();
            parametros.Add("@Ped_Det_Id", valores.Ped_Det_Id);
            parametros.Add("@Ord_Com_Id", valores.Ord_Com_Id);
            parametros.Add("@Can_Ing", valores.Can_Ing);

            parametros.Add("@Codigo", 0);
            parametros.Add("@sMsj", "");

            parametros.Add("@Codigo", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@sMsj", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            try
            {
                connection.Execute(
                    "[dbo].[PA_Lg_Pedido_Det_U0004]"
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
