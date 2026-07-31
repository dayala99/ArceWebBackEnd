using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IClimaRepository
{
    Task<IEnumerable<ClimaEntity>?> ListarClima(int? Id, string? Nombre, string? Estado);
    Task<IEnumerable<ClimaEntity>?> ConsultarDatosClima(int? Clima_Id);
    Task<(int Codigo, string Mensaje)> RegistrarClima(ClimaEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarClima(ClimaEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarClima(int? Id, string? Usr_Mod);
}
