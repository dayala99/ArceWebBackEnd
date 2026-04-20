using Arce.Web.Service.Comunes;
using Arce.Web.Entity.Proveedor;
namespace Arce.Web.Service;

public interface IProveedorService
{
    Task<ServiceResponseList<ProveedorEntity>?> ListarProveedorActivo(string Prv_Id, string Prv_Nom, string Prv_Ruc, string Prv_Nom_Con, string Flg_Est);
    Task<ServiceResponse<int>> RegistrarProveedor(ProveedorEntity valores);
    Task<ServiceResponse<int>> ActualizarProveedor(ProveedorEntity valores);
}
