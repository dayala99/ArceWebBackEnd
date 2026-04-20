using Arce.Web.Data;
using Arce.Web.Service.Comunes;
using Arce.Web.Entity.Usuario;

namespace Arce.Web.Service;

public class UsuarioService: IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<ServiceResponseList<UsuarioEntity>?> ListarUsuarioActivo(string Usr_Id, string Usr_Cod, string Usr_Nom, string Flg_Est)
    {
        var result = new ServiceResponseList<UsuarioEntity>();
        try
        {
            var resultData = await _usuarioRepository.ListarUsuarioActivo(Usr_Id, Usr_Cod, Usr_Nom, Flg_Est);
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

    public async Task<ServiceResponse<int>> RegistrarUsuario(UsuarioEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _usuarioRepository.RegistrarUsuario(valores);
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

    public async Task<ServiceResponse<int>> ActualizarUsuario(UsuarioEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _usuarioRepository.ActualizarUsuario(valores);
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
