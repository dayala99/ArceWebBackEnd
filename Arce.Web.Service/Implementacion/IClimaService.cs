using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IClimaService
{
    Task<ServiceResponseList<ClimaEntity>?> ListarClima(int? Id, string? Nombre, string? Estado);
    Task<ServiceResponseList<ClimaEntity>?> ConsultarDatosClima(int? Clima_Id);
    Task<ServiceResponse<int>> RegistrarClima(ClimaEntity valores);
    Task<ServiceResponse<int>> ActualizarClima(ClimaEntity valores);
    Task<ServiceResponse<int>> EliminarClima(int? Id, string? Usr_Mod);
}
