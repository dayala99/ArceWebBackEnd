using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IPedidoRepository
{
    Task<IEnumerable<PedidoCabeceraEntity>?> ListarPedido(int? Ped_Id, string? Prv_Nom, string? Flg_Est, string? Ped_Tip_Com);
    Task<IEnumerable<PedidoCabeceraEntity>?> ListarPedidoCorrelativoNuevo();
    Task<(int Codigo, string Mensaje)> RegistrarPedido(PedidoCabeceraEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarPedido(PedidoCabeceraEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarPedidoEstado(PedidoCabeceraEntity valores);
    Task<IEnumerable<PedidoCabeceraCentroCostoEntity>?> ListarPedidoRegistradoCentroCosto(int? Ped_Id);
    Task<(int Codigo, string Mensaje)> RegistrarCentroCostoPedidoRegistrado(PedidoCabeceraCentroCostoEntity valores);
    
}
