using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class SubGrupoItemService: ISubGrupoItemService
{
    private readonly ISubGrupoItemRepository _repository;

    public SubGrupoItemService(ISubGrupoItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<SubGrupoItemEntity>?> ListarSubGrupoItem(string? Sub_Grp_Cod, string? Sub_Grp_Des, string? Flg_Est)
    {
        var result = new ServiceResponseList<SubGrupoItemEntity>();
        try
        {
            var resultData = await _repository.ListarSubGrupoItem(Sub_Grp_Cod, Sub_Grp_Des, Flg_Est);
            
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

    public async Task<ServiceResponse<int>> RegistrarSubGrupoItem(SubGrupoItemEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.RegistrarSubGrupoItem(valores);

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

    public async Task<ServiceResponse<int>> ActualizarSubGrupoItem(SubGrupoItemEntity valores)
    {
        var result = new ServiceResponse<int>();

        try
        {
            var resultData = await _repository.ActualizarSubGrupoItem(valores);

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

    public async Task<ServiceResponseList<SubGrupoItemEntity>?> ListarSubGrupoItemPorGrpId(int? Grp_Id)
    {
        var result = new ServiceResponseList<SubGrupoItemEntity>();
        try
        {
            var resultData = await _repository.ListarSubGrupoItemPorGrpId(Grp_Id);
            
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
