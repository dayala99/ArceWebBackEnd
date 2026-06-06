using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IDetraccionService
{
    Task<ServiceResponseList<DetraccionEntity>?> ListarDetraccion(int? Det_Id, string? Det_Des, string? Flg_Est);
    Task<ServiceResponse<int>> RegistrarDetraccion(DetraccionEntity valores);
    Task<ServiceResponse<int>> ActualizarDetraccion(DetraccionEntity valores);
}
