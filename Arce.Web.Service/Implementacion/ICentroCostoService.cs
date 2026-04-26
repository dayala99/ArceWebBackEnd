using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface ICentroCostoService
{
    Task<ServiceResponseList<CentroCostoEntity>?> ListarCentroCostoActivo(int? Cen_Cos_Id, string? Cen_Cos_Des, string? Flg_Est);
    Task<ServiceResponse<int>> RegistrarCentroCosto(CentroCostoEntity valores);
    Task<ServiceResponse<int>> ActualizarCentroCosto(CentroCostoEntity valores);
}
