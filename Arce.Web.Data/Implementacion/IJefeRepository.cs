using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IJefeRepository
{
    Task<IEnumerable<JefeEntity>?> ListarJefe(int? Id, string? Nombre, string? Dni, string? Estado);
    Task<IEnumerable<JefeEntity>?> ConsultarDatosJefe(int? Jefe_Id);
    Task<(int Codigo, string Mensaje)> RegistrarJefe(JefeEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarJefe(JefeEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarJefe(int? Id, string? Usr_Mod);
}
