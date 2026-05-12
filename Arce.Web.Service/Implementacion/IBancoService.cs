using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IBancoService
{
    Task<ServiceResponseList<BancoEntity>?> ListarBanco(int? Ban_Id, string? Ban_Des, string? Flg_Est);
    Task<ServiceResponse<int>> RegistrarBanco(BancoEntity valores);
    Task<ServiceResponse<int>> ActualizarBanco(BancoEntity valores);
}
