using Arce.Web.Entity.Usuario;
namespace Arce.Web.Data;

public interface IUsuarioRepository
{
    Task<IEnumerable<UsuarioEntity>?> ListarUsuarioActivo();
    Task<(int Codigo, string Mensaje)> RegistrarUsuario(UsuarioEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarUsuario(UsuarioEntity valores);
}
