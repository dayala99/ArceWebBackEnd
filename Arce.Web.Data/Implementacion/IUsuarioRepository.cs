using Arce.Web.Entity.Usuario;
namespace Arce.Web.Data;

public interface IUsuarioRepository
{
    Task<IEnumerable<UsuarioEntity>?> ListarUsuarioActivo(int? Usr_Id, string? Usr_Cod, string? Usr_Nom, string? Flg_Est);
    Task<(int Codigo, string Mensaje)> RegistrarUsuario(UsuarioEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarUsuario(UsuarioEntity valores);
}
