using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IMonedaRepository
{
    Task<IEnumerable<MonedaEntity>?> ListarMoneda(int? Mon_Id, string? Mon_Des, string? Flg_Est);
    Task<(int Codigo, string Mensaje)> RegistrarMoneda(MonedaEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarMoneda(MonedaEntity valores);
}
