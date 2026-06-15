using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IPerfilRepository
{
    Task<IEnumerable<PerfilEntity>?> ListarPerfil(string? Prf_Cod, string? Prf_Des, string? Flg_Est);
}
