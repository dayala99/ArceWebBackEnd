using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IFormaPagoService
{
    Task<ServiceResponseList<FormaPagoEntity>?> ListarFormaPagoActivo(int? For_Pag_Id, string? For_Pag_Des, string? Flg_Est);
    Task<ServiceResponse<int>> RegistrarFormaPago(FormaPagoEntity valores);
    Task<ServiceResponse<int>> ActualizarFormaPago(FormaPagoEntity valores);
}
