using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IJefeService
{
    Task<ServiceResponseList<JefeEntity>?> ListarJefe(int? Id, string? Nombre, string? Dni, string? Estado, int? Cen_Cos_Id);
    Task<ServiceResponseList<JefeEntity>?> ConsultarDatosJefe(int? Jefe_Id);
    Task<ServiceResponse<int>> RegistrarJefe(JefeEntity valores);
    Task<ServiceResponse<int>> ActualizarJefe(JefeEntity valores);
    Task<ServiceResponse<int>> EliminarJefe(int? Id, string? Usr_Mod);
}
