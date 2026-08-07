namespace Arce.Web.Entity;

public class AsignacionDetalleEntity
{
    public int? Asg_Det_Id { get; set; }
    public int? Asg_Id { get; set; }
    public int? Asg_Det_Itm_Id { get; set; }
    public decimal? Asg_Det_Can { get; set; }
    public string? Asg_Det_Ser { get; set; }
    public string? Asg_Det_Obs { get; set; }
    public string? Flg_Est { get; set; }
	public string? Itm_Des { get; set; }
	public string? Uni_Med_Abr { get; set; }
}
