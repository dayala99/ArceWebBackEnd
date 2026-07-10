ALTER PROCEDURE SP_Filtrar_Stop_Work
    @Fecha_Desde DATETIME,
    @Fecha_Hasta DATETIME,
    @Estado CHAR(1)
AS
BEGIN
    SELECT
        t1.Stop_Work_Id,
        t1.Codigo_Stop_Work,
        t1.We_Report_Cod,
        t2.Usr_Nom,
        t3.Cen_Cos_Des,
        t4.Usr_Nom AS Stop_Supervisor_Nom,
        t1.Stop_Inspector,
        t5.Cliente_Nombre,
        t1.Stop_OT AS OT,
        t6.Tipo_Riesgo
    FROM Ins_Stop_Work t1
    JOIN Sg_Usuario t2
        ON (t1.Usr_Cod = t2.Usr_Cod)
    JOIN Lg_Cen_Cos t3
        ON (t2.Usr_Cen_Cos_Id = t3.Cen_Cos_Id)
    JOIN Sg_Usuario t4
        ON (t1.Stop_Supervisor = t4.Usr_Cod)
    JOIN Ins_Cliente t5
        ON (t1.Cliente_Id = t5.Cliente_Id)
    JOIN Ins_Tipo_Riesgo t6
        ON (t1.Tipo_Riesgo_Id = t6.Tipo_Riesgo_Id)
    WHERE
        t1.Fec_Reg >= @Fecha_Desde
        AND t1.Fec_Reg < DATEADD(DAY, 1, @Fecha_Hasta)
        AND t1.Estado = @Estado
END
GO

CREATE PROCEDURE SP_Eliminar_Stop_Work
    @Stop_Work_Id INT,
    @Usr_Mod VARCHAR (55)
AS
BEGIN
    UPDATE Ins_Stop_Work
    SET
        Estado  = 'I',
        Usr_Mod = @Usr_Mod,
        Fec_Mod = GETDATE()
    WHERE Stop_Work_Id = @Stop_Work_Id
END
GO

ALTER PROCEDURE SP_Mostrar_Actualizar_Stop_Work
    @Stop_Work_Id INT
AS
BEGIN
    SELECT t2.Codigo_We_Report, t3.Usr_Nom, t4.Cargo_Nombre, t5.Cen_Cos_Des, t6.Usr_Nom,
           t1.Stop_Inspector, t7.Cliente_Nombre, t8.Subestacion_Nombre, t1.Stop_OT,
           t1.Stop_Trabajo, t1.Stop_Procedimiento, t9.Tipo_Riesgo, t1.Estado
    FROM Ins_Stop_Work t1
    JOIN Ins_We_Report t2
    ON (t1.We_Report_Cod = t2.Codigo_We_Report)
    JOIN Sg_Usuario t3
    ON (t1.Usr_Cod = t3.Usr_Cod)
    JOIN Ins_Cargo t4
    ON (t3.Usr_Crg = t4.Cargo_Id)
    JOIN Lg_Cen_Cos t5
    ON (t3.Usr_Cen_Cos_Id = t5.Cen_Cos_Id)
    JOIN Sg_Usuario t6
    ON (t1.Stop_Supervisor = t6.Usr_Cod)
    JOIN Ins_Cliente t7
    ON (t1.Cliente_Id = t7.Cliente_Id)
    JOIN Ins_SubEstacion t8
    ON (t1.Subestacion_Id = t8.Subestacion_Id)
    JOIN Ins_Tipo_Riesgo t9
    ON (t1.Tipo_Riesgo_Id = t9.Tipo_Riesgo_Id)
    WHERE t1.Stop_Work_Id = @Stop_Work_Id
END
GO

CREATE PROCEDURE SP_Actualizar_Stop_Work
    @Stop_Work_Id INT,
    @Stop_Supervisor VARCHAR (55),
    @Stop_Inspector VARCHAR (255),
    @Cliente_Id INT,
    @Subestacion_Id INT,
    @Stop_OT VARCHAR (255),
    @Stop_Trabajo VARCHAR (255),
    @Stop_Procedimiento VARCHAR (255),
    @Tipo_Riesgo_Id INT,
    @Usr_Mod VARCHAR (55),
    @Estado CHAR (1)
AS
BEGIN
    UPDATE Ins_Stop_Work
    SET
        Stop_Supervisor    = @Stop_Supervisor,
        Stop_Inspector     = @Stop_Inspector,
        Cliente_Id         = @Cliente_Id,
        Subestacion_Id     = @Subestacion_Id,
        Stop_OT            = @Stop_OT,
        Stop_Trabajo       = @Stop_Trabajo,
        Stop_Procedimiento = @Stop_Procedimiento,
        Tipo_Riesgo_Id     = @Tipo_Riesgo_Id,
        Usr_Mod            = @Usr_Mod,
        Fec_Mod            = GETDATE(),
        Estado             = @Estado
    WHERE Stop_Work_Id = @Stop_Work_Id
END
GO
