using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IUnidadMedidaRepository
{
    Task<IEnumerable<UnidadMedidaEntity>?> ListarUnidadMedida(int? Uni_Med_Id, string? Uni_Med_Des, string? Flg_Est);
    Task<(int Codigo, string Mensaje)> RegistrarUnidadMedida(UnidadMedidaEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarUnidadMedida(UnidadMedidaEntity valores);

}
