CREATE OR ALTER PROCEDURE SP_Actualizar_Tarea
    @Tarea_Id INT,
    @Tarea_Nombre VARCHAR (55),
    @Usr_Mod VARCHAR (55),
    @Estado CHAR (1)
AS
BEGIN	
    UPDATE Ins_Tarea
    SET
        Tarea_Nombre = @Tarea_Nombre,
        Estado       = @Estado,
        Usr_Mod      = @Usr_Mod,
        Fec_Mod      = GETDATE()
    WHERE Tarea_Id = @Tarea_Id
END
GO
