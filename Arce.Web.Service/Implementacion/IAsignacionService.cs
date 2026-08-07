using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IAsignacionService
{
    Task<ServiceResponseList<AsignacionCabeceraEntity>?> ListarAsignacion(int? Asg_Id, DateTime? Fec_Ini, DateTime? Fec_Fin,
    string? Asg_Usr, string? Usr_Reg, string? Flg_Est, int? Asg_Usr_Cen_Cos);
    Task<ServiceResponse<int>> RegistrarAsignacion(AsignacionCabeceraEntity valores);
    Task<ServiceResponse<int>> ActualizarAsignacion(AsignacionCabeceraEntity valores);
    Task<ServiceResponse<int>> RegistrarAsignacionDetalle(AsignacionDetalleEntity valores);
    Task<ServiceResponse<int>> ActualizarAsignacionDetalle(AsignacionDetalleEntity valores);
    Task<ServiceResponseList<AsignacionDetalleEntity>?> ListarDetallesXAsignacion(int? Asg_Id);
    Task<ServiceResponseList<AsignacionDetalleEntity>?> ListarAsignacionDetalleModificar(int? Asg_Det_Id);
    Task<ServiceResponseList<AsignacionCabeceraEntity>?> ListarAsignacionModificar(int? Asg_Id);
    Task<ServiceResponseList<AsignacionCabeceraEntity>?> ObtenerStockReservadoAsignacion(int? Asg_Usr_Cen_Cos, int? Asg_Det_Itm_Id);
    Task<ServiceResponse<int>> EliminarAsignacionDetalle(AsignacionDetalleEntity valores);
    Task<ServiceResponse<int>> EliminarAsignacion(AsignacionCabeceraEntity valores);
    Task<ServiceResponse<int>> EliminarAsignacionDetalleTotal(AsignacionDetalleEntity valores);
    Task<ServiceResponseList<ReporteAsignacionEntity>?> ReporteAsignacionUsuario(string? Flg_Est, 
    string? Asg_Usr, string? Usr_Reg, int? Asg_Usr_Cen_Cos, int? Asg_Id, int? Asg_Det_Itm_Id,
    DateTime? Fec_Ini, DateTime? Fec_Fin);
    Task<ServiceResponseList<AsignacionCabeceraEntity>?> ObtenerDatosCabeceraValeSalidaPDF (int? Asg_Id);
    Task<ServiceResponseList<AsignacionDetalleEntity>?> ObtenerDatosDetalleValeSalidaPDF (int? Asg_Id);
}
