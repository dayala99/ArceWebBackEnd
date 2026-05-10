using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IGrupoItemService
{
    Task<ServiceResponseList<GrupoItemEntity>?> ListarGrupoItem(int? Grp_Id, string? Grp_Des, string? Flg_Est);
    Task<ServiceResponse<int>> RegistrarGrupoItem(GrupoItemEntity valores);
    Task<ServiceResponse<int>> ActualizarGrupoItem(GrupoItemEntity valores);
}
