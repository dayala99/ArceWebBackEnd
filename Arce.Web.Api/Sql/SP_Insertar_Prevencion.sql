CREATE PROCEDURE SP_Insertar_Inspeccion_Prevencion
    @Usr_Cod VARCHAR(55),
    @Cliente_Id INT,
    @Subestacion_Id INT,
    @SubContrata_Id INT,
    @Jefe_Id INT,
    @Actividad VARCHAR(55),
    @Orden_Trabajo VARCHAR(55),
    @Procedimiento_Trabajo VARCHAR(255),
    @Tipo_Id INT,
    @Usr_Reg VARCHAR(55)
AS
BEGIN
    INSERT INTO Ins_Inspecciones_Prevencion
    (
        Usr_Cod,
        Cliente_Id,
        Subestacion_Id,
        SubContrata_Id,
        Jefe_Id,
        Actividad,
        Orden_Trabajo,
        Procedimiento_Trabajo,
        Tipo_Id,
        Usr_Reg,
        Fec_Reg,
        Estado
    )
    VALUES
    (
        @Usr_Cod,
        @Cliente_Id,
        @Subestacion_Id,
        @SubContrata_Id,
        @Jefe_Id,
        @Actividad,
        @Orden_Trabajo,
        @Procedimiento_Trabajo,
        @Tipo_Id,
        @Usr_Reg,
        GETDATE(),
        'A'
    );
END;
GO
