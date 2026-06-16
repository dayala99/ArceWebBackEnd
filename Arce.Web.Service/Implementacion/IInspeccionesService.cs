using Arce.Web.Entity.Inspecciones;
using Arce.Web.Entity.Usuario;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IInspeccionesService
{
    Task<IEnumerable<InspeccionEntity>?> ListarInspeccionesAsync();
    Task<ServiceResponseList<UsuarioEntity>?> ConsultarDatosUsuario(string? Usr_Cod);
    Task<ServiceResponseList<SubEstacionEntity>?> ListarSubEstacionesPorCliente(int Cliente_Id);
    Task<ServiceResponseList<InsClienteEntity>?> ListarClientes();
    Task<ServiceResponseList<InsMotivoEntity>?> ListarMotivos();
    Task<ServiceResponseList<InsClimaEntity>?> ListarClimas();
    Task<ServiceResponseList<InsTareaEntity>?> ListarTareas();
    Task<ServiceResponseList<InsSubContrataEntity>?> ListarSubContratas();
    Task<ServiceResponseList<InsJefeAreaEntity>?> ListarJefesArea();
    Task<ServiceResponseList<ObservacionPlaneadaListadoEntity>?> ListarObservacionesPlaneadas();
    Task<ServiceResponse<int>> RegistrarObservacionPlaneada(ObservacionPlaneadaEntity valores);
}