using Arce.Web.Entity.Inspecciones;
using Arce.Web.Entity.Usuario;
using System;

namespace Arce.Web.Data;

public interface IInspeccionesRepository
{
    Task<IEnumerable<InspeccionEntity>?> ListarInspeccionesAsync();
    Task<IEnumerable<UsuarioEntity>?> ConsultarDatosUsuario(string? Usr_Cod);
    Task<IEnumerable<SubEstacionEntity>?> ListarSubEstacionesPorCliente(int Cliente_Id);
    Task<IEnumerable<SubEstacionEntity>?> ListarSubEstaciones(int? Id, string? Nombre, int? Cliente_Id, string? Estado);
    Task<IEnumerable<InsClienteEntity>?> ListarClientes();
    Task<IEnumerable<InsMotivoEntity>?> ListarMotivos();
    Task<IEnumerable<InsClimaEntity>?> ListarClimas();
    Task<IEnumerable<InsTareaEntity>?> ListarTareas();
    Task<IEnumerable<InsSubContrataEntity>?> ListarSubContratas();
    Task<IEnumerable<InsJefeAreaEntity>?> ListarJefesArea();
    Task<IEnumerable<ObservacionPlaneadaListadoEntity>?> ListarObservacionesPlaneadas();
    Task<IEnumerable<ObservacionPlaneadaListadoEntity>?> ConsultarEstadoObservaciones(string Estado);
    Task<IEnumerable<ObservacionPlaneadaListadoEntity>?> FiltrarObservaciones(DateTime? Fecha_Desde, DateTime? Fecha_Hasta, string? Estado);
    Task<IEnumerable<ObservacionPlaneadaDetalleEntity>?> MostrarObservacionPlaneada(string Codigo_Obs);
    Task<(int Codigo, string Mensaje)> RegistrarObservacionPlaneada(ObservacionPlaneadaEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarObservacionPlaneada(ActualizarObservacionPlaneadaEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarObservacionPlaneada(EliminarObservacionPlaneadaEntity valores);
}
