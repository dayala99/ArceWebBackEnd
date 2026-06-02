
using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IOrdenCompraRepository
{
    Task<IEnumerable<OrdenCompraEntity>?> ListarOrdenCompraActivo(int? Ord_Com_Id, string? Ord_Com_Prv, string? Flg_Est);
    Task<(int Codigo, string Mensaje, int Codigo_Orden_Compra)> RegistrarOrdenCompra(OrdenCompraEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarOdenCompra(OrdenCompraEntity valores);
}
