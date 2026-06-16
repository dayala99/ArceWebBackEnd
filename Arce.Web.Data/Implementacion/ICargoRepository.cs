using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface ICargoRepository
{
    Task<IEnumerable<CargoEntity>?> ListarCargo(int? Cargo_Id, string? Cargo_Nombre);
}
