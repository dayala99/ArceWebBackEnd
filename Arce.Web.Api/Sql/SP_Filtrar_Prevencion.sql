CREATE PROCEDURE SP_Filtrar_Prevencion
    @Fecha_Desde DATETIME,
    @Fecha_Hasta DATETIME,
    @Estado CHAR(1)
AS
BEGIN
    SELECT t1.Prevencion_Id, t1.Prevencion_Cod, t2.Usr_Nom, t3.Jef_Nombre, t4.Cen_Cos_Des, t5.Cliente_Nombre,
           t6.Subestacion_Nombre, t1.Actividad, t1.Orden_Trabajo, t7.Tipo_Nombre
    FROM Ins_Inspecciones_Prevencion t1
    JOIN Sg_Usuario t2 ON (t1.Usr_Cod = t2.Usr_Cod)
    JOIN Ins_Jefe t3 ON (t1.Jefe_Id = t3.Jefe_Id)
    JOIN Lg_Cen_Cos t4 ON (t3.Cen_Cos_Id = t4.Cen_Cos_Id)
    JOIN Ins_Cliente t5 ON (t1.Cliente_Id = t5.Cliente_Id)
    JOIN Ins_SubEstacion t6 ON (t1.Subestacion_Id = t6.Subestacion_Id)
    JOIN Ins_Tipo_Inspeccion t7 ON (t1.Tipo_Id = t7.Tipo_Id)
    WHERE
        t1.Fec_Reg >= @Fecha_Desde
        AND t1.Fec_Reg < DATEADD(DAY, 1, @Fecha_Hasta)
        AND t1.Estado = @Estado
END
GO
