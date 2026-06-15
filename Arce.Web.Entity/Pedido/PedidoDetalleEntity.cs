namespace Arce.Web.Entity;

public class PedidoDetalleEntity
{
    public int? Ped_Det_Id { get; set; }
    public int? Ped_Cab_Id { get; set; }
    public int? Ped_Cod_Itm  { get; set; }
    public string? Itm_Des { get; set; }
    public int? Ped_Uni_Med { get; set; }
    public string? Uni_Med_Des { get; set; }
    public decimal? Ped_Can { get; set;}
    public decimal? Ped_Cos_Uni { get; set; }
    public decimal? Ped_Cos_Tot { get; set; }
    public int? Ped_Cen_Cos_Asg { get; set; }
    public string? Cen_Cos_Des { get; set; }
    public string? Usr_Reg { get; set; }
    public DateTime? Fec_Reg { get; set; }
    public string? Usr_Mod { get; set; }
    public DateTime? Fec_Mod { get; set; }
    public int? Ord_Com_Id { get; set; }
    public string? Ped_Obs { get; set; }
    public string? Itm_Cod { get; set; }
    public string? Uni_Med_Abr { get; set; }
    public int? Can_Ing {get; set; }
    public DateTime? Fec_Ing { get; set; }
    public string? Ped_Obs_Ped { get; set; }
}
