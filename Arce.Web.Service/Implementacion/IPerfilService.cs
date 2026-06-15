using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IPerfilService
{
    Task<ServiceResponseList<PerfilEntity>?> ListarPerfil(string? Prf_Cod, string? Prf_Des, string? Flg_Est);
}
