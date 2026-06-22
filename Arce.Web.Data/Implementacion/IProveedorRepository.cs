using Arce.Web.Entity.Proveedor;
namespace Arce.Web.Data;

public interface IProveedorRepository
{
    Task<IEnumerable<ProveedorEntity>?> ListarProveedorActivo(int? Prv_Id, string? Prv_Nom, string? Prv_Ruc, string? Prv_Nom_Con, string? Flg_Est);
    Task<(int Codigo, string Mensaje)> RegistrarProveedor(ProveedorEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarProveedor(ProveedorEntity valores);
    Task<IEnumerable<ProveedorBancoEntity>?> ListarProveedorBanco(int? Prv_Ban_Id, int? Prv_Id);
    Task<(int Codigo, string Mensaje)> RegistrarProveedorBanco(ProveedorBancoEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarProveedorBanco(ProveedorBancoEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarProveedorBanco(ProveedorBancoEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarCuentaBancariaProveedor(ProveedorBancoEntity valores);
}
