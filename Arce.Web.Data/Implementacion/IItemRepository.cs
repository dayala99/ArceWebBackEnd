using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IItemRepository
{
    Task<IEnumerable<ItemEntity>?> ListarItem(string? Itm_Cod, string? Itm_Des, int? Itm_Grp, int? Itm_Sub_Grp, int? Itm_Det_Mat_Id,string? Flg_Est);
    Task<(int Codigo, string Mensaje)> RegistrarItem(ItemEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarItem(ItemEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarStockItem(ItemEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarStockItemIngresoDirecto(ItemEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarStockItemSalida(ItemEntity valores);
    Task<IEnumerable<ItemEntity>?> ListarStocksItems(int? Usr_Cen_Cos_Id, int? Alm_Det_Itm_Id);
    Task<(int Codigo, string Mensaje)> ActualizarStockItemSalidaAnulacion(ItemEntity valores);
}
