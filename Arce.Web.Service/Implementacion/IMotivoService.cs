using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IMotivoService
{
    Task<ServiceResponseList<MotivoEntity>?> ListarMotivo(int? Id, string? Nombre, string? Estado);
    Task<ServiceResponseList<MotivoEntity>?> ConsultarDatosMotivo(int? Motivo_Id);
    Task<ServiceResponse<int>> RegistrarMotivo(MotivoEntity valores);
    Task<ServiceResponse<int>> ActualizarMotivo(MotivoEntity valores);
    Task<ServiceResponse<int>> EliminarMotivo(int? Id, string? Usr_Mod);
}
