using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IDireccionEntregaRepository
{   
    Task<IEnumerable<DireccionEntregaEntity>?> ListarDireccionEntregaActivo(int? Dir_Id, string? Dir_Des, string? Flg_Est);
    Task<(int Codigo, string Mensaje)> RegistrarDireccionEntrega(DireccionEntregaEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarDireccionEntrega(DireccionEntregaEntity valores);
}
