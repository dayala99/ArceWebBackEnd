using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IAlmacenService
{
    Task<ServiceResponseList<AlmacenEntity>?> ListarIngresoAlmacen(int? Alm_Mov_Id, string? Alm_Tip_Ing, string? Flg_Est, string? Flg_Est_Apr);
    Task<ServiceResponseList<AlmacenEntity>?> ListarIngresoAlmacenModificar(int? Alm_Mov_Id);
    Task<ServiceResponse<int>> RegistrarIngresoAlmacen(AlmacenEntity valores);
    Task<ServiceResponse<int>> ActualizarIngresoAlmacen(AlmacenEntity valores);
    Task<ServiceResponseList<AlmacenDetalleEntity>?> ListarIngresoAlmacenDetalleModificar(int? Alm_Mov_Id);
    Task<ServiceResponse<int>> RegistrarIngresoAlmacenDetalle(AlmacenDetalleEntity valores);
    Task<ServiceResponse<int>> ActualizarIngresoAlmacenDetalle(AlmacenDetalleEntity valores);
    Task<ServiceResponse<int>> RegistrarIngresoAlmacenOrdenCompra(AlmacenEntity valores);
    Task<ServiceResponse<int>> RegistrarTransferenciaAlmacen(AlmacenEntity valores);
    Task<ServiceResponse<int>> ActualizarIngresoAlmacenDetalleOrdenCompra(AlmacenDetalleEntity valores);
    Task<ServiceResponse<int>> ActualizarMotivoRechazoAlmacen(AlmacenEntity valores);
    Task<ServiceResponseList<AlmacenDetalleEntity>?> ListarIngresoSalidaAlmacenPorCentroCosto(int? Alm_Det_Itm_Id);
    Task<ServiceResponseList<AlmacenDetalleEntity>?> ReporteListarSalidas(DateTime? Fec_Ini, DateTime? Fec_Fin ,int? Alm_Det_Itm_Id);
    Task<ServiceResponseList<AlmacenEntity>?> ReporteIngresoSalidasAlmacen(string? Usr_Cod, DateTime? Fec_Ini, DateTime? Fec_Fin, int? Alm_Det_Cen_Cos_Id, int? Alm_Det_Prv_Id, int? Alm_Det_Itm_Id, int? Alm_Tip_Ing);
    Task<ServiceResponseList<AlmacenEntity>?> ListarTransferenciaAlmacen(
        DateTime? Fec_Ini, DateTime? Fec_Fin , string? Alm_Sol_Dni, int? Alm_Cen_Cos, int? Alm_Destino,
        int? Alm_Tip_Ing, string? Alm_Usr_Apr, int? Alm_Mov_Ori
        );
}
