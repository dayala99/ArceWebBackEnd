using Microsoft.AspNetCore.Http;

namespace Arce.Web.Api.Models.TJH2B;

public class ActualizarCotizacionTjh2bFormRequest
{
    public int? Cotizacion_Id { get; set; }
    public string? Cotizacion_Numero { get; set; }
    public int? Cliente_Id { get; set; }
    public string? Cotizacion_Servicio { get; set; }
    public DateTime? Cotizacion_FechaIni { get; set; }
    public DateTime? Cotizacion_FechaFin { get; set; }
    public string? Cotizacion_DocumentoPDF { get; set; }
    public IFormFile? Cotizacion_DocumentoPDF_File { get; set; }
    public string? Usr_Mod { get; set; }
    public string? Cotizacion_Estado { get; set; }
    public string? Estado { get; set; }
}
