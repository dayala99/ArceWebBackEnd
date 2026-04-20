using Arce.Web.Service.Comunes;
using Arce.Web.Entity.Usuario;
namespace Arce.Web.Service;

public interface IUsuarioService
{
    Task<ServiceResponseList<UsuarioEntity>?> ListarUsuarioActivo();
    Task<ServiceResponse<int>> RegistrarUsuario(UsuarioEntity valores);
    Task<ServiceResponse<int>> ActualizarUsuario(UsuarioEntity valores);
}
