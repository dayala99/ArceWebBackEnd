namespace Arce.Web.Entity.CentroMonitoreoHse;

public class CentroMonitoreoHseEntity
{
    public int? Centro_Monitoreo_Id { get; set; }
    public string? Codigo_Centro_Monitoreo { get; set; }
    public string? Usr_Cod { get; set; }
    public string? Supervisor_Nom { get; set; }
    public int? Cliente_Id { get; set; }
    public string? Cliente_Nombre { get; set; }
    public string? Monitoreo_Documentos_Ubicacion { get; set; }
    public string? Monitoreo_Audio_Ubicacion { get; set; }
    public string? Estado { get; set; }
    public string? Usr_Reg { get; set; }
    public string? Usr_Mod { get; set; }
    public DateTime? Fec_Reg { get; set; }
    public DateTime? Fec_Mod { get; set; }

    public int? Id
    {
        get => Centro_Monitoreo_Id;
        set => Centro_Monitoreo_Id = value;
    }

    public string? Codigo
    {
        get => Codigo_Centro_Monitoreo;
        set => Codigo_Centro_Monitoreo = value;
    }
}

/// <summary>
/// Fila que devuelve [dbo].[SP_Filtrar_Centro_HSE] para la tabla de Centro de Monitoreo HSE.
/// </summary>
public class CentroHseListadoEntity
{
    public int? Centro_HSE_Id { get; set; }
    public string? Centro_HSE_Cod { get; set; }
    public string? Usr_Inspector { get; set; }
    public string? Usr_Supervisor { get; set; }
    public string? Cliente_Nombre { get; set; }
    public string? Centro_Revision { get; set; }
    public string? Centro_Puntaje { get; set; }
    public string? Centro_Comentario { get; set; }
}
