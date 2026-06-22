namespace Arce.Web.Entity;

public class PedidoCabeceraEntity
{
    public int? Ped_Id  { get; set; }
    public string? Prv_Nom { get; set; }
    public string? Ped_Usr_Apr  { get; set; }
    public int? Ped_Lug_Ent { get; set; }
    public string? Ped_Ref { get; set; }
    public int? Ped_Tip_Com { get; set; }
    public int? Ped_Tip_Mon { get; set; }
    public string? Ped_Tip_Mon_Des { get; set; }
    public DateTime? Ped_Fec_Ent { get; set; }
    public string? Ped_Sus { get; set; }
    public string? Ped_Arc_Adj_Nom { get; set; }
    public string? Ped_Arc_Adj_Rut { get; set; }
    public int? Ped_Prv_Cod { get; set; }
    public int? Ped_For_Pag_Cod { get; set; }
    public decimal? Ped_Tot { get; set; }
    public string? Flg_Est { get; set; }
    public string? Ped_Est_Des { get; set; }
    public string? Estado { get; set; }
    public string? Usr_Reg { get; set; }
    public DateTime? Fec_Reg { get; set; }
    public string? Usr_Mod { get; set; }
    public DateTime? Fec_Mod { get; set; }
    public decimal? Ped_Can_Tot { get; set; }
    public DateTime? Fec_Apr { get; set; }
    public string? Mon_Abr { get; set; }
    public string? Tip_Ser_Des { get; set; }
    public IEnumerable<PedidoDetalleEntityReporte>? Detalle_Reporte{ get; set; }
    public string? Ped_Mot_Rch { get; set; }
    public string? Usr_Nom { get; set; }
    public string? Ord_Com_Ped_Id { get; set; }
    public string? Ped_Ref_Gral { get; set; }
}

public class PedidoDetalleEntityReporte
{
    public int? Ped_Cab_Id { get; set; }
    public string? Itm_Des { get; set; }
	public string? Uni_Med_Abr { get; set; }
	public decimal? Ped_Can { get; set; }
	public decimal? Ped_Cos_Uni { get; set; }
	public decimal? Ped_Cos_Tot { get; set; }
}