using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class PedidoService: IPedidoService
{
    private readonly IPedidoRepository _repository;

    public PedidoService(IPedidoRepository pedidoRepository)
    {
        _repository = pedidoRepository;
    }
    public async Task<ServiceResponseList<PedidoCabeceraEntity>?> ListarPedido(int? Ped_Id, string? Prv_Nom, string? Flg_Est, string? Ped_Tip_Com)
    {
        var result = new ServiceResponseList<PedidoCabeceraEntity>();
        try
        {
            var resultData = await _repository.ListarPedido(Ped_Id, Prv_Nom, Flg_Est, Ped_Tip_Com);

            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PedidoCabeceraEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<PedidoCabeceraEntity>?> ListarPedidoCorrelativoNuevo()
    {
        var result = new ServiceResponseList<PedidoCabeceraEntity>();
        try
        {
            var resultData = await _repository.ListarPedidoCorrelativoNuevo();
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PedidoCabeceraEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> RegistrarPedido(PedidoCabeceraEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.RegistrarPedido(valores);
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

    public async Task<ServiceResponse<int>> ActualizarPedido(PedidoCabeceraEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarPedido(valores);
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

    public async Task<ServiceResponse<int>> ActualizarPedidoEstado(PedidoCabeceraEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarPedidoEstado(valores);
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

    public async Task<ServiceResponseList<PedidoCabeceraCentroCostoEntity>?> ListarPedidoRegistradoCentroCosto(int? Ped_Id)
    {
        var result = new ServiceResponseList<PedidoCabeceraCentroCostoEntity>();
        try
        {
            var resultData = await _repository.ListarPedidoRegistradoCentroCosto(Ped_Id);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PedidoCabeceraCentroCostoEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> RegistrarCentroCostoPedidoRegistrado(PedidoCabeceraCentroCostoEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.RegistrarCentroCostoPedidoRegistrado(valores);
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
