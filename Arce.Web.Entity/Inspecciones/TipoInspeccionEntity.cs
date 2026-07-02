namespace Arce.Web.Entity.Inspecciones;

public class TipoInspeccionEntity
{
    public int? Id { get; set; }
    public string? Nombre { get; set; }
    public string? Estado { get; set; }
    public string? Usr_Reg { get; set; }
    public string? Usr_Mod { get; set; }
    public DateTime? Fec_Reg { get; set; }
    public DateTime? Fec_Mod { get; set; }

    // Compatibilidad con versiones anteriores del frontend/backend
    public int? Tipo_Id
    {
        get => Id;
        set => Id = value;
    }

    public string? Tipo_Nombre
    {
        get => Nombre;
        set => Nombre = value;
    }
}
