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
}
