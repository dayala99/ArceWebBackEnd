using Arce.Web.Service.Comunes;
using Arce.Web.Entity.Proveedor;
namespace Arce.Web.Service;

public interface IProveedorService
{
    Task<ServiceResponseList<ProveedorEntity>?> ListarProveedorActivo(int? Prv_Id, string? Prv_Nom, string? Prv_Ruc, string? Prv_Nom_Con, string? Flg_Est);
    Task<ServiceResponse<int>> RegistrarProveedor(ProveedorEntity valores);
    Task<ServiceResponse<int>> ActualizarProveedor(ProveedorEntity valores);
    Task<ServiceResponseList<ProveedorBancoEntity>?> ListarProveedorBanco(int? Prv_Ban_Id, int? Prv_Id);
    Task<ServiceResponse<int>> RegistrarProveedorBanco(ProveedorBancoEntity valores);
    Task<ServiceResponse<int>> ActualizarProveedorBanco(ProveedorBancoEntity valores);
    Task<ServiceResponse<int>> EliminarProveedorBanco(ProveedorBancoEntity valores);
    Task<ServiceResponse<int>> ActualizarCuentaBancariaProveedor(ProveedorBancoEntity valores);
}
