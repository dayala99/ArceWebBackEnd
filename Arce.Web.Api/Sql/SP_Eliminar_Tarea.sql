CREATE OR ALTER PROCEDURE SP_Eliminar_Tarea
    @Tarea_Id INT,
    @Usr_Mod VARCHAR (55)
AS
BEGIN
    UPDATE Ins_Tarea
    SET
        Estado  = 'I',
        Usr_Mod = @Usr_Mod,
        Fec_Mod = GETDATE()
    WHERE Tarea_Id = @Tarea_Id
END
GO
