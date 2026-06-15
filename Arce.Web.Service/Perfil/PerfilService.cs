using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class PerfilService: IPerfilService
{
    private readonly IPerfilRepository _repository;

    public PerfilService(IPerfilRepository repository)
    {
        _repository = repository;
    }
    public async Task<ServiceResponseList<PerfilEntity>?> ListarPerfil(string? Prf_Cod, string? Prf_Des, string? Flg_Est)
    {
        var result = new ServiceResponseList<PerfilEntity>();
        try
        {
            var resultData = await _repository.ListarPerfil(Prf_Cod, Prf_Des, Flg_Est);

            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PerfilEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }
}
