using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IMotivoRepository
{
    Task<IEnumerable<MotivoEntity>?> ListarMotivo(int? Id, string? Nombre, string? Estado);
    Task<IEnumerable<MotivoEntity>?> ConsultarDatosMotivo(int? Motivo_Id);
    Task<(int Codigo, string Mensaje)> RegistrarMotivo(MotivoEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarMotivo(MotivoEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarMotivo(int? Id, string? Usr_Mod);
}
