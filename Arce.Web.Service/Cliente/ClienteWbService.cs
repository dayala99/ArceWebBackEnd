using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class ClienteWbService: IClienteWbService
{
    private readonly IClienteWbRepository _repository;

    public ClienteWbService(IClienteWbRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<ClienteEntityWb>?> ListarCliente(int? Cli_Id, string? Cli_Nom, string? Cli_Ruc, string? Flg_Est)
    {
        var result = new ServiceResponseList<ClienteEntityWb>();

        try
        {
            var resultData = await _repository.ListarCliente(Cli_Id, Cli_Nom, Cli_Ruc, Flg_Est);

            if (resultData == null || !resultData.Any())
            {
                result.Success = false;
                result.Message = "No existe información";
                return result;
            }

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = resultData.ToList();
            result.TotalElements = resultData.ToList().Count();

            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepcion no controlada " + ex.Message;
            return result;
        }
    }
}
