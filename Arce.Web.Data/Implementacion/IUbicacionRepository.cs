using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IUbicacionRepository
{
    Task<IEnumerable<UbicacionEntity>?> ListarUbicacionActivo(int? Ubi_Id, string? Ubi_Des, string? Flg_Est);
}
