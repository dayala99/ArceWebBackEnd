using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IItemDetalleMaterialService
{
    Task<ServiceResponseList<ItemDetalleMaterialEntity>?> ListarItemDetalleMaterial(string? Det_Mat_Cod, string? Det_Mat_Des, int? Grp_Id, int? Sub_Grp_Id, string? Flg_Est);
    Task<ServiceResponse<int>> RegistrarItemDetalleMaterial(ItemDetalleMaterialEntity valores);
    Task<ServiceResponse<int>> ActualizarItemDetalleMaterial(ItemDetalleMaterialEntity valores);
    Task<ServiceResponseList<ItemDetalleMaterialEntity>?> ListarItemDetalleMaterialPorGrupoySubgrupo(int? Grp_Id, int? Sub_Grp_Id);
}
