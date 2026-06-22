using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IPedidoService
{
    Task<ServiceResponseList<PedidoCabeceraEntity>?> ListarPedido(int? Ped_Id, string? Flg_Est, int? Ped_Tip_Com, string? Usr_Cod);
    Task<ServiceResponseList<PedidoCabeceraEntity>?> ListarPedidoCorrelativoNuevo();
    Task<ServiceResponseList<PedidoCabeceraEntity>?> ListarPedidoModificar(int Ped_Id);
    Task<ServiceResponse<int>> RegistrarPedido(PedidoCabeceraEntity valores);
    Task<ServiceResponse<int>> ActualizarPedido(PedidoCabeceraEntity valores);
    Task<ServiceResponse<int>> ActualizarPedidoEstado(PedidoCabeceraEntity valores);
    Task<ServiceResponseList<PedidoCabeceraCentroCostoEntity>?> ListarPedidoRegistradoCentroCosto(int? Ped_Id);
    Task<ServiceResponseList<PedidoCabeceraCentroCostoEntity>?> ListarPedidoRegistradoCentroCostoModificar(int? Ped_Cen_Cos_Id);
    Task<ServiceResponse<int>> RegistrarCentroCostoPedidoRegistrado(PedidoCabeceraCentroCostoEntity valores);
    Task<ServiceResponse<int>> EliminarCentroCostoPedidoRegistrado(PedidoCabeceraCentroCostoEntity valores);
    Task<ServiceResponseList<PedidoCabeceraCentroCostoEntity>?> ObtenerTotalPedidoPorCenCos(int Ped_Id, string Ped_Cen_Cos);
    Task<ServiceResponseList<PedidoDetalleEntity>?> ListarDetallePedido(int Ped_Cab_Id);
    Task<ServiceResponseList<PedidoDetalleEntity>?> ListarDetallePedidoModificar(int Ped_Det_Id);
    Task<ServiceResponse<int>> RegistrarDetallePedido(PedidoDetalleEntity valores);
    Task<ServiceResponse<int>> ActualizarDetallePedido(PedidoDetalleEntity valores);
    Task<ServiceResponse<int>> EliminarDetallePedido(PedidoDetalleEntity valores);
    Task<ServiceResponse<int>> AsignarOrdenCompra(PedidoCabeceraCentroCostoEntity valores);
    Task<ServiceResponse<int>> AsignarOrdenCompraADetallePedido(PedidoDetalleEntity valores);
    Task<ServiceResponseList<PedidoDetalleEntity>?> ListarItemsAsignadosPedidoCentroCosto(int Ped_Cab_Id);
    Task<ServiceResponseList<PedidoDetalleEntity>?> ListarItemsAsignadosPedidoCentroCostoModificar(int Ord_Com_Id, int Ped_Cab_Id);
    Task<ServiceResponseList<PedidoCabeceraEntity>?> CargarReportePedido(string Ped_Id);
    Task<ServiceResponse<int>> DesAsignarOrdenCompraADetallePedido(PedidoDetalleEntity valores);
    Task<ServiceResponseList<PedidoCabeceraEntity>?> ListarPedidoAprobadoParaOC(int? Ped_Id, string? Flg_Est, int? Ped_Tip_Com);
    Task<ServiceResponse<int>> ActualizarPedidoCuandoDetalleCompleto(PedidoCabeceraEntity valores);
    Task<ServiceResponseList<PedidoDetalleEntity>?> ListarDetalleIngresoAlmacen(int? Ped_Cab_Id, int? Ord_Com_Id);
    Task<ServiceResponse<int>> ActualizarPedidoDetalleIngresoAlmacen(PedidoDetalleEntity valores);
    Task<ServiceResponse<int>> RechazarPedido(PedidoCabeceraEntity valores);
    Task<ServiceResponse<int>> ActualizarReferenciaGeneral(PedidoCabeceraEntity valores);
}
