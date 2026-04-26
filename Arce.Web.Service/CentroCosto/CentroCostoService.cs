using Arce.Web.Service.Comunes;
using Arce.Web.Data;
using Arce.Web.Entity;
using System.Linq.Expressions;

namespace Arce.Web.Service;

public class CentroCostoService: ICentroCostoService
{
    private readonly ICentroCostoRepository _centroCostoRepository;

    public CentroCostoService(ICentroCostoRepository centroCostoRepository)
    {
        _centroCostoRepository = centroCostoRepository;
    }

    public async Task<ServiceResponseList<CentroCostoEntity>?> ListarCentroCostoActivo(int? Cen_Cos_Id, string? Cen_Cos_Des, string? Flg_Est)
    {
        var result = new ServiceResponseList<CentroCostoEntity>();

        try
        {
            var resultData = await _centroCostoRepository.ListarCentroCostoActivo(Cen_Cos_Id, Cen_Cos_Des, Flg_Est);

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

    public async Task<ServiceResponse<int>> RegistrarCentroCosto(CentroCostoEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _centroCostoRepository.RegistrarCentroCosto(valores);

            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }

            result.Success = false;
            result.Message = resultData.Mensaje;
            result.CodeTransacc = resultData.Codigo;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> ActualizarCentroCosto(CentroCostoEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _centroCostoRepository.ActualizarCentroCosto(valores);

            if(resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }

            result.Success = false;
            result.Message = resultData.Mensaje;
            result.CodeTransacc = resultData.Codigo;
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
