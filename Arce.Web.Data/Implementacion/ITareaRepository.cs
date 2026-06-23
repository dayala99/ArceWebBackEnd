using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface ITareaRepository
{
    Task<IEnumerable<TareaEntity>?> ListarTarea(int? Id, string? Nombre, string? Estado);
    Task<IEnumerable<TareaEntity>?> ConsultarDatosTarea(int? Tarea_Id);
    Task<(int Codigo, string Mensaje)> RegistrarTarea(TareaEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarTarea(TareaEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarTarea(int? Id, string? Usr_Mod);
}
