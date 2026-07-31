using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IAsignacionService
{
    Task<ServiceResponseList<AsignacionCabeceraEntity>?> ListarAsignacion();
    Task<ServiceResponse<int>> RegistrarAsignacion(AsignacionCabeceraEntity valores);
    Task<ServiceResponse<int>> ActualizarAsignacion(AsignacionCabeceraEntity valores);
    Task<ServiceResponse<int>> RegistrarAsignacionDetalle(AsignacionDetalleEntity valores);
    Task<ServiceResponse<int>> ActualizarAsignacionDetalle(AsignacionDetalleEntity valores);
    Task<ServiceResponseList<AsignacionDetalleEntity>?> ListarDetallesXAsignacion(int? Asg_Id);
    Task<ServiceResponseList<AsignacionDetalleEntity>?> ListarAsignacionDetalleModificar(int? Asg_Det_Id);
    Task<ServiceResponseList<AsignacionCabeceraEntity>?> ListarAsignacionModificar(int? Asg_Id);
    Task<ServiceResponseList<AsignacionCabeceraEntity>?> ObtenerStockReservadoAsignacion(int? Asg_Usr_Cen_Cos, int? Asg_Det_Itm_Id);
    Task<ServiceResponse<int>> EliminarAsignacionDetalle(AsignacionDetalleEntity valores);
}
