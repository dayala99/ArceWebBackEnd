using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class OrdenCompraService : IOrdenCompraService
{
    private readonly IOrdenCompraRepository _repository;

    public OrdenCompraService(IOrdenCompraRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<OrdenCompraEntity>?> ListarOrdenCompraActivo(int? Ord_Com_Id, string? Ord_Com_Prv, string? Flg_Est)
    {
        var result = new ServiceResponseList<OrdenCompraEntity>();
        try
        {
            var resultData = await _repository.ListarOrdenCompraActivo(Ord_Com_Id, Ord_Com_Prv, Flg_Est);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
            }
            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = resultData.ToList();
            result.TotalElements = resultData.ToList().Count();
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<OrdenCompraEntity>?> ListarOrdenCompraModificar(int? Ord_Com_Id)
    {
        var result = new ServiceResponseList<OrdenCompraEntity>();
        try
        {
            var resultData = await _repository.ListarOrdenCompraModificar(Ord_Com_Id);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
            }
            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = resultData.ToList();
            result.TotalElements = resultData.ToList().Count();
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> RegistrarOrdenCompra(OrdenCompraEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.RegistrarOrdenCompra(valores);
            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                result.Data = resultData.Codigo_Orden_Compra;
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
            result.Data = 0;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> ActualizarOdenCompra(OrdenCompraEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarOdenCompra(valores);
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

    public async Task<ServiceResponseList<OrdenCompraEntity>?> ListarOrdenCompraPendienteAlmacen(int? Ord_Com_Id, string? Ord_Com_Prv, string? Flg_Est)
    {
        var result = new ServiceResponseList<OrdenCompraEntity>();
        try
        {
            var resultData = await _repository.ListarOrdenCompraPendienteAlmacen(Ord_Com_Id, Ord_Com_Prv, Flg_Est);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
            }
            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = resultData.ToList();
            result.TotalElements = resultData.ToList().Count();
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<OrdenCompraEntity>?> ListarCabeceraIngresoAlmacen(int? Ord_Com_Id)
    {
        var result = new ServiceResponseList<OrdenCompraEntity>();
        try
        {
            var resultData = await _repository.ListarCabeceraIngresoAlmacen(Ord_Com_Id);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
            }
            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = resultData.ToList();
            result.TotalElements = resultData.ToList().Count();
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> CambiarEstadoOrdenCompra(OrdenCompraEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.CambiarEstadoOrdenCompra(valores);
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
