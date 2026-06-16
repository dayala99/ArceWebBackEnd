using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface ICargoService
{
    Task<ServiceResponseList<CargoEntity>?> ListarCargo(int? Cargo_Id, string? Cargo_Nombre);
}
