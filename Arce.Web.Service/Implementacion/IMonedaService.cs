using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IMonedaService
{
    Task<ServiceResponseList<MonedaEntity>?> ListarMoneda(int? Mon_Id, string? Mon_Des, string? Flg_Est);
    Task<ServiceResponse<int>> RegistrarMoneda(MonedaEntity valores);
    Task<ServiceResponse<int>> ActualizarMoneda(MonedaEntity valores);
}
