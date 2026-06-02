namespace Arce.Web.Entity;

public class AlmacenEntity
{
    public int? Alm_Mov_Id  {get; set; }
	public string? Alm_Sol_Dni  {get; set; }
	public int? Alm_Cen_Cos  {get; set; }
	public int? Alm_Ubi  {get; set; }
	public string? Flg_Est  {get; set; }
	public string? Flg_Est_Apr {get; set; }
	public string? Usr_Reg {get; set; }
	public DateTime? Fec_Reg {get; set; }
	public string? Usr_Mod {get; set; }
	public DateTime? Fec_Mod {get; set; }
    public int? Alm_Tip_Ing { get; set; }
    public string? Ubi_Des { get; set; }
    public string? Ing_Des { get; set; } 
    public string? Cen_Cos_Des { get; set; }
    public string? Usr_Nom { get; set; }
}
