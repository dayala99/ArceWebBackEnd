using Arce.Web.Entity;
using Arce.Web.Data;

namespace Arce.Web.Data;

public interface ICentroCostoRepository
{
    Task<IEnumerable<CentroCostoEntity>?> ListarCentroCostoActivo(int? Cen_Cos_Id, string? Cen_Cos_Des, string? Flg_Est);
    Task<IEnumerable<CentroCostoEntity>?> ListarCentroCostoParaJefe();
    Task<(int Codigo, string Mensaje)> RegistrarCentroCosto(CentroCostoEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarCentroCosto(CentroCostoEntity valores);
}
