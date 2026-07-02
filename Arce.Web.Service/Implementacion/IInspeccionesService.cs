using Arce.Web.Entity.Inspecciones;
using Arce.Web.Entity.Usuario;
using Arce.Web.Service.Comunes;
using System;

namespace Arce.Web.Service;

public interface IInspeccionesService
{
    Task<IEnumerable<InspeccionEntity>?> ListarInspeccionesAsync();
    Task<ServiceResponseList<UsuarioEntity>?> ConsultarDatosUsuario(string? Usr_Cod);
    Task<ServiceResponseList<SubEstacionEntity>?> ListarSubEstacionesPorCliente(int Cliente_Id);
    Task<ServiceResponseList<SubEstacionEntity>?> ListarSubEstaciones(int? Id, string? Nombre, int? Cliente_Id, string? Estado);
    Task<ServiceResponseList<InsClienteEntity>?> ListarClientes();
    Task<ServiceResponseList<InsMotivoEntity>?> ListarMotivos();
    Task<ServiceResponseList<InsClimaEntity>?> ListarClimas();
    Task<ServiceResponseList<InsTareaEntity>?> ListarTareas();
    Task<ServiceResponseList<InsSubContrataEntity>?> ListarSubContratas();
    Task<ServiceResponseList<InsJefeAreaEntity>?> ListarJefesArea();
    // NUEVO: devuelve Cen_Cos_Des y Usr_Doc_Nro del jefe
    Task<ServiceResponseList<InsJefeDatosEntity>?> MostrarJefe(string Jefe_Cod);
    Task<ServiceResponseList<ObservacionPlaneadaListadoEntity>?> ListarObservacionesPlaneadas();
    Task<ServiceResponseList<ObservacionPlaneadaListadoEntity>?> ConsultarEstadoObservaciones(string Estado);
    Task<ServiceResponseList<ObservacionPlaneadaListadoEntity>?> FiltrarObservaciones(DateTime? Fecha_Desde, DateTime? Fecha_Hasta, string? Estado);
    Task<ServiceResponseList<ObservacionPlaneadaDetalleEntity>?> MostrarObservacionPlaneada(string Codigo_Obs);
    Task<ServiceResponse<int>> RegistrarObservacionPlaneada(ObservacionPlaneadaEntity valores);
    Task<ServiceResponse<int>> ActualizarObservacionPlaneada(ActualizarObservacionPlaneadaEntity valores);
    Task<ServiceResponse<int>> EliminarObservacionPlaneada(EliminarObservacionPlaneadaEntity valores);
    Task<ServiceResponseList<InsTipoInspeccionEntity>?> ListarTiposInspeccion();
    Task<ServiceResponseList<PrevencionListadoEntity>?> FiltrarPrevencion(DateTime? Fecha_Desde, DateTime? Fecha_Hasta, string? Estado);
    Task<ServiceResponseList<PrevencionDetalleEntity>?> MostrarPrevencion(int Prevencion_Id);
    Task<ServiceResponse<int>> InsertarPrevencion(InsPrevencionEntity valores);
    Task<ServiceResponse<int>> ActualizarPrevencion(ActualizarPrevencionEntity valores);
    Task<ServiceResponse<int>> EliminarPrevencion(EliminarPrevencionEntity valores);
    Task<ServiceResponse<int>> InsertarMedioAmbiente(InsMedioAmbienteEntity valores);
    Task<ServiceResponseList<MedioAmbienteListadoEntity>?> FiltrarMedioAmbiente(DateTime? Fecha_Desde, DateTime? Fecha_Hasta, string? Estado);
    Task<ServiceResponseList<MedioAmbienteDetalleEntity>?> MostrarMedioAmbiente(int Medio_Ambiente_Id);
    Task<ServiceResponse<int>> ActualizarMedioAmbiente(ActualizarMedioAmbienteEntity valores);
    Task<ServiceResponse<int>> EliminarMedioAmbiente(EliminarMedioAmbienteEntity valores);
}
