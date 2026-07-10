using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IJefeService
{
    Task<ServiceResponseList<JefeEntity>?> ListarJefe(int? Id, string? Reporte_Tipo, string? Estado);
    Task<ServiceResponseList<JefeEntity>?> ConsultarDatosJefe(int? Reporte_Id);
    Task<ServiceResponse<int>> RegistrarJefe(JefeEntity valores);
    Task<ServiceResponse<int>> ActualizarJefe(JefeEntity valores);
    Task<ServiceResponse<int>> EliminarJefe(int? Id, string? Usr_Mod);
}
