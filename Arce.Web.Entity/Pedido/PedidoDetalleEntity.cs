namespace Arce.Web.Entity;

public class PedidoDetalleEntity
{
    public int? Ped_Det_Id { get; set; }
    public int? Ped_Cab_Id { get; set; }
    public string? Ped_Cod_Itm  { get; set; }
    public string? Ped_Uni_Med { get; set; }
    public decimal? Ped_Can { get; set;}
    public decimal? Ped_Cos_Uni { get; set; }
    public decimal? Ped_Cos_Tot { get; set; }
    public string? Usr_Reg { get; set; }
    public DateTime? Fec_Reg { get; set; }
    public string? Usr_Mod { get; set; }
    public DateTime? Fec_Mod { get; set; }
}
