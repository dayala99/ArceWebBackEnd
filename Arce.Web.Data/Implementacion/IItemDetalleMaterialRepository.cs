using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IItemDetalleMaterialRepository
{
    Task<IEnumerable<ItemDetalleMaterialEntity>?> ListarItemDetalleMaterial(string? Det_Mat_Cod, string? Det_Mat_Des, int? Grp_Id, int? Sub_Grp_Id, string? Flg_Est);
    Task<(int Codigo, string Mensaje)> RegistrarItemDetalleMaterial(ItemDetalleMaterialEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarItemDetalleMaterial(ItemDetalleMaterialEntity valores);
    Task<IEnumerable<ItemDetalleMaterialEntity>?> ListarItemDetalleMaterialPorGrupoySubgrupo(int? Grp_Id, int? Sub_Grp_Id);
}
