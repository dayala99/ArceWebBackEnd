using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IClienteWbService
{
    Task<ServiceResponseList<ClienteEntityWb>?> ListarCliente(int? Cli_Id, string? Cli_Nom, string? Cli_Ruc, string? Flg_Est);
}
