using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IItemService
{
    Task<ServiceResponseList<ItemEntity>?> ListarItem(int? Itm_Id, string? Itm_Des, int? Itm_Grp, string? Flg_Est);
    Task<ServiceResponse<int>> RegistrarItem(ItemEntity valores);
    Task<ServiceResponse<int>> ActualizarItem(ItemEntity valores);
}
