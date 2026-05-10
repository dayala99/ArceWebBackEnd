using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IItemRepository
{
    Task<IEnumerable<ItemEntity>?> ListarItem(int? Itm_Id, string? Itm_Des, int? Itm_Grp, string? Flg_Est);
    Task<(int Codigo, string Mensaje)> RegistrarItem(ItemEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarItem(ItemEntity valores);
}
