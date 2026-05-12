using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IBancoRepository
{
    Task<IEnumerable<BancoEntity>?> ListarBanco(int? Ban_Id, string? Ban_Des, string? Flg_Est);
    Task<(int Codigo, string Mensaje)> RegistrarBanco(BancoEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarBanco(BancoEntity valores);
}
