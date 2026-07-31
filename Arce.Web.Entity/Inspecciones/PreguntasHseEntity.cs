namespace Arce.Web.Entity.Inspecciones;

public class PreguntasHseEntity
{
    public int? Id { get; set; }
    public string? Pregunta_Nombre { get; set; }
    public string? Estado { get; set; }
    public string? Usr_Reg { get; set; }
    public string? Usr_Mod { get; set; }
    public DateTime? Fec_Reg { get; set; }
    public DateTime? Fec_Mod { get; set; }

    public int? Pregunta_Id
    {
        get => Id;
        set => Id = value;
    }

    public string? Nombre
    {
        get => Pregunta_Nombre;
        set => Pregunta_Nombre = value;
    }
}
