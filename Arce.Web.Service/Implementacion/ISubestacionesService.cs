using Arce.Web.Entity.Inspecciones;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface ISubestacionesService
{
    Task<ServiceResponseList<SubEstacionEntity>?> ListarSubEstaciones(int? Id, string? Nombre, int? Cliente_Id, string? Estado);
    Task<ServiceResponseList<SubEstacionEntity>?> ConsultarEditarSubEstaciones(int? Subestacion_Id);
    Task<ServiceResponse<int>> RegistrarSubEstacion(SubEstacionEntity valores);
    Task<ServiceResponse<int>> ActualizarSubEstacion(SubEstacionEntity valores);
}
