using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IAsignacionService
{
    Task<ServiceResponse<int>> RegistrarAsignacion(AsignacionCabeceraEntity valores);
    Task<ServiceResponse<int>> ActualizarAsignacion(AsignacionCabeceraEntity valores);
}
