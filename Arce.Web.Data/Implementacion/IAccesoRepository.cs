using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IAccesoRepository
{
    Task<IEnumerable<AccesoEntity>?> ListarAcceso(string? @Prf_Acc_Cod);
    Task<(int Codigo, string Mensaje)> RegistrarAcceso(AccesoEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarAcceso(AccesoEntity valores);
}
