using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class AccesoService: IAccesoService
{
    private readonly IAccesoRepository _repository;

    public AccesoService(IAccesoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<AccesoEntity>?> ListarAcceso(string? @Prf_Acc_Cod)
    {
        var result = new ServiceResponseList<AccesoEntity>();
        try
        {
            var resultData = await _repository.ListarAcceso(Prf_Acc_Cod);
            
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
    public async Task<ServiceResponse<int>> RegistrarAcceso(AccesoEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.RegistrarAcceso(valores);

            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }
            
            result.Success = false;
            result.Message = resultData.Mensaje;
            result.Data = 0;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            result.Data = 0;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> EliminarAcceso(AccesoEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.EliminarAcceso(valores);

            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }
            
            result.Success = false;
            result.Message = resultData.Mensaje;
            result.Data = 0;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            result.Data = 0;
            return result;
        }
    }
}
