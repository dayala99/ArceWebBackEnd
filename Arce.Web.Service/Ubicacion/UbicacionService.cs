using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class UbicacionService: IUbicacionService
{
    private readonly IUbicacionRepository _repository;

    public UbicacionService(IUbicacionRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<UbicacionEntity>?> ListarUbicacionActivo(int? Ubi_Id, string? Ubi_Des, string? Flg_Est)
    {
        var result = new ServiceResponseList<UbicacionEntity>();

        try
        {
            var resultData = await _repository.ListarUbicacionActivo(Ubi_Id, Ubi_Des, Flg_Est);
            var elements = (resultData ?? Enumerable.Empty<UbicacionEntity>()).ToList();

            result.Success = true;
            result.Message = elements.Any() ? "Completado con éxito" : "No existe información";
            result.Elements = elements;
            result.TotalElements = elements.Count;

            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }
}
