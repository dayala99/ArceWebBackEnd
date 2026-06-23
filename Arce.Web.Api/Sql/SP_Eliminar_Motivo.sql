CREATE OR ALTER PROCEDURE SP_Eliminar_Motivo
    @Motivo_Id INT,
    @Usr_Mod VARCHAR (55)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Ins_Motivo
    SET Estado = 'I',
        Usr_Mod = @Usr_Mod,
        Fec_Mod = GETDATE()
    WHERE Motivo_Id = @Motivo_Id
END
GO
