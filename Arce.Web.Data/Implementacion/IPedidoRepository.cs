using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IPedidoRepository
{
    Task<IEnumerable<PedidoCabeceraEntity>?> ListarPedido(int? Ped_Id, string? Prv_Nom, string? Flg_Est, string? Ped_Tip_Com);
    Task<IEnumerable<PedidoCabeceraEntity>?> ListarPedidoCorrelativoNuevo();
    Task<IEnumerable<PedidoCabeceraEntity>?> ListarPedidoModificar(int Ped_Id);
    Task<(int Codigo, string Mensaje)> RegistrarPedido(PedidoCabeceraEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarPedido(PedidoCabeceraEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarPedidoEstado(PedidoCabeceraEntity valores);
    Task<IEnumerable<PedidoCabeceraCentroCostoEntity>?> ListarPedidoRegistradoCentroCosto(int? Ped_Id);
    Task<IEnumerable<PedidoCabeceraCentroCostoEntity>?> ListarPedidoRegistradoCentroCostoModificar(int? Ped_Cen_Cos_Id);
    Task<(int Codigo, string Mensaje)> RegistrarCentroCostoPedidoRegistrado(PedidoCabeceraCentroCostoEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarCentroCostoPedidoRegistrado(PedidoCabeceraCentroCostoEntity valores);
    Task<IEnumerable<PedidoCabeceraCentroCostoEntity>?> ObtenerTotalPedidoPorCenCos(int Ped_Id, string Ped_Cen_Cos);
    Task<IEnumerable<PedidoDetalleEntity>?> ListarDetallePedido(int Ped_Cab_Id);
    Task<IEnumerable<PedidoDetalleEntity>?> ListarDetallePedidoModificar(int Ped_Det_Id);
    Task<(int Codigo, string Mensaje)> RegistrarDetallePedido(PedidoDetalleEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarDetallePedido(PedidoDetalleEntity valores);
    
}
