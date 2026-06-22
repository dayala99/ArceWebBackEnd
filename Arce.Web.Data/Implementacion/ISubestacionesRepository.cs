using Arce.Web.Entity.Inspecciones;

namespace Arce.Web.Data;

public interface ISubestacionesRepository
{
    Task<IEnumerable<SubEstacionEntity>?> ListarSubEstaciones(int? Id, string? Nombre, int? Cliente_Id, string? Estado);
    Task<IEnumerable<SubEstacionEntity>?> ConsultarEditarSubEstaciones(int? Subestacion_Id);
    Task<(int Codigo, string Mensaje)> RegistrarSubEstacion(SubEstacionEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarSubEstacion(SubEstacionEntity valores);
}
