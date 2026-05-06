using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IUnidadMedidaService
{
    Task<ServiceResponseList<UnidadMedidaEntity>?> ListarUnidadMedida(int? Uni_Med_Id, string? Uni_Med_Des, string? Flg_Est);
    Task<ServiceResponse<int>> RegistrarUnidadMedida(UnidadMedidaEntity valores);
    Task<ServiceResponse<int>> ActualizarUnidadMedida(UnidadMedidaEntity valores);
}
