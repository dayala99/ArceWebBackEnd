using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class ItemService: IItemService
{
    private readonly IItemRepository _repository;

    public ItemService(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<ItemEntity>?> ListarItem(string? Itm_Cod, string? Itm_Des, int? Itm_Grp, int? Itm_Sub_Grp, int? Itm_Det_Mat_Id,string? Flg_Est)
    {
        var result = new ServiceResponseList<ItemEntity>();
        try
        {
            var resultData = await _repository.ListarItem(Itm_Cod, Itm_Des, Itm_Grp, Itm_Sub_Grp, Itm_Det_Mat_Id, Flg_Est);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<ItemEntity>();
                result.TotalElements = 0;
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
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> RegistrarItem(ItemEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.RegistrarItem(valores);
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

    public async Task<ServiceResponse<int>> ActualizarItem(ItemEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarItem(valores);
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

    public async Task<ServiceResponse<int>> ActualizarStockItem(ItemEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarStockItem(valores);
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

    public async Task<ServiceResponse<int>> ActualizarStockItemIngresoDirecto(ItemEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarStockItemIngresoDirecto(valores);
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

    public async Task<ServiceResponse<int>> ActualizarStockItemSalida(ItemEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarStockItemSalida(valores);
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
