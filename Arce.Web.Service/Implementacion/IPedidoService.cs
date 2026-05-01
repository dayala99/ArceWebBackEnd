using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IPedidoService
{
    Task<ServiceResponseList<PedidoCabeceraEntity>?> ListarPedido(int? Ped_Id, string? Prv_Nom, string? Flg_Est, string? Ped_Tip_Com);
    Task<ServiceResponseList<PedidoCabeceraEntity>?> ListarPedidoCorrelativoNuevo();
    Task<ServiceResponse<int>> RegistrarPedido(PedidoCabeceraEntity valores);
    Task<ServiceResponse<int>> ActualizarPedido(PedidoCabeceraEntity valores);
    Task<ServiceResponse<int>> ActualizarPedidoEstado(PedidoCabeceraEntity valores);
    Task<ServiceResponseList<PedidoCabeceraCentroCostoEntity>?> ListarPedidoRegistradoCentroCosto(int? Ped_Id);
    Task<ServiceResponse<int>> RegistrarCentroCostoPedidoRegistrado(PedidoCabeceraCentroCostoEntity valores);
    Task<ServiceResponseList<PedidoCabeceraCentroCostoEntity>?> ObtenerTotalPedidoPorCenCos(int Ped_Id, string Ped_Cen_Cos);
    Task<ServiceResponseList<PedidoDetalleEntity>?> ListarDetallePedido(int Ped_Cab_Id);
    Task<ServiceResponseList<PedidoDetalleEntity>?> ListarDetallePedidoModificar(int Ped_Det_Id);
    Task<ServiceResponse<int>> RegistrarDetallePedido(PedidoDetalleEntity valores);
    Task<ServiceResponse<int>> ActualizarDetallePedido(PedidoDetalleEntity valores);
}
