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
}
