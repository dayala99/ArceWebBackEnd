namespace Arce.Web.Entity.Proveedor;

public class ProveedorEntity
{
    public int? Prv_Id { get; set; }
    public string? Prv_Nom { get; set; }
    public string? Prv_Ruc { get; set; }
    public string? Prv_Tel { get; set; }
    public string? Prv_Dir { get; set; }
    public string? Prv_Nom_Con { get; set; }
    public string? Flg_Est { get; set; }
    public string? Usr_Reg { get; set; }
    public DateTime? Fec_Reg { get; set; }
    public string? Usr_Mod { get; set; }
    public DateTime? Fec_Mod { get; set; }
    public string? Prv_Email { get; set; }
    public string? Prv_Nro_Cue_Ban { get; set; }
    public string? Prv_Nro_Cue_Ban_CCI { get; set; }
    public int? Prv_Ban { get; set; }
    public string? Ban_Des { get; set; }
}

public class ProveedorBancoEntity
{
    public int? Prv_Ban_Id { get; set; }
    public int? Prv_Id { get; set; }
    public int? Ban_Id { get; set; }
    public int? Tip_Mon { get; set; }
    public string? Prv_Ban_Nro_Cta { get; set; }
    public string? Prv_Ban_Nro_Cta_CCI { get; set; }
    public string? Usr_Reg { get; set; }
    public DateTime? Fec_Reg { get; set; }
    public string? Usr_Mod { get; set; }
    public DateTime? Fec_Mod { get; set; }
    public string? Ban_Abr { get; set; }
    public string? Mon_Des { get; set; }
}
