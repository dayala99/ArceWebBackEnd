using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IJefeRepository
{
    Task<IEnumerable<JefeEntity>?> ListarJefe(int? Id, string? Reporte_Tipo, string? Estado);
    Task<IEnumerable<JefeEntity>?> ConsultarDatosJefe(int? Reporte_Id);
    Task<(int Codigo, string Mensaje)> RegistrarJefe(JefeEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarJefe(JefeEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarJefe(int? Id, string? Usr_Mod);
}
