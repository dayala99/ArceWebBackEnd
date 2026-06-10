
using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IOrdenCompraRepository
{
    Task<IEnumerable<OrdenCompraEntity>?> ListarOrdenCompraActivo(int? Ord_Com_Id, string? Ord_Com_Prv, string? Flg_Est);
    Task<IEnumerable<OrdenCompraEntity>?> ListarOrdenCompraModificar(int? Ord_Com_Id);
    Task<(int Codigo, string Mensaje, int Codigo_Orden_Compra)> RegistrarOrdenCompra(OrdenCompraEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarOdenCompra(OrdenCompraEntity valores);
    Task<IEnumerable<OrdenCompraEntity>?> ListarOrdenCompraPendienteAlmacen(int? Ord_Com_Id, string? Ord_Com_Prv, string? Flg_Est);
    Task<IEnumerable<OrdenCompraEntity>?> ListarCabeceraIngresoAlmacen(int? Ord_Com_Id);
    Task<(int Codigo, string Mensaje)> CambiarEstadoOrdenCompra(OrdenCompraEntity valores);
}
