using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Arce.Web.Api.Models.CentroMonitoreoHse;

public class ActualizarCentroMonitoreoHseFormRequest
{
    public int Centro_Monitoreo_Id { get; set; }
    public string? Usr_Cod { get; set; }
    public int? Cliente_Id { get; set; }
    public List<IFormFile>? Monitoreo_Documentos { get; set; }
    public List<string>? Monitoreo_Documentos_Ubicacion { get; set; }
    public List<IFormFile>? Monitoreo_Audio { get; set; }
    public List<string>? Monitoreo_Audio_Ubicacion { get; set; }
    public string? Estado { get; set; }
    public string? Usr_Mod { get; set; }
}
