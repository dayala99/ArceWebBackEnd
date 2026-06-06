using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface ISubGrupoItemRepository
{
    Task<IEnumerable<SubGrupoItemEntity>?> ListarSubGrupoItem(string? Sub_Grp_Cod, string? Sub_Grp_Des, string? Flg_Est);
    Task<(int Codigo, string Mensaje)> RegistrarSubGrupoItem(SubGrupoItemEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarSubGrupoItem(SubGrupoItemEntity valores);
    Task<IEnumerable<SubGrupoItemEntity>?> ListarSubGrupoItemPorGrpId(int? Grp_Id);
}
