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
    public DateTime? Alm_Det_Fec { get; set; }
    public string? Alm_Ser { get; set; }
    public string? Alm_Gui_Rem { get; set; }
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

public class ReporteItemOcos
{
    public string? EstadoPedido { get; set; }	
    public string? CodigoPedido { get; set; }	
    public string? IdTipoPedido { get; set; }	
    public string? TipoPedido { get; set; }
    public string? DescripcionItem { get; set; }
    public string? CentroCostos { get; set; }
    public decimal? CostoTotalPedidoItem { get; set; }
    public string? IdUbicacion { get; set; }	
    public string? Ubicacion { get; set; }	
    public string? CodSolicitante { get; set; }	
    public string? NomSolicitante { get; set; }	
    public string? FechaPedido { get; set; }	
    public string? EstadoAtencion { get; set; }	
    public string? CodOrden { get; set; }	
    public string? Descripcion { get; set; }	
    public string? Proveedor { get; set; }	
    public DateTime? FechaOrden { get; set; }	
    public string? FormaPago { get; set; }	
    public string? Moneda { get; set; }	
    public string? EstadoPago { get; set; }	
    public decimal? BaseSoles { get; set; }	
    public decimal? IgvSoles { get; set; }	
    public decimal? TotalSoles { get; set; }	
    public decimal? BaseDolares { get; set; }	
    public decimal? IgvDolares { get; set; }	
    public decimal? TotalDolares { get; set; }	
    public string? AbonoProveedor { get; set; }	
    public decimal? Detraccion { get; set; }	
    public string? Pendiente { get; set; }	
    public string? PeriodoGasto { get; set; }	
    public DateTime? FechaPago { get; set; }
    public string? NroComprobantePago { get; set; }	
    public DateTime? FechaEmisionFactura { get; set; }	
    public DateTime? FechaVencimientoFactura { get; set; }
}