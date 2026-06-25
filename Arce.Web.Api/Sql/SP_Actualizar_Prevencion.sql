CREATE PROCEDURE SP_Actualizar_Prevencion
    @Prevencion_Id INT,
    @Usr_Cod VARCHAR(55),
    @Cliente_Id INT,
    @Subestacion_Id INT,
    @SubContrata_Id INT,
    @Jefe_Id INT,
    @Actividad VARCHAR (55),
    @Orden_Trabajo VARCHAR (55),
    @Procedimiento_Trabajo VARCHAR (255),
    @Tipo_Id INT,
    @Usr_Mod VARCHAR(20),
    @Estado CHAR (1)
AS
BEGIN
    UPDATE Ins_Inspecciones_Prevencion
    SET
        Usr_Cod               = @Usr_Cod,
        Cliente_Id            = @Cliente_Id,
        Subestacion_Id        = @Subestacion_Id,
        SubContrata_Id        = @SubContrata_Id,
        Jefe_Id               = @Jefe_Id,
        Actividad             = @Actividad,
        Orden_Trabajo         = @Orden_Trabajo,
        Procedimiento_Trabajo = @Procedimiento_Trabajo,
        Tipo_Id               = @Tipo_Id,
        Usr_Mod               = @Usr_Mod,
        Fec_Mod               = GETDATE(),
        Estado                = @Estado
    WHERE Prevencion_Id = @Prevencion_Id
END
GO
