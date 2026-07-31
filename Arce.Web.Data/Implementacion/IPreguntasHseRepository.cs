using Arce.Web.Entity.Inspecciones;

namespace Arce.Web.Data;

public interface IPreguntasHseRepository
{
    Task<IEnumerable<PreguntasHseEntity>?> ListarPreguntasHse(int? Pregunta_Id, string? Pregunta_Nombre, string? Estado);
    Task<IEnumerable<PreguntasHseEntity>?> ConsultarDatosPreguntasHse(int? Pregunta_Id);
    Task<IEnumerable<PreguntasHseEntity>?> ListarPreguntasHseSinEstado();
    Task<(int Codigo, string Mensaje)> RegistrarPreguntasHse(PreguntasHseEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarPreguntasHse(PreguntasHseEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarPreguntasHse(int? Pregunta_Id, string? Usr_Mod);
}
