using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class FormaPagoService: IFormaPagoService
{
    private readonly IFormaPagoRepository _formaPagoRepository;

    public FormaPagoService(IFormaPagoRepository formaPagoRepository)
    {
        _formaPagoRepository = formaPagoRepository;
    }

    public async Task<ServiceResponseList<FormaPagoEntity>?> ListarFormaPagoActivo(int? For_Pag_Id, string? For_Pag_Des, string? Flg_Est)
    {
        var result = new ServiceResponseList<FormaPagoEntity>();
        try
        {
            var resultData = await _formaPagoRepository.ListarFormaPagoActivo(For_Pag_Id, For_Pag_Des, Flg_Est);
            
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

    public async Task<ServiceResponse<int>> RegistrarFormaPago(FormaPagoEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _formaPagoRepository.RegistrarFormaPago(valores);

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

    public async Task<ServiceResponse<int>> ActualizarFormaPago(FormaPagoEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _formaPagoRepository.ActualizarFormaPago(valores);

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
