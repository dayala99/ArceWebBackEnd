using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service.Inspecciones.SubContrata;

public class SubContrataService : ISubContrataService
{
    private readonly ISubContrataRepository _repository;

    public SubContrataService(ISubContrataRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<SubContrataEntity>?> ListarSubContrata(int? Id, string? Nombre, string? Estado)
    {
        var result = new ServiceResponseList<SubContrataEntity>();

        try
        {
            var resultData = await _repository.ListarSubContrata(Id, Nombre, Estado);
            var elements = (resultData ?? Enumerable.Empty<SubContrataEntity>()).ToList();

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

    public async Task<ServiceResponseList<SubContrataEntity>?> ConsultarDatosSubContrata(int? SubContrata_Id)
    {
        var result = new ServiceResponseList<SubContrataEntity>();

        try
        {
            var resultData = await _repository.ConsultarDatosSubContrata(SubContrata_Id);
            var elements = (resultData ?? Enumerable.Empty<SubContrataEntity>()).ToList();

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

    public async Task<ServiceResponse<int>> RegistrarSubContrata(SubContrataEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.RegistrarSubContrata(valores);

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
            result.Message = "Excepcion no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> ActualizarSubContrata(SubContrataEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarSubContrata(valores);

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
            result.Message = "Excepcion no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> EliminarSubContrata(int? Id, string? Usr_Mod)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.EliminarSubContrata(Id, Usr_Mod);

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
            result.Message = "Excepcion no controlada " + ex.Message;
            return result;
        }
    }
}
