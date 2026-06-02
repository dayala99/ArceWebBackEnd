namespace Arce.Web.Entity;

public class AlmacenDetalleEntity
{
    public int? Alm_Det_Id { get; set; }
	public int? Alm_Mov_Id { get; set; }
	public int? Alm_Det_Itm_Id { get; set; }
	public int? Alm_Det_Uni_Med_Id { get; set; }
	public decimal? Alm_Det_Can { get; set; }
	public string? Alm_Det_Doc_Nro { get; set; }
	public DateTime? Alm_Det_Fec { get; set; }
	public int? Alm_Det_Cen_Cos_Id { get; set; }
	public string? Usr_Reg { get; set; }
	public DateTime? Fec_Reg { get; set; }
	public string? Usr_Mod { get; set; }
    public DateTime? Fec_Mod { get; set; }
    public int? Alm_Det_Prv_Id { get; set; }
}
