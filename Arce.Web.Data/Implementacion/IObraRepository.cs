using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IObraRepository
{
    Task<IEnumerable<ObraEntity>?> ListarObra(
        int? Obr_Id, int? Obr_Cen_Cos, string? Obr_Nom, string? Obr_Ubi, string? Obr_Are,
        int? Obr_Cli_Id, string? Flg_Est, string? Obr_Rsp
        );
    Task<(int Codigo, string Mensaje)> RegistrarObra(ObraEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarObra(ObraEntity valores);
    Task<IEnumerable<ObraEntity>?> CargarObraModificar(int? Obr_Id);
    Task<(int Codigo, string Mensaje)> ActualizarFechaInicioObra(ObraEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarFechaFinObra(ObraEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarFechaCierreObra(ObraEntity valores);
}
