namespace Arce.Web.Entity.TJH2B;

public class CotizacionTjh2bEntity
{
    public int? Id { get; set; }
    public string? Numero { get; set; }
    public int? Cliente_Id { get; set; }
    public string? Cliente_Nombre { get; set; }
    public string? Servicio { get; set; }
    public DateTime? FechaIni { get; set; }
    public DateTime? FechaFin { get; set; }
    public string? DocumentoPdf { get; set; }
    public string? Cotizacion_Estado { get; set; }
    public string? Estado { get; set; }
    public string? Usr_Reg { get; set; }
    public DateTime? Fec_Reg { get; set; }
    public string? Usr_Mod { get; set; }
    public DateTime? Fec_Mod { get; set; }
}
