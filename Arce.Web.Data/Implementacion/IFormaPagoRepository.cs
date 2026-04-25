using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IFormaPagoRepository
{
    Task<IEnumerable<FormaPagoEntity>?> ListarFormaPagoActivo(int? For_Pag_Id, string? For_Pag_Des, string? Flg_Est);
    Task<(int Codigo, string Mensaje)> RegistrarFormaPago(FormaPagoEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarFormaPago(FormaPagoEntity valores);
}
