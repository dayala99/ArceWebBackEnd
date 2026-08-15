using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IClienteWbRepository
{
    Task<IEnumerable<ClienteEntityWb>?> ListarCliente(int? Cli_Id, string? Cli_Nom, string? Cli_Ruc, string? Flg_Est);
}
