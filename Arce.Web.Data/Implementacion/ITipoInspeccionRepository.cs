using Arce.Web.Entity.Inspecciones;

namespace Arce.Web.Data;

public interface ITipoInspeccionRepository
{
    Task<IEnumerable<TipoInspeccionEntity>?> ListarTipoInspeccion(int? Id, string? Nombre, string? Estado);
    Task<IEnumerable<TipoInspeccionEntity>?> ConsultarDatosTipoInspeccion(int? Tipo_Id);
    Task<(int Codigo, string Mensaje)> RegistrarTipoInspeccion(TipoInspeccionEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarTipoInspeccion(TipoInspeccionEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarTipoInspeccion(int? Id, string? Usr_Mod);
}
