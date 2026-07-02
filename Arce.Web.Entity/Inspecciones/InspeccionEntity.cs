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

    // El Jefe se identifica por Usr_Cod (string) — JOIN con Sg_Usuario filtrando cargo JEFE
    public class InsJefeAreaEntity
    {
        public string? Usr_Cod { get; set; }
        public string? Usr_Nom { get; set; }
    }

    // Datos auxiliares del jefe: área y DNI
    public class InsJefeDatosEntity
    {
        public string? Cen_Cos_Des { get; set; }
        public string? Usr_Doc_Nro { get; set; }
    }

    // Entity para INSERT de observación planeada
    public class ObservacionPlaneadaEntity
    {
        public string? Usr_Cod { get; set; }
        public int? Cliente_Id { get; set; }
        public int? Subestacion_Id { get; set; }
        public int? SubContrata_Id { get; set; }
        public string? Jefe_Cod { get; set; }
        public int? Motivo_Id { get; set; }
        public int? Clima_Id { get; set; }
        public int? Tarea_Id { get; set; }
        public string? Obs_Detalle { get; set; }
        public string? Obs_Actividad { get; set; }
        public string? Usr_Reg { get; set; }
    }

    // Entity para UPDATE de observación planeada
    public class ActualizarObservacionPlaneadaEntity
    {
        public string? Codigo_Obs { get; set; }
        public int? Cliente_Id { get; set; }
        public int? Subestacion_Id { get; set; }
        public int? SubContrata_Id { get; set; }
        public string? Jefe_Cod { get; set; }
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

    // Entity para el listado/filtrado de observaciones planeadas
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
        public string? Fec_Reg { get; set; }
        public string? Estado { get; set; }
    }

    // Entity para el detalle (mostrar/editar) de una observación planeada
    // Columnas alineadas con el SQL inline del repository (aliases explícitos)
    public class ObservacionPlaneadaDetalleEntity
    {
        // Datos del observador (usuario que registró)
        public string? Usr_Nom { get; set; }
        public string? Cen_Cos_Des { get; set; }
        public string? Cargo_Nombre { get; set; }
        public string? Usr_Doc_Nro { get; set; }

        // Datos de la observación
        public int? Cliente_Id { get; set; }
        public string? Cliente_Nombre { get; set; }
        public int? Subestacion_Id { get; set; }
        public string? Subestacion_Nombre { get; set; }
        public int? SubContrata_Id { get; set; }
        public string? SubContrata_Nombre { get; set; }

        // Datos del jefe (alias explícitos en el SQL)
        public string? Jefe_Cod { get; set; }
        public string? Jef_Nombre { get; set; }   // alias: t8.Usr_Nom AS Jef_Nombre
        public string? Jef_Area { get; set; }     // alias: t9.Cen_Cos_Des AS Jef_Area
        public string? Jef_DNI { get; set; }      // alias: t8.Usr_Doc_Nro AS Jef_DNI

        // Motivo, clima, tarea
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

    // ── Prevención ──────────────────────────────────────────────────
    public class InsPrevencionEntity
    {
        public string? Usr_Cod { get; set; }
        public int? Cliente_Id { get; set; }
        public int? Subestacion_Id { get; set; }
        public int? SubContrata_Id { get; set; }
        public string? Jefe_Cod { get; set; }
        public string? Actividad { get; set; }
        public string? Orden_Trabajo { get; set; }
        public string? Procedimiento_Trabajo { get; set; }
        public int? Tipo_Id { get; set; }
        public string? Usr_Reg { get; set; }
    }

    public class ActualizarPrevencionEntity
    {
        public int? Prevencion_Id { get; set; }
        public string? Usr_Cod { get; set; }
        public int? Cliente_Id { get; set; }
        public int? Subestacion_Id { get; set; }
        public int? SubContrata_Id { get; set; }
        public string? Jefe_Cod { get; set; }
        public string? Actividad { get; set; }
        public string? Orden_Trabajo { get; set; }
        public string? Procedimiento_Trabajo { get; set; }
        public int? Tipo_Id { get; set; }
        public string? Usr_Mod { get; set; }
        public string? Estado { get; set; }
    }

    public class EliminarPrevencionEntity
    {
        public int? Prevencion_Id { get; set; }
        public string? Usr_Mod { get; set; }
    }

    public class PrevencionListadoEntity
    {
        public int? Prevencion_Id { get; set; }
        public string? Prevencion_Cod { get; set; }
        public string? Usr_Nom { get; set; }
        public string? Jef_Nombre { get; set; }
        public string? Cen_Cos_Des { get; set; }
        public string? Cliente_Nombre { get; set; }
        public string? Subestacion_Nombre { get; set; }
        public string? Actividad { get; set; }
        public string? Orden_Trabajo { get; set; }
        public string? Tipo_Nombre { get; set; }
    }

    // Columnas alineadas con la lectura posicional del SP_Mostrar_Actualizar_Prevencion
    // (columnas duplicadas sin alias, se leen por posicion con SqlDataReader)
    public class PrevencionDetalleEntity
    {
        public string? Usr_Nom               { get; set; }  // pos 0: supervisor
        public string? Cen_Cos_Des           { get; set; }  // pos 1: area supervisor
        public string? Usr_Doc_Nro           { get; set; }  // pos 2: DNI supervisor
        public string? Cliente_Nombre        { get; set; }  // pos 3
        public string? Subestacion_Nombre    { get; set; }  // pos 4
        public string? SubContrata_Nombre    { get; set; }  // pos 5
        public string? Jef_Nombre            { get; set; }  // pos 6: jefe
        public string? Jef_DNI               { get; set; }  // pos 7: DNI jefe
        public string? Cen_Cos_Des_Jefe      { get; set; }  // pos 8: area jefe
        public string? Actividad             { get; set; }  // pos 9
        public string? Orden_Trabajo         { get; set; }  // pos 10
        public string? Procedimiento_Trabajo { get; set; }  // pos 11
        public string? Tipo_Nombre           { get; set; }  // pos 12
        public string? Estado                { get; set; }  // pos 13
    }

    // ── Medio Ambiente ────────────────────────────────────────────────
    public class InsMedioAmbienteEntity
    {
        public string? Usr_Cod { get; set; }
        public int? Cliente_Id { get; set; }
        public int? Subestacion_Id { get; set; }
        public int? SubContrata_Id { get; set; }
        public string? Jefe_Cod { get; set; }
        public string? Actividad { get; set; }
        public string? Orden_Trabajo { get; set; }
        public string? Procedimiento_Trabajo { get; set; }
        public int? Tipo_Id { get; set; }
        public string? Usr_Reg { get; set; }
    }

    // Listado filtrado — aliases explícitos para evitar columnas duplicadas del SP
    public class MedioAmbienteListadoEntity
    {
        public int? Medio_Ambiente_Id { get; set; }
        public string? Medio_Ambiente_Cod { get; set; }
        public string? Supervisor_Nom { get; set; }   // alias: t7.Usr_Nom AS Supervisor_Nom
        public string? Jef_Nombre { get; set; }       // alias: t2.Usr_Nom AS Jef_Nombre
        public string? Cen_Cos_Des { get; set; }      // alias: t3.Cen_Cos_Des AS Cen_Cos_Des (área del jefe)
        public string? Cliente_Nombre { get; set; }
        public string? Subestacion_Nombre { get; set; }
        public string? Actividad { get; set; }
        public string? Orden_Trabajo { get; set; }
        public string? Tipo_Nombre { get; set; }
    }

    public class MedioAmbienteDetalleEntity
    {
        public string? Usr_Cod { get; set; }
        public string? Cen_Cos_Des { get; set; }
        public string? Usr_Doc_Nro { get; set; }
        public string? Cliente_Nombre { get; set; }
        public string? Subestacion_Nombre { get; set; }
        public string? SubContrata_Nombre { get; set; }
        public string? Jef_Nombre { get; set; }
        public string? Jef_DNI { get; set; }
        public string? Cen_Cos_Des_Jefe { get; set; }
        public string? Actividad { get; set; }
        public string? Orden_Trabajo { get; set; }
        public string? Procedimiento_Trabajo { get; set; }
        public string? Tipo_Nombre { get; set; }
        public string? Estado { get; set; }
    }

    public class ActualizarMedioAmbienteEntity
    {
        public int? Medio_Ambiente_Id { get; set; }
        public string? Usr_Cod { get; set; }
        public int? Cliente_Id { get; set; }
        public int? Subestacion_Id { get; set; }
        public int? SubContrata_Id { get; set; }
        public string? Jefe_Cod { get; set; }
        public string? Actividad { get; set; }
        public string? Orden_Trabajo { get; set; }
        public string? Procedimiento_Trabajo { get; set; }
        public int? Tipo_Id { get; set; }
        public string? Usr_Mod { get; set; }
        public string? Estado { get; set; }
    }

    public class EliminarMedioAmbienteEntity
    {
        public int? Medio_Ambiente_Id { get; set; }
        public string? Usr_Mod { get; set; }
    }
}
