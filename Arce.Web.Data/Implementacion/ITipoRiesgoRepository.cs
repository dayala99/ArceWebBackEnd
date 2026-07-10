using Arce.Web.Entity.Inspecciones;

namespace Arce.Web.Data;

public interface ITipoRiesgoRepository
{
    Task<IEnumerable<TipoRiesgoEntity>?> ListarTipoRiesgo(int? Id, string? Nombre, string? Estado);
    Task<IEnumerable<TipoRiesgoEntity>?> ConsultarDatosTipoRiesgo(int? Tipo_Id);
    Task<(int Codigo, string Mensaje)> RegistrarTipoRiesgo(TipoRiesgoEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarTipoRiesgo(TipoRiesgoEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarTipoRiesgo(int? Id, string? Usr_Mod);
}
