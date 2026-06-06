namespace Arce.Web.Entity;

public class OrdenCompraEntity
{
    public int? Ord_Com_Id { get; set; }
	public int? Ord_Com_Prv { get; set; }
	public int? Ord_Com_For_Pag { get; set; }
	public string? Ord_Com_Ref_Obr { get; set; }
	//public int? Ord_Com_Cen_Cos VARCHAR(55
	public string? Ord_Com_Obs { get; set; }
	public string? Ord_Com_Ref { get; set; }
	public decimal? Ord_Com_Sub_Tot { get; set; }
	public decimal? Ord_Com_Igv { get; set; }
	public decimal? Ord_Com_Tot { get; set; }
	public int? Ord_Com_Ped_Id { get; set; }
	//public int? Ord_Com_Det { get; set; }
	public string? Flg_Est { get; set; }
	public string? Usr_Reg { get; set; }
	public DateTime? Fec_Reg { get; set; }
	public string? Usr_Mod { get; set; }
	public DateTime? Fec_Mod { get; set; }
	public string? Prv_Nom { get; set; }
	public string? For_Pag_Des { get; set; }
	public string? Ord_Com_Arc_Adj_Nom { get; set; }
	public string? Ord_Com_Arc_Adj_Rut { get; set; }
	public int? Ord_Com_Det_Id { get; set; }
	public decimal? Ord_Com_Det_Mon { get; set; }
	public int? Ped_Cab_Id { get; set; }
	public int? Ped_Tip_Com { get; set; }
	public string? Tip_Ser_Des { get; set; }
}
