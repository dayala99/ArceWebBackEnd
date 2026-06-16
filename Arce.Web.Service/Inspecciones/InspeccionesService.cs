using Arce.Web.Data;
using Arce.Web.Entity.Inspecciones;
using Arce.Web.Entity.Usuario;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class InspeccionesService : IInspeccionesService
{
    private readonly IInspeccionesRepository _inspeccionesRepository;

    public InspeccionesService(IInspeccionesRepository inspeccionesRepository)
    {
        _inspeccionesRepository = inspeccionesRepository;
    }

    public async Task<IEnumerable<InspeccionEntity>?> ListarInspeccionesAsync()
    {
        return await _inspeccionesRepository.ListarInspeccionesAsync();
    }

    public async Task<ServiceResponseList<UsuarioEntity>?> ConsultarDatosUsuario(string? Usr_Cod)
    {
        var result = new ServiceResponseList<UsuarioEntity>();
        try
        {
            var resultData = await _inspeccionesRepository.ConsultarDatosUsuario(Usr_Cod);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<UsuarioEntity>();
                result.TotalElements = 0;
                return result;
            }
            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = resultData.ToList();
            result.TotalElements = result.Elements!.Count();
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<SubEstacionEntity>?> ListarSubEstacionesPorCliente(int Cliente_Id)
    {
        var result = new ServiceResponseList<SubEstacionEntity>();
        try
        {
            var resultData = await _inspeccionesRepository.ListarSubEstacionesPorCliente(Cliente_Id);
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

    public async Task<ServiceResponseList<InsClienteEntity>?> ListarClientes()
    {
        var result = new ServiceResponseList<InsClienteEntity>();
        try
        {
            var resultData = await _inspeccionesRepository.ListarClientes();
            var elements = (resultData ?? Enumerable.Empty<InsClienteEntity>()).ToList();
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

    public async Task<ServiceResponseList<InsMotivoEntity>?> ListarMotivos()
    {
        var result = new ServiceResponseList<InsMotivoEntity>();
        try
        {
            var resultData = await _inspeccionesRepository.ListarMotivos();
            var elements = (resultData ?? Enumerable.Empty<InsMotivoEntity>()).ToList();
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

    public async Task<ServiceResponseList<InsClimaEntity>?> ListarClimas()
    {
        var result = new ServiceResponseList<InsClimaEntity>();
        try
        {
            var resultData = await _inspeccionesRepository.ListarClimas();
            var elements = (resultData ?? Enumerable.Empty<InsClimaEntity>()).ToList();
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

    public async Task<ServiceResponseList<InsTareaEntity>?> ListarTareas()
    {
        var result = new ServiceResponseList<InsTareaEntity>();
        try
        {
            var resultData = await _inspeccionesRepository.ListarTareas();
            var elements = (resultData ?? Enumerable.Empty<InsTareaEntity>()).ToList();
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

    public async Task<ServiceResponseList<InsSubContrataEntity>?> ListarSubContratas()
    {
        var result = new ServiceResponseList<InsSubContrataEntity>();
        try
        {
            var resultData = await _inspeccionesRepository.ListarSubContratas();
            var elements = (resultData ?? Enumerable.Empty<InsSubContrataEntity>()).ToList();
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

    public async Task<ServiceResponseList<InsJefeAreaEntity>?> ListarJefesArea()
    {
        var result = new ServiceResponseList<InsJefeAreaEntity>();
        try
        {
            var resultData = await _inspeccionesRepository.ListarJefesArea();
            var elements = (resultData ?? Enumerable.Empty<InsJefeAreaEntity>()).ToList();
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

    public async Task<ServiceResponseList<ObservacionPlaneadaListadoEntity>?> ListarObservacionesPlaneadas()
    {
        var result = new ServiceResponseList<ObservacionPlaneadaListadoEntity>();
        try
        {
            var resultData = await _inspeccionesRepository.ListarObservacionesPlaneadas();
            var elements = (resultData ?? Enumerable.Empty<ObservacionPlaneadaListadoEntity>()).ToList();
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

    public async Task<ServiceResponse<int>> RegistrarObservacionPlaneada(ObservacionPlaneadaEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _inspeccionesRepository.RegistrarObservacionPlaneada(valores);
            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                result.Data = 1;
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
}