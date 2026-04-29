namespace Arce.Web.Entity;

public class PedidoCabeceraEntity
{
    public int? Ped_Id  { get; set; }
    public string? Ped_Usr_Apr  { get; set; }
    public string? Ped_Lug_Ent { get; set; }
    public string? Ped_Ref { get; set; }
    public string? Ped_Tip_Com { get; set; }
    public int? Ped_Tip_Mon { get; set; }
    public DateTime? Ped_Fec_Ent { get; set; }
    public string? Ped_Sus { get; set; }
    public string? Ped_Arc_Adj_Nom { get; set; }
    public string? Ped_Arc_Adj_Rut { get; set; }
    public int? Ped_Prv_Cod { get; set; }
    public int? Ped_For_Pag_Cod { get; set; }
    public decimal? Ped_Tot { get; set; }
    public string? Flg_Est { get; set; }
    public string? Usr_Reg { get; set; }
    public DateTime? Fec_Reg { get; set; }
    public string? Usr_Mod { get; set; }
    public DateTime? Fec_Mod { get; set; }
}
