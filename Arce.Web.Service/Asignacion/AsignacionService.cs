using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class AsignacionService: IAsignacionService
{
    private readonly IAsignacionRepository _repository;

    public AsignacionService(IAsignacionRepository repository)
    {
        _repository = repository;
    }

    // public async Task<ServiceResponseList<BancoEntity>?> ListarBanco(int? Ban_Id, string? Ban_Des, string? Flg_Est)
    // {
    //     var result = new ServiceResponseList<BancoEntity>();
    //     try
    //     {
    //         var resultData = await _repository.ListarBanco(Ban_Id, Ban_Des, Flg_Est);
            
    //         if (resultData == null || !resultData.Any())
    //         {
    //             result.Success = true;
    //             result.Message = "No existe información";
    //             return result;
    //         }
            
    //         result.Success = true;
    //         result.Message = "Completado con éxito";
    //         result.Elements = resultData.ToList();
    //         result.TotalElements = resultData.ToList().Count();

    //         return result;
    //     }
    //     catch (Exception ex)
    //     {
    //         result.Message = "Excepcion no controlada " + ex.Message;
    //         return result;
    //     }
    // }

    public async Task<ServiceResponse<int>> RegistrarAsignacion(AsignacionCabeceraEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.RegistrarAsignacion(valores);

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

    public async Task<ServiceResponse<int>> ActualizarAsignacion(AsignacionCabeceraEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.ActualizarAsignacion(valores);

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
