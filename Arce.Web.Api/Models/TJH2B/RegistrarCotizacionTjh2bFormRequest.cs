using Microsoft.AspNetCore.Http;

namespace Arce.Web.Api.Models.TJH2B;

public class RegistrarCotizacionTjh2bFormRequest
{
    public string? Cotizacion_Numero { get; set; }
    public int? Cliente_Id { get; set; }
    public string? Cotizacion_Servicio { get; set; }
    public DateTime? Cotizacion_FechaIni { get; set; }
    public DateTime? Cotizacion_FechaFin { get; set; }
    public string? Cotizacion_DocumentoPDF { get; set; }
    public IFormFile? Cotizacion_DocumentoPDF_File { get; set; }
    public string? Usr_Reg { get; set; }
}
