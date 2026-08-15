using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IObraService
{
    Task<ServiceResponseList<ObraEntity>?> ListarObra(
        int? Obr_Id, int? Obr_Cen_Cos, string? Obr_Nom, string? Obr_Ubi, string? Obr_Are,
        int? Obr_Cli_Id, string? Flg_Est, string? Obr_Rsp
        );
    Task<ServiceResponse<int>> RegistrarObra(ObraEntity valores);
    Task<ServiceResponse<int>> ActualizarObra(ObraEntity valores);
    Task<ServiceResponseList<ObraEntity>?> CargarObraModificar(int? Obr_Id);
    Task<ServiceResponse<int>> ActualizarFechaInicioObra(ObraEntity valores);
    Task<ServiceResponse<int>> ActualizarFechaFinObra(ObraEntity valores);
    Task<ServiceResponse<int>> ActualizarFechaCierreObra(ObraEntity valores);
}
