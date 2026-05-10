using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IGrupoItemRepository
{
    Task<IEnumerable<GrupoItemEntity>?> ListarGrupoItem(int? Grp_Id, string? Grp_Des, string? Flg_Est);
    Task<(int Codigo, string Mensaje)> RegistrarGrupoItem(GrupoItemEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarGrupoItem(GrupoItemEntity valores);
}
