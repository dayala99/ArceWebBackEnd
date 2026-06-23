using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface ISubContrataService
{
    Task<ServiceResponseList<SubContrataEntity>?> ListarSubContrata(int? Id, string? Nombre, string? Estado);
    Task<ServiceResponseList<SubContrataEntity>?> ConsultarDatosSubContrata(int? SubContrata_Id);
    Task<ServiceResponse<int>> RegistrarSubContrata(SubContrataEntity valores);
    Task<ServiceResponse<int>> ActualizarSubContrata(SubContrataEntity valores);
    Task<ServiceResponse<int>> EliminarSubContrata(int? Id, string? Usr_Mod);
}
