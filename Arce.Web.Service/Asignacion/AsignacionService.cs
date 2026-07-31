using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class AsignacionService: IAsignacionService
{
    private readonly IAsignacionRepository _repository;

    public AsignacionService(IAsignacionRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<AsignacionCabeceraEntity>?> ListarAsignacion(int? Asg_Id, DateTime? Fec_Ini, DateTime? Fec_Fin,
    string? Asg_Usr, string? Usr_Reg, string? Flg_Est, int? Asg_Usr_Cen_Cos)
    {
        var result = new ServiceResponseList<AsignacionCabeceraEntity>();
        try
        {
            var resultData = await _repository.ListarAsignacion(Asg_Id, Fec_Ini, Fec_Fin, Asg_Usr, Usr_Reg, Flg_Est, Asg_Usr_Cen_Cos);
            
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

    public async Task<ServiceResponse<int>> RegistrarAsignacion(AsignacionCabeceraEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.RegistrarAsignacion(valores);

            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                result.Data = resultData.AsignacionId;
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
            return result;
        }
    }

    public async Task<ServiceResponse<int>> ActualizarAsignacion(AsignacionCabeceraEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.ActualizarAsignacion(valores);

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

    public async Task<ServiceResponse<int>> RegistrarAsignacionDetalle(AsignacionDetalleEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.RegistrarAsignacionDetalle(valores);

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

    public async Task<ServiceResponse<int>> ActualizarAsignacionDetalle(AsignacionDetalleEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.ActualizarAsignacionDetalle(valores);

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

    public async Task<ServiceResponseList<AsignacionDetalleEntity>?> ListarDetallesXAsignacion(int? Asg_Id)
    {
        var result = new ServiceResponseList<AsignacionDetalleEntity>();
        try
        {
            var resultData = await _repository.ListarDetallesXAsignacion(Asg_Id);
            
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

    public async Task<ServiceResponseList<AsignacionDetalleEntity>?> ListarAsignacionDetalleModificar(int? Asg_Det_Id)
    {
        var result = new ServiceResponseList<AsignacionDetalleEntity>();
        try
        {
            var resultData = await _repository.ListarAsignacionDetalleModificar(Asg_Det_Id);
            
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

    public async Task<ServiceResponseList<AsignacionCabeceraEntity>?> ListarAsignacionModificar(int? Asg_Id)
    {
        var result = new ServiceResponseList<AsignacionCabeceraEntity>();
        try
        {
            var resultData = await _repository.ListarAsignacionModificar(Asg_Id);
            
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

    public async Task<ServiceResponseList<AsignacionCabeceraEntity>?> ObtenerStockReservadoAsignacion(int? Asg_Usr_Cen_Cos, int? Asg_Det_Itm_Id)
    {
        var result = new ServiceResponseList<AsignacionCabeceraEntity>();
        try
        {
            var resultData = await _repository.ObtenerStockReservadoAsignacion(Asg_Usr_Cen_Cos, Asg_Det_Itm_Id);
            
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

    public async Task<ServiceResponse<int>> EliminarAsignacionDetalle(AsignacionDetalleEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.EliminarAsignacionDetalle(valores);

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

    public async Task<ServiceResponse<int>> EliminarAsignacion(AsignacionCabeceraEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.EliminarAsignacion(valores);

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

    public async Task<ServiceResponse<int>> EliminarAsignacionDetalleTotal(AsignacionDetalleEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.EliminarAsignacionDetalleTotal(valores);

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
