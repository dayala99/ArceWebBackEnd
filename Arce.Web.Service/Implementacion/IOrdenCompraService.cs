using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IOrdenCompraService
{
    Task<ServiceResponseList<OrdenCompraEntity>?> ListarOrdenCompraActivo(int? Ord_Com_Id, string? Ord_Com_Prv, string? Flg_Est);
    Task<ServiceResponseList<OrdenCompraEntity>?> ListarOrdenCompraModificar(int? Ord_Com_Id);
    Task<ServiceResponse<int>> RegistrarOrdenCompra(OrdenCompraEntity valores);
    Task<ServiceResponse<int>> ActualizarOdenCompra(OrdenCompraEntity valores);
    Task<ServiceResponseList<OrdenCompraEntity>?> ListarOrdenCompraPendienteAlmacen(int? Ord_Com_Id, string? Ord_Com_Prv, string? Flg_Est);
    Task<ServiceResponseList<OrdenCompraEntity>?> ListarCabeceraIngresoAlmacen(int? Ord_Com_Id);
}
