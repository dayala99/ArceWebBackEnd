using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IAlmacenRepository
{
    Task<IEnumerable<AlmacenEntity>?> ListarIngresoAlmacen(int? Alm_Mov_Id, string? Alm_Tip_Ing, string? Flg_Est, string? Flg_Est_Apr);
    Task<IEnumerable<AlmacenEntity>?> ListarIngresoAlmacenModificar(int? Alm_Mov_Id);
    Task<(int Codigo, string Mensaje, int Alm_Mov_Id)> RegistrarIngresoAlmacen(AlmacenEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarIngresoAlmacen(AlmacenEntity valores);
    Task<IEnumerable<AlmacenDetalleEntity>?> ListarIngresoAlmacenDetalleModificar(int? Alm_Mov_Id);
    Task<(int Codigo, string Mensaje)> RegistrarIngresoAlmacenDetalle(AlmacenDetalleEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarIngresoAlmacenDetalle(AlmacenDetalleEntity valores);
    Task<(int Codigo, string Mensaje, int Alm_Mov_Id)> RegistrarIngresoAlmacenOrdenCompra(AlmacenEntity valores);
    Task<(int Codigo, string Mensaje, int Alm_Mov_Id)> RegistrarTransferenciaAlmacen(AlmacenEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarIngresoAlmacenDetalleOrdenCompra(AlmacenDetalleEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarMotivoRechazoAlmacen(AlmacenEntity valores);
    Task<IEnumerable<AlmacenDetalleEntity>?> ListarIngresoSalidaAlmacenPorCentroCosto(int? Alm_Det_Itm_Id);
    Task<IEnumerable<AlmacenDetalleEntity>?> ReporteListarSalidas(DateTime? Fec_Ini, DateTime? Fec_Fin ,int? Alm_Det_Itm_Id);
    Task<IEnumerable<AlmacenEntity>?> ReporteIngresoSalidasAlmacen(string? Usr_Cod, DateTime? Fec_Ini, DateTime? Fec_Fin, int? Alm_Det_Cen_Cos_Id, int? Alm_Det_Prv_Id, int? Alm_Det_Itm_Id, int? Alm_Tip_Ing);
    Task<IEnumerable<AlmacenEntity>?> ListarTransferenciaAlmacen(
        DateTime? Fec_Ini, DateTime? Fec_Fin , string? Alm_Sol_Dni, int? Alm_Cen_Cos, int? Alm_Destino,
        int? Alm_Tip_Ing, string? Alm_Usr_Apr, int? Alm_Mov_Ori
        );
}
