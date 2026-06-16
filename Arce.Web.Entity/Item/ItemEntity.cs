namespace Arce.Web.Entity;

public class ItemEntity
{
    public int? Itm_Id { get; set; }
    public string? Itm_Des { get; set; }
    public int? Itm_Grp { get; set; }
    public string? Usr_Reg { get; set; }
    public DateTime? Fec_Reg { get; set; }
    public string? Usr_Mod { get; set; }
    public DateTime? Fec_Mod { get; set; }
    public string? Flg_Est { get; set; }

    //COGER NOMBRE DE GRUPO DESDE LA TABLA GRUPOITEM
    public string? Grp_Des { get; set; }
    public int? Itm_Sub_Grp { get; set; }
    public string? Sub_Grp_Des { get; set; }
    public int? Itm_Det_Mat_Id { get; set; }
    public string? Det_Mat_Des { get; set; }
    public string? Itm_Cod { get; set; }
    public int? Uni_Med_Id { get; set; }
    public string? Uni_Med_Des { get; set; }
}
