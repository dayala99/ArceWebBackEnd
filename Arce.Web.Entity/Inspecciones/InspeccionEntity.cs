namespace Arce.Web.Entity.Inspecciones
{
    public class InspeccionEntity
    {
        public int? Inspeccion_Id { get; set; }
        public string? Inspeccion_Des { get; set; }
        public string? Flg_Est { get; set; }
    }

    public class SubEstacionEntity
    {
        public int? Subestacion_Id { get; set; }
        public string? Subestacion_Nombre { get; set; }
        public int? Cliente_Id { get; set; }
        public string? Cliente_Nombre { get; set; }
        public string? Estado { get; set; }
        public string? Usr_Reg { get; set; }
        public DateTime? Fec_Reg { get; set; }
        public string? Usr_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; }
    }

    public class InsClienteEntity
    {
        public int? Cliente_Id { get; set; }
        public string? Cliente_Nombre { get; set; }
    }

    public class InsMotivoEntity
    {
        public int? Motivo_Id { get; set; }
        public string? Motivo_Nombre { get; set; }
    }

    public class InsClimaEntity
    {
        public int? Clima_Id { get; set; }
        public string? Clima_Nombre { get; set; }
    }

    public class InsTareaEntity
    {
        public int? Tarea_Id { get; set; }
        public string? Tarea_Nombre { get; set; }
    }

    public class InsSubContrataEntity
    {
        public int? SubContrata_Id { get; set; }
        public string? SubContrata_Nombre { get; set; }
    }

    public class InsJefeAreaEntity
    {
        public int? Jefe_Id { get; set; }
        public string? Jef_Nombre { get; set; }
        public string? Jef_DNI { get; set; }
        public int? Cen_Cos_Id { get; set; }
        public string? Cen_Cos_Des { get; set; }
    }

    public class ObservacionPlaneadaEntity
    {
        public string? Usr_Cod { get; set; }
        public int? Cliente_Id { get; set; }
        public int? Subestacion_Id { get; set; }
        public int? SubContrata_Id { get; set; }
        public int? Jefe_Id { get; set; }
        public int? Motivo_Id { get; set; }
        public int? Clima_Id { get; set; }
        public int? Tarea_Id { get; set; }
        public string? Obs_Detalle { get; set; }
        public string? Obs_Actividad { get; set; }
        public string? Usr_Reg { get; set; }
    }

    public class ActualizarObservacionPlaneadaEntity
    {
        public string? Codigo_Obs { get; set; }
        public int? Cliente_Id { get; set; }
        public int? Subestacion_Id { get; set; }
        public int? SubContrata_Id { get; set; }
        public int? Jefe_Id { get; set; }
        public int? Motivo_Id { get; set; }
        public int? Clima_Id { get; set; }
        public int? Tarea_Id { get; set; }
        public string? Estado { get; set; }
        public string? Obs_Detalle { get; set; }
        public string? Obs_Actividad { get; set; }
        public string? Usr_Mod { get; set; }
    }

    public class EliminarObservacionPlaneadaEntity
    {
        public string? Codigo_Obs { get; set; }
        public string? Usr_Mod { get; set; }
    }

    public class ObservacionPlaneadaListadoEntity
    {
        public int? Observacion_Id { get; set; }
        public string? Codigo_Obs { get; set; }
        public string? Usr_Nom { get; set; }
        public string? Jef_Nombre { get; set; }
        public string? Cen_Cos_Des { get; set; }
        public string? Cliente_Nombre { get; set; }
        public string? Subestacion_Nombre { get; set; }
        public string? Motivo_Nombre { get; set; }
        public string? Obs_Detalle { get; set; }
    }

    public class ObservacionPlaneadaDetalleEntity
    {
        public string? Usr_Cod { get; set; }
        public string? Usr_Crg { get; set; }
        public string? Usr_Doc_Nro { get; set; }
        public string? Usr_Nom { get; set; }
        public int? Cliente_Id { get; set; }
        public string? Cliente_Nombre { get; set; }
        public int? Subestacion_Id { get; set; }
        public string? Subestacion_Nombre { get; set; }
        public int? SubContrata_Id { get; set; }
        public string? SubContrata_Nombre { get; set; }
        public int? Jefe_Id { get; set; }
        public string? Jef_Nombre { get; set; }
        public string? Jef_DNI { get; set; }
        public string? Cen_Cos_Des { get; set; }
        public int? Motivo_Id { get; set; }
        public string? Motivo_Nombre { get; set; }
        public string? Obs_Detalle { get; set; }
        public int? Clima_Id { get; set; }
        public string? Clima_Nombre { get; set; }
        public int? Tarea_Id { get; set; }
        public string? Tarea_Nombre { get; set; }
        public string? Obs_Actividad { get; set; }
        public string? Estado { get; set; }
    }
    // ── Tipos de Inspección ───────────────────────────────────────────
    public class InsTipoInspeccionEntity
    {
        public int? Tipo_Id { get; set; }
        public string? Tipo_Nombre { get; set; }
    }

    // ── Medio Ambiente ────────────────────────────────────────────────
    public class InsMedioAmbienteEntity
    {
        public string? Usr_Cod { get; set; }
        public int? Cliente_Id { get; set; }
        public int? Subestacion_Id { get; set; }
        public int? SubContrata_Id { get; set; }
        public int? Jefe_Id { get; set; }
        public string? Actividad { get; set; }
        public string? Orden_Trabajo { get; set; }
        public string? Procedimiento_Trabajo { get; set; }
        public int? Tipo_Id { get; set; }
        public string? Usr_Reg { get; set; }
    }

}