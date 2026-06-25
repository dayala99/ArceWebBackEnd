CREATE PROCEDURE SP_Eliminar_Prevencion
    @Prevencion_Id INT,
    @Usr_Mod VARCHAR(20)
AS
BEGIN
    UPDATE Ins_Inspecciones_Prevencion
    SET
        Estado = 'I',
        Usr_Mod = @Usr_Mod,
        Fec_Mod = GETDATE()
    WHERE Prevencion_Id = @Prevencion_Id
END
GO
