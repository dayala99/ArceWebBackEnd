using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class DetraccionService: IDetraccionService
{
    private readonly IDetraccionRepository _repository;

    public DetraccionService(IDetraccionRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<DetraccionEntity>?> ListarDetraccion(int? Det_Id, string? Det_Des, string? Flg_Est)
    {
        var result = new ServiceResponseList<DetraccionEntity>();
        try
        {
            var resultData = await _repository.ListarDetraccion(Det_Id, Det_Des, Flg_Est);
            
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

    public async Task<ServiceResponse<int>> RegistrarDetraccion(DetraccionEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.RegistrarDetraccion(valores);

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

    public async Task<ServiceResponse<int>> ActualizarDetraccion(DetraccionEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.ActualizarDetraccion(valores);

            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }

            result.Success = false;
            result.Message = resultData.Mensaje;

            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }
}
