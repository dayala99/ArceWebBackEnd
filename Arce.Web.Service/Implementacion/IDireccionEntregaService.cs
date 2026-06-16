using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IDireccionEntregaService
{
    Task<ServiceResponseList<DireccionEntregaEntity>?> ListarDireccionEntregaActivo(int? Dir_Id, string? Dir_Des, string? Flg_Est);
    Task<ServiceResponse<int>> RegistrarDireccionEntrega(DireccionEntregaEntity valores);
    Task<ServiceResponse<int>> ActualizarDireccionEntrega(DireccionEntregaEntity valores);
}
