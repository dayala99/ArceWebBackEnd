using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IDetraccionRepository
{
    Task<IEnumerable<DetraccionEntity>?> ListarDetraccion(int? Det_Id, string? Det_Des, string? Flg_Est);
    Task<(int Codigo, string Mensaje)> RegistrarDetraccion(DetraccionEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarDetraccion(DetraccionEntity valores);
}
