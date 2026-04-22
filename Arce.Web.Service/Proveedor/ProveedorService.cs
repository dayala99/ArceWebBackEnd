using Arce.Web.Data;
using Arce.Web.Service.Comunes;
using Arce.Web.Entity.Proveedor;
namespace Arce.Web.Service;

public class ProveedorService: IProveedorService
{
    private readonly IProveedorRepository _proveedorRepository;

    public ProveedorService(IProveedorRepository proveedorRepository)
    {
        _proveedorRepository = proveedorRepository;
    }

    public async Task<ServiceResponseList<ProveedorEntity>?> ListarProveedorActivo(int? Prv_Id, string? Prv_Nom, string? Prv_Ruc, string? Prv_Nom_Con, string? Flg_Est)
    {
        var result = new ServiceResponseList<ProveedorEntity>();
        try
        {
            var resultData = await _proveedorRepository.ListarProveedorActivo(Prv_Id, Prv_Nom, Prv_Ruc, Prv_Nom_Con, Flg_Est);
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

    public async Task<ServiceResponse<int>> RegistrarProveedor(ProveedorEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _proveedorRepository.RegistrarProveedor(valores);
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

    public async Task<ServiceResponse<int>> ActualizarProveedor(ProveedorEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _proveedorRepository.ActualizarProveedor(valores);
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
