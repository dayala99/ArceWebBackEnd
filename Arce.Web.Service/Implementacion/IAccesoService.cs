using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IAccesoService
{
    Task<ServiceResponseList<AccesoEntity>?> ListarAcceso(string? @Prf_Acc_Cod);
    Task<ServiceResponse<int>> RegistrarAcceso(AccesoEntity valores);
    Task<ServiceResponse<int>> EliminarAcceso(AccesoEntity valores);
}
