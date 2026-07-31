using System.Collections.Generic;

namespace Arce.Web.Api.Models.Inspecciones;

public class PuntajeCentroHseActualizarDetalle
{
    public int Puntaje_Id { get; set; }

    // 'S' = Pasó, 'N' = No pasó
    public string Puntaje_Rpta { get; set; } = string.Empty;
}

public class ActualizarPuntajeCentroHseRequest
{
    public int Centro_HSE_Id { get; set; }
    public string Usr_Mod { get; set; } = string.Empty;
    public string Centro_Revision { get; set; } = "CERRADO";
    public string? Centro_Motivo { get; set; }
    public string? Centro_Comentario { get; set; }
    public List<PuntajeCentroHseActualizarDetalle> Detalles { get; set; } = new();
}
