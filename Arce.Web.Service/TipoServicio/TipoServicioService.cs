using System.Security.Cryptography.X509Certificates;
using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class TipoServicioService: ITipoServicioService
{
    private readonly ITipoServicioRepository _tipoServicioRepository;

    public TipoServicioService(ITipoServicioRepository tipoServicioRepository)
    {
        _tipoServicioRepository = tipoServicioRepository;
    }

    public async Task<ServiceResponseList<TipoServicioEntity>?> ListarTipoServicioActivo(int? Tip_Ser_Id, string? Tip_Ser_Des, string? Flg_Est)
    {
        var result = new ServiceResponseList<TipoServicioEntity>();

        try
        {
            var resultData = await _tipoServicioRepository.ListarTipoServicioActivo(Tip_Ser_Id, Tip_Ser_Des, Flg_Est);
            var elements = (resultData ?? Enumerable.Empty<TipoServicioEntity>()).ToList();

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

    public async Task<ServiceResponse<int>> RegistrarTipoServicio(TipoServicioEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _tipoServicioRepository.RegistrarTipoServicio(valores);

            if(resultData.Codigo == 0)
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
            result.Message = "Excepcion no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> ActualizarTipoServicio(TipoServicioEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _tipoServicioRepository.ActualizarTipoServicio(valores);

            if(resultData.Codigo == 0)
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
            result.Message = "Excepcion no controlada " + ex.Message;
            return result;
        }
    }
}
