using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface ITipoReporteRepository
{
    Task<IEnumerable<TipoReporteEntity>?> ListarTipoReporte(int? Reporte_Id, string? Reporte_Tipo, string? Estado);
    Task<(int Codigo, string Mensaje)> RegistrarTipoReporte(TipoReporteEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarTipoReporte(TipoReporteEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarTipoReporte(int? Tipo_Reporte_Id, string? Usr_Mod);
}
