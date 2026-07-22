using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Arce.Web.Api.Models.Inspecciones;

public class RegistrarCentroMonitoreoHseFormRequest
{
    public string? Usr_Cod { get; set; }
    public int? Cliente_Id { get; set; }
    public List<IFormFile>? Monitoreo_Documentos { get; set; }
    public List<IFormFile>? Monitoreo_Audio { get; set; }
    public string? Monitoreo_Documentos_Ubicacion { get; set; }
    public List<string>? Monitoreo_Audio_Ubicacion { get; set; }
    public string? Estado { get; set; }
    public string? Usr_Reg { get; set; }
}
