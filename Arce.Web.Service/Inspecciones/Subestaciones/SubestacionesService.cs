using Arce.Web.Data;
using Arce.Web.Entity.Inspecciones;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service.Inspecciones.Subestaciones;

public class SubestacionesService : ISubestacionesService
{
    private readonly ISubestacionesRepository _repository;

    public SubestacionesService(ISubestacionesRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<SubEstacionEntity>?> ListarSubEstaciones(int? Id, string? Nombre, int? Cliente_Id, string? Estado)
    {
        var result = new ServiceResponseList<SubEstacionEntity>();
        try
        {
            var resultData = await _repository.ListarSubEstaciones(
                Id,
                string.IsNullOrWhiteSpace(Nombre) ? string.Empty : Nombre.Trim(),
                Cliente_Id,
                string.IsNullOrWhiteSpace(Estado) ? "A" : Estado.Trim().Substring(0, 1).ToUpperInvariant()
            );

            var elements = (resultData ?? Enumerable.Empty<SubEstacionEntity>()).ToList();
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

    public async Task<ServiceResponseList<SubEstacionEntity>?> ConsultarEditarSubEstaciones(int? Subestacion_Id)
    {
        var result = new ServiceResponseList<SubEstacionEntity>();
        try
        {
            var resultData = await _repository.ConsultarEditarSubEstaciones(Subestacion_Id);
            var elements = (resultData ?? Enumerable.Empty<SubEstacionEntity>()).ToList();

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

    public async Task<ServiceResponse<int>> RegistrarSubEstacion(SubEstacionEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.RegistrarSubEstacion(valores);
            result.Success = resultData.Codigo == 0;
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

    public async Task<ServiceResponse<int>> ActualizarSubEstacion(SubEstacionEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarSubEstacion(valores);
            result.Success = resultData.Codigo == 0;
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
