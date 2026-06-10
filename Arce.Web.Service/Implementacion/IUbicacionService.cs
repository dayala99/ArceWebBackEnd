using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IUbicacionService
{
    Task<ServiceResponseList<UbicacionEntity>?> ListarUbicacionActivo(int? Ubi_Id, string? Ubi_Des, string? Flg_Est);
}
