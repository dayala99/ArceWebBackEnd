using Arce.Web.Entity.Inspecciones;
using Arce.Web.Entity.Usuario;

namespace Arce.Web.Data;

public interface IInspeccionesRepository
{
    Task<IEnumerable<InspeccionEntity>?> ListarInspeccionesAsync();
    Task<IEnumerable<UsuarioEntity>?> ConsultarDatosUsuario(string? Usr_Cod);
    Task<IEnumerable<SubEstacionEntity>?> ListarSubEstacionesPorCliente(int Cliente_Id);
    Task<IEnumerable<InsClienteEntity>?> ListarClientes();
    Task<IEnumerable<InsMotivoEntity>?> ListarMotivos();
    Task<IEnumerable<InsClimaEntity>?> ListarClimas();
    Task<IEnumerable<InsTareaEntity>?> ListarTareas();
    Task<IEnumerable<InsSubContrataEntity>?> ListarSubContratas();
    Task<IEnumerable<InsJefeAreaEntity>?> ListarJefesArea();
    Task<IEnumerable<ObservacionPlaneadaListadoEntity>?> ListarObservacionesPlaneadas();
    Task<(int Codigo, string Mensaje)> RegistrarObservacionPlaneada(ObservacionPlaneadaEntity valores);
}