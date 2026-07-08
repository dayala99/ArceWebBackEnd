ALTER PROCEDURE SP_Filtrar_We_Report
    @Fecha_Desde DATETIME,
    @Fecha_Hasta DATETIME,
    @Estado CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        t1.We_Report_Id,
        t1.Codigo_We_Report,
        CASE
            WHEN t1.Report_Anonimo = 'S' THEN 'Anónimo'
            ELSE t2.Usr_Nom
        END AS Usr_Nom,
        t3.Reporte_Tipo,
        t4.Cen_Cos_Des,
        t5.Cliente_Nombre,
        t1.Report_Descripcion,
        t1.Report_Acciones_Inmediata,
        t1.Report_Foto1_Ubicacion,
        t1.Report_Foto2_Ubicacion,
        t1.Report_Acciones_Propuestas,
        t1.Report_Potencial,
        t1.Report_Aplica
    FROM Ins_We_Report t1
    JOIN Sg_Usuario t2
        ON t1.Usr_Cod = t2.Usr_Cod
    JOIN Ins_Tipo_Reporte t3
        ON t1.Reporte_Id = t3.Reporte_Id
    JOIN Lg_Cen_Cos t4
        ON t1.Cen_Cos_Id = t4.Cen_Cos_Id
    JOIN Ins_Cliente t5
        ON t1.Cliente_Id = t5.Cliente_Id
    JOIN Ins_SubEstacion t6
        ON t1.Subestacion_Id = t6.Subestacion_Id
    WHERE
        t1.Fec_Reg >= @Fecha_Desde
        AND t1.Fec_Reg < DATEADD(DAY, 1, @Fecha_Hasta)
        AND t1.Estado = @Estado;
END
GO
