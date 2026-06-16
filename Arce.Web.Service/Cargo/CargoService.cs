using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class CargoService: ICargoService
{
    private readonly ICargoRepository _repository;

    public CargoService(ICargoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<CargoEntity>?> ListarCargo(int? Cargo_Id, string? Cargo_Nombre)
    {
        var result = new ServiceResponseList<CargoEntity>();
        try
        {
            var resultData = await _repository.ListarCargo(Cargo_Id, Cargo_Nombre);
            
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
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
