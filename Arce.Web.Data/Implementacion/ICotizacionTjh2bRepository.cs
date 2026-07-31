using Arce.Web.Entity.TJH2B;

namespace Arce.Web.Data;

public interface ICotizacionTjh2bRepository
{
    Task<IEnumerable<CotizacionTjh2bEntity>?> ListarCotizacionTjh2b(string? Numero, string? ClienteNombre, string? Servicio, string? Estado, string? CotizacionEstado, DateTime? FechaInicio, DateTime? FechaFin);
    Task<IEnumerable<CotizacionTjh2bEntity>?> ConsultarDatosCotizacionTjh2b(int? Cotizacion_Id);
    Task<(int Codigo, string Mensaje)> RegistrarCotizacionTjh2b(CotizacionTjh2bEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarCotizacionTjh2b(CotizacionTjh2bEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarCotizacionTjh2b(int? Cotizacion_Id, string? Usr_Mod);
}
