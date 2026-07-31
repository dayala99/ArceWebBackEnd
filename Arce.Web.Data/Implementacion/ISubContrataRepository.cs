using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface ISubContrataRepository
{
    Task<IEnumerable<SubContrataEntity>?> ListarSubContrata(int? Id, string? Nombre, string? Estado);
    Task<IEnumerable<SubContrataEntity>?> ConsultarDatosSubContrata(int? SubContrata_Id);
    Task<(int Codigo, string Mensaje)> RegistrarSubContrata(SubContrataEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarSubContrata(SubContrataEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarSubContrata(int? Id, string? Usr_Mod);
}
