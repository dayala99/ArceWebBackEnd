using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IAsignacionRepository
{
    Task<(int Codigo, string Mensaje)> RegistrarAsignacion(AsignacionCabeceraEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarAsignacion(AsignacionCabeceraEntity valores);
}
