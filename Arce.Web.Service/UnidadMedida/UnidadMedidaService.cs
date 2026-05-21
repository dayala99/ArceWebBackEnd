using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class UnidadMedidaService : IUnidadMedidaService
{
    private readonly IUnidadMedidaRepository _repository;

    public UnidadMedidaService(IUnidadMedidaRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<UnidadMedidaEntity>?> ListarUnidadMedida(int? Uni_Med_Id, string? Uni_Med_Des, string? Flg_Est)
    {
        var result = new ServiceResponseList<UnidadMedidaEntity>();

        try
        {
            var resultData = await _repository.ListarUnidadMedida(Uni_Med_Id, Uni_Med_Des, Flg_Est);
            var elements = (resultData ?? Enumerable.Empty<UnidadMedidaEntity>()).ToList();

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

    public async Task<ServiceResponse<int>> RegistrarUnidadMedida(UnidadMedidaEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.RegistrarUnidadMedida(valores);

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

    public async Task<ServiceResponse<int>> ActualizarUnidadMedida(UnidadMedidaEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.ActualizarUnidadMedida(valores);

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
