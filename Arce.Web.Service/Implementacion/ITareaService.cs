using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface ITareaService
{
    Task<ServiceResponseList<TareaEntity>?> ListarTarea(int? Id, string? Nombre, string? Estado);
    Task<ServiceResponseList<TareaEntity>?> ConsultarDatosTarea(int? Tarea_Id);
    Task<ServiceResponse<int>> RegistrarTarea(TareaEntity valores);
    Task<ServiceResponse<int>> ActualizarTarea(TareaEntity valores);
    Task<ServiceResponse<int>> EliminarTarea(int? Id, string? Usr_Mod);
}
