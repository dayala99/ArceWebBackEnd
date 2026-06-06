using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class ItemDetalleMaterialService: IItemDetalleMaterialService
{
    private readonly IItemDetalleMaterialRepository _repository;

    public ItemDetalleMaterialService(IItemDetalleMaterialRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<ItemDetalleMaterialEntity>?> ListarItemDetalleMaterial(string? Det_Mat_Cod, string? Det_Mat_Des, int? Grp_Id, int? Sub_Grp_Id, string? Flg_Est)
    {
        var result = new ServiceResponseList<ItemDetalleMaterialEntity>();
        try
        {
            var resultData = await _repository.ListarItemDetalleMaterial(Det_Mat_Cod, Det_Mat_Des, Grp_Id, Sub_Grp_Id, Flg_Est);
            
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

    public async Task<ServiceResponse<int>> RegistrarItemDetalleMaterial(ItemDetalleMaterialEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.RegistrarItemDetalleMaterial(valores);

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

    public async Task<ServiceResponse<int>> ActualizarItemDetalleMaterial(ItemDetalleMaterialEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.RegistrarItemDetalleMaterial(valores);

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

    public async Task<ServiceResponseList<ItemDetalleMaterialEntity>?> ListarItemDetalleMaterialPorGrupoySubgrupo(int? Grp_Id, int? Sub_Grp_Id)
    {
        var result = new ServiceResponseList<ItemDetalleMaterialEntity>();
        try
        {
            var resultData = await _repository.ListarItemDetalleMaterialPorGrupoySubgrupo(Grp_Id, Sub_Grp_Id);
            
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
}
