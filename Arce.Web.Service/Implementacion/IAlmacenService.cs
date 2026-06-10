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
}
