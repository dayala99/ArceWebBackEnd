using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IAsignacionRepository
{
    Task<IEnumerable<AsignacionCabeceraEntity>?> ListarAsignacion();
    Task<(int Codigo, string Mensaje, int AsignacionId)> RegistrarAsignacion(AsignacionCabeceraEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarAsignacion(AsignacionCabeceraEntity valores);
    Task<(int Codigo, string Mensaje)> RegistrarAsignacionDetalle(AsignacionDetalleEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarAsignacionDetalle(AsignacionDetalleEntity valores);
    Task<IEnumerable<AsignacionDetalleEntity>?> ListarDetallesXAsignacion(int? Asg_Id);
    Task<IEnumerable<AsignacionDetalleEntity>?> ListarAsignacionDetalleModificar(int? Asg_Det_Id);
    Task<IEnumerable<AsignacionCabeceraEntity>?> ListarAsignacionModificar(int? Asg_Id);
    Task<IEnumerable<AsignacionCabeceraEntity>?> ObtenerStockReservadoAsignacion(int? Asg_Usr_Cen_Cos, int? Asg_Det_Itm_Id);
    Task<(int Codigo, string Mensaje)> EliminarAsignacionDetalle(AsignacionDetalleEntity valores);
}
