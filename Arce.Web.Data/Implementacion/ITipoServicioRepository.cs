using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface ITipoServicioRepository
{
    Task<IEnumerable<TipoServicioEntity>?> ListarTipoServicioActivo(int? Tip_Ser_Id, string? Tip_Ser_Des, string? Flg_Est);
    Task<(int Codigo, string Mensaje)> RegistrarTipoServicio(TipoServicioEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarTipoServicio(TipoServicioEntity valores);
}
