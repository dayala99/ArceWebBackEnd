using Arce.Web.Entity.Usuario;
namespace Arce.Web.Data;

public interface IUsuarioRepository
{
    Task<IEnumerable<UsuarioEntity>?> ListarUsuarioActivo(int? Usr_Id, string? Usr_Cod, string? Usr_Nom, string? Flg_Est);
    Task<(int Codigo, string Mensaje)> RegistrarUsuario(UsuarioEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarUsuario(UsuarioEntity valores);
    Task<IEnumerable<UsuarioEntity>?> ObtenerAccesoUsuario(string? Usr_Cod, string? Usr_Pass);
    Task<IEnumerable<UsuarioEntity>?> ObtenerUsuariosAprobacion(string? Usr_Apr);
    Task<IEnumerable<UsuarioEntity>?> ConsultarDatosUsuario(string? Usr_Cod);
}
