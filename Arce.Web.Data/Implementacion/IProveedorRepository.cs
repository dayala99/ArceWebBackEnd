using Arce.Web.Entity.Proveedor;
namespace Arce.Web.Data;

public interface IProveedorRepository
{
    Task<IEnumerable<ProveedorEntity>?> ListarProveedorActivo(string Prv_Id, string Prv_Nom, string Prv_Ruc, string Prv_Nom_Con, string Flg_Est);
    Task<(int Codigo, string Mensaje)> RegistrarProveedor(ProveedorEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarProveedor(ProveedorEntity valores);
}
