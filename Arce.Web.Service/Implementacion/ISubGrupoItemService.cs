using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface ISubGrupoItemService
{
    Task<ServiceResponseList<SubGrupoItemEntity>?> ListarSubGrupoItem(string? Sub_Grp_Cod, string? Sub_Grp_Des, string? Flg_Est);
    Task<ServiceResponse<int>> RegistrarSubGrupoItem(SubGrupoItemEntity valores);
    Task<ServiceResponse<int>> ActualizarSubGrupoItem(SubGrupoItemEntity valores);
    Task<ServiceResponseList<SubGrupoItemEntity>?> ListarSubGrupoItemPorGrpId(int? Grp_Id);
}
