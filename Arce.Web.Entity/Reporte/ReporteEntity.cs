namespace Arce.Web.Entity;

public class ReporteEntity
{
    public int? Itm_Id	 {get; set; }
    public int? Alm_Mov_Id	 {get; set; }
    public string? Alm_Sol_Dni	 {get; set; }
    public string? Usr_Nom	 {get; set; }
    public string? Itm_Cod	 {get; set; }
    public string? Itm_Des	 {get; set; }
    public string? Cen_Cos_Des	 {get; set; }
    public string? Ing_Des	 {get; set; }
    public string? Flg_Est_Apr	 {get; set; }
    public decimal? Alm_Det_Can {get; set; }
}

public class ReporteSalidaEntity
{
    public DateTime? Fec_Reg { get; set; }
    public string? Usr_Nom  { get; set; }
    public string? Usr_Doc_Nro  { get; set; }
    public string? Itm_Des { get; set; }
    public string? Uni_Med_Abr  { get; set; }
    public string? Alm_Det_Can { get; set; }
}

public class ReporteAsignacionEntity
{
    public int? Asg_Id { get; set; }
	public DateTime? Asg_Fec { get; set; }
	public string? Asg_Usr { get; set; }
	public string? Usr_Asignacion { get; set; }
	public int? Asg_Usr_Cen_Cos { get; set; }
	public string? Cen_Cos_Des { get; set; }
	public string? Usr_Reg { get; set; }
	public string? Usr_Registro { get; set; }
	public int? Asg_Det_Itm_Id { get; set; }
	public string? Itm_Cod { get; set; }
	public string? Itm_Des { get; set; }
	public string? Asg_Det_Can { get; set; }
}