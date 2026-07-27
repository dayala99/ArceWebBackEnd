using System.Collections.Generic;

namespace Arce.Web.Api.Models.Inspecciones;

public class PuntajeCentroHseDetalle
{
    public int Pregunta_Id { get; set; }

    // 'A' = Audio, 'D' = Documento
    public string Puntaje_Tipo { get; set; } = string.Empty;

    // 'S' = Pasó, 'N' = No pasó
    public string Puntaje_Rpta { get; set; } = string.Empty;
}

public class InsertarPuntajeCentroHseRequest
{
    public int Centro_HSE_Id { get; set; }
    public string Usr_Reg { get; set; } = string.Empty;
    public List<PuntajeCentroHseDetalle> Detalles { get; set; } = new();
}
