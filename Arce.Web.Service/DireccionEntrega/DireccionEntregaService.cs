using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class DireccionEntregaService: IDireccionEntregaService
{
    private readonly IDireccionEntregaRepository _repository;

    public DireccionEntregaService(IDireccionEntregaRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<DireccionEntregaEntity>?> ListarDireccionEntregaActivo(int? Dir_Id, string? Dir_Des, string? Flg_Est)
    {
        var result = new ServiceResponseList<DireccionEntregaEntity>();

        try
        {
            var resultData = await _repository.ListarDireccionEntregaActivo(Dir_Id, Dir_Des, Flg_Est);

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

    public async Task<ServiceResponse<int>> RegistrarDireccionEntrega(DireccionEntregaEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.RegistrarDireccionEntrega(valores);

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

    public async Task<ServiceResponse<int>> ActualizarDireccionEntrega(DireccionEntregaEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.ActualizarDireccionEntrega(valores);

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
