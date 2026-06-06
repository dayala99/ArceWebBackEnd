using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IItemService
{
    Task<ServiceResponseList<ItemEntity>?> ListarItem(string? Itm_Cod, string? Itm_Des, int? Itm_Grp, int? Itm_Sub_Grp, int? Itm_Det_Mat_Id,string? Flg_Est);
    Task<ServiceResponse<int>> RegistrarItem(ItemEntity valores);
    Task<ServiceResponse<int>> ActualizarItem(ItemEntity valores);
}
