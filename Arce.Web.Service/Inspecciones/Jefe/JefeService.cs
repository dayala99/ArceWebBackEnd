using Arce.Web.Data;
using Arce.Web.Service;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service.Inspecciones.Jefe;

public class JefeService : IJefeService
{
    private readonly IJefeRepository _repository;

    public JefeService(IJefeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<JefeEntity>?> ListarJefe(int? Id, string? Nombre, string? Dni, string? Estado)
    {
        var result = new ServiceResponseList<JefeEntity>();
        try
        {
            var resultData = await _repository.ListarJefe(Id, Nombre, Dni, Estado);
            var elements = (resultData ?? Enumerable.Empty<JefeEntity>()).ToList();

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

    public async Task<ServiceResponseList<JefeEntity>?> ConsultarDatosJefe(int? Jefe_Id)
    {
        var result = new ServiceResponseList<JefeEntity>();
        try
        {
            var resultData = await _repository.ConsultarDatosJefe(Jefe_Id);
            var elements = (resultData ?? Enumerable.Empty<JefeEntity>()).ToList();

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

    public async Task<ServiceResponse<int>> RegistrarJefe(JefeEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.RegistrarJefe(valores);

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

    public async Task<ServiceResponse<int>> ActualizarJefe(JefeEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarJefe(valores);

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

    public async Task<ServiceResponse<int>> EliminarJefe(int? Id, string? Usr_Mod)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.EliminarJefe(Id, Usr_Mod);

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
