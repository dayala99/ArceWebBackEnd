using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface ITipoReporteService
{
    Task<ServiceResponseList<TipoReporteEntity>?> ListarTipoReporte(int? Reporte_Id, string? Reporte_Tipo, string? Estado);
    Task<ServiceResponse<int>> RegistrarTipoReporte(TipoReporteEntity valores);
    Task<ServiceResponse<int>> ActualizarTipoReporte(TipoReporteEntity valores);
    Task<ServiceResponse<int>> EliminarTipoReporte(int? Tipo_Reporte_Id, string? Usr_Mod);
}
