using Arce.Web.Data;
using Arce.Web.Entity.Inspecciones;
using Arce.Web.Entity.Usuario;
using Arce.Web.Service.Comunes;
using System;

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

    public async Task<ServiceResponseList<SubEstacionEntity>?> ListarSubEstaciones(int? Id, string? Nombre, int? Cliente_Id, string? Estado)
    {
        var result = new ServiceResponseList<SubEstacionEntity>();
        try
        {
            var resultData = await _inspeccionesRepository.ListarSubEstaciones(
                Id ?? 0,
                string.IsNullOrWhiteSpace(Nombre) ? string.Empty : Nombre.Trim(),
                Cliente_Id ?? 0,
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

    // NUEVO: MostrarJefe — llama a SP_Mostrar_Jefe y devuelve Cen_Cos_Des + Usr_Doc_Nro
    public async Task<ServiceResponseList<InsJefeDatosEntity>?> MostrarJefe(string Jefe_Cod)
    {
        var result = new ServiceResponseList<InsJefeDatosEntity>();
        try
        {
            var datos = await _inspeccionesRepository.MostrarJefe(Jefe_Cod);
            if (datos != null)
            {
                result.Success = true;
                result.Message = "Completado con éxito";
                result.Elements = new List<InsJefeDatosEntity> { datos };
                result.TotalElements = 1;
            }
            else
            {
                result.Success = false;
                result.Message = "No existe información";
                result.Elements = new List<InsJefeDatosEntity>();
                result.TotalElements = 0;
            }
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
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

    public async Task<ServiceResponseList<ObservacionPlaneadaListadoEntity>?> ConsultarEstadoObservaciones(string Estado)
    {
        var result = new ServiceResponseList<ObservacionPlaneadaListadoEntity>();
        try
        {
            var estadoNormalizado = string.IsNullOrWhiteSpace(Estado) ? "I" : Estado.Trim().Substring(0, 1).ToUpperInvariant();
            var resultData = await _inspeccionesRepository.ConsultarEstadoObservaciones(estadoNormalizado);
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

    

public async Task<ServiceResponseList<ObservacionPlaneadaListadoEntity>?> FiltrarObservaciones(DateTime? Fecha_Desde, DateTime? Fecha_Hasta, string? Estado)
{
    var result = new ServiceResponseList<ObservacionPlaneadaListadoEntity>();
    try
    {
        if (!Fecha_Desde.HasValue || !Fecha_Hasta.HasValue)
        {
            result.Success = true;
            result.Message = "No existe información";
            result.Elements = new List<ObservacionPlaneadaListadoEntity>();
            result.TotalElements = 0;
            return result;
        }

        var estadoNormalizado = string.IsNullOrWhiteSpace(Estado)
            ? "A"
            : Estado.Trim().Substring(0, 1).ToUpperInvariant();

        var resultData = await _inspeccionesRepository.FiltrarObservaciones(
            Fecha_Desde.Value,
            Fecha_Hasta.Value,
            estadoNormalizado
        );

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

public async Task<ServiceResponseList<ObservacionPlaneadaDetalleEntity>?> MostrarObservacionPlaneada(string Codigo_Obs)
    {
        var result = new ServiceResponseList<ObservacionPlaneadaDetalleEntity>();
        try
        {
            var resultData = await _inspeccionesRepository.MostrarObservacionPlaneada(Codigo_Obs);
            var elements = (resultData ?? Enumerable.Empty<ObservacionPlaneadaDetalleEntity>()).ToList();
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

    public async Task<ServiceResponse<int>> ActualizarObservacionPlaneada(ActualizarObservacionPlaneadaEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _inspeccionesRepository.ActualizarObservacionPlaneada(valores);
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

    public async Task<ServiceResponse<int>> EliminarObservacionPlaneada(EliminarObservacionPlaneadaEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _inspeccionesRepository.EliminarObservacionPlaneada(valores);
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
    public async Task<ServiceResponseList<InsTipoInspeccionEntity>?> ListarTiposInspeccion()
    {
        var result = new ServiceResponseList<InsTipoInspeccionEntity>();
        try
        {
            var resultData = await _inspeccionesRepository.ListarTiposInspeccion();
            var elements = resultData?.ToList() ?? new List<InsTipoInspeccionEntity>();
            result.Success = true;
            result.Elements = elements;
            result.TotalElements = elements.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> InsertarMedioAmbiente(InsMedioAmbienteEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _inspeccionesRepository.InsertarMedioAmbiente(valores);
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




    public async Task<ServiceResponseList<PrevencionListadoEntity>?> FiltrarPrevencion(DateTime? Fecha_Desde, DateTime? Fecha_Hasta, string? Estado)
    {
        var result = new ServiceResponseList<PrevencionListadoEntity>();
        try
        {
            if (!Fecha_Desde.HasValue || !Fecha_Hasta.HasValue)
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PrevencionListadoEntity>();
                result.TotalElements = 0;
                return result;
            }

            var estadoNormalizado = string.IsNullOrWhiteSpace(Estado)
                ? "A"
                : Estado.Trim().Substring(0, 1).ToUpperInvariant();

            var resultData = await _inspeccionesRepository.FiltrarPrevencion(
                Fecha_Desde.Value,
                Fecha_Hasta.Value,
                estadoNormalizado
            );

            var elements = (resultData ?? Enumerable.Empty<PrevencionListadoEntity>()).ToList();
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

    public async Task<ServiceResponseList<PrevencionDetalleEntity>?> MostrarPrevencion(int Prevencion_Id)
    {
        var result = new ServiceResponseList<PrevencionDetalleEntity>();
        try
        {
            var resultData = await _inspeccionesRepository.MostrarPrevencion(Prevencion_Id);
            var elements = (resultData ?? Enumerable.Empty<PrevencionDetalleEntity>()).ToList();
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

    public async Task<ServiceResponse<int>> InsertarPrevencion(InsPrevencionEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _inspeccionesRepository.InsertarPrevencion(valores);
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

    public async Task<ServiceResponse<int>> ActualizarPrevencion(ActualizarPrevencionEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _inspeccionesRepository.ActualizarPrevencion(valores);
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

    public async Task<ServiceResponse<int>> EliminarPrevencion(EliminarPrevencionEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _inspeccionesRepository.EliminarPrevencion(valores);
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



public async Task<ServiceResponseList<MedioAmbienteListadoEntity>?> FiltrarMedioAmbiente(DateTime? Fecha_Desde, DateTime? Fecha_Hasta, string? Estado)
{
    var result = new ServiceResponseList<MedioAmbienteListadoEntity>();
    try
    {
        var resultData = await _inspeccionesRepository.FiltrarMedioAmbiente(Fecha_Desde, Fecha_Hasta, Estado);
        var elements = resultData?.ToList() ?? new List<MedioAmbienteListadoEntity>();
        result.Success = true;
        result.Elements = elements;
        result.TotalElements = elements.Count;
        return result;
    }
    catch (Exception ex)
    {
        result.Success = false;
        result.Message = "Error inesperado " + ex.Message;
        return result;
    }
}

public async Task<ServiceResponseList<MedioAmbienteDetalleEntity>?> MostrarMedioAmbiente(int Medio_Ambiente_Id)
{
    var result = new ServiceResponseList<MedioAmbienteDetalleEntity>();
    try
    {
        var resultData = await _inspeccionesRepository.MostrarMedioAmbiente(Medio_Ambiente_Id);
        var elements = resultData?.ToList() ?? new List<MedioAmbienteDetalleEntity>();
        result.Success = true;
        result.Elements = elements;
        result.TotalElements = elements.Count;
        return result;
    }
    catch (Exception ex)
    {
        result.Success = false;
        result.Message = "Error inesperado " + ex.Message;
        return result;
    }
}

public async Task<ServiceResponse<int>> ActualizarMedioAmbiente(ActualizarMedioAmbienteEntity valores)
{
    var result = new ServiceResponse<int>();
    try
    {
        var resultData = await _inspeccionesRepository.ActualizarMedioAmbiente(valores);
        if (resultData.Codigo == 0)
        {
            result.Success = true;
            result.Message = string.IsNullOrWhiteSpace(resultData.Mensaje) ? null : resultData.Mensaje;
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

public async Task<ServiceResponse<int>> EliminarMedioAmbiente(EliminarMedioAmbienteEntity valores)
{
    var result = new ServiceResponse<int>();
    try
    {
        var resultData = await _inspeccionesRepository.EliminarMedioAmbiente(valores);
        if (resultData.Codigo == 0)
        {
            result.Success = true;
            result.Message = string.IsNullOrWhiteSpace(resultData.Mensaje) ? null : resultData.Mensaje;
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
