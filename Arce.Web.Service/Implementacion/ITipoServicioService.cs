using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface ITipoServicioService
{
    Task<ServiceResponseList<TipoServicioEntity>?> ListarTipoServicioActivo(int? Tip_Ser_Id, string? Tip_Ser_Des, string? Flg_Est);
    Task<ServiceResponse<int>> RegistrarTipoServicio(TipoServicioEntity valores);
    Task<ServiceResponse<int>> ActualizarTipoServicio(TipoServicioEntity valores);
}
