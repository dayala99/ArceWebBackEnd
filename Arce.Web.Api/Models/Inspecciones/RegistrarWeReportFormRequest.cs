using Microsoft.AspNetCore.Http;

namespace Arce.Web.Api.Models.Inspecciones;

public class RegistrarWeReportFormRequest
{
    public string? Usr_Cod { get; set; }
    public string? Report_Anonimo { get; set; }
    public int? Reporte_Id { get; set; }
    public int? Cen_Cos_Id { get; set; }
    public int? Cliente_Id { get; set; }
    public int? Subestacion_Id { get; set; }
    public string? Report_Descripcion { get; set; }
    public IFormFile? Report_Foto1 { get; set; }
    public string? Report_Acciones_Inmediata { get; set; }
    public IFormFile? Report_Foto2 { get; set; }
    public string? Report_Acciones_Propuestas { get; set; }
    public string? Report_Potencial { get; set; }
    public string? Report_Aplica { get; set; }
    public string? Usr_Reg { get; set; }
}
