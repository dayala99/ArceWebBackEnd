CREATE OR ALTER PROCEDURE SP_Eliminar_Clima
    @Clima_Id INT,
    @Usr_Mod VARCHAR (55)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Ins_Clima
    SET Estado = 'I',
        Usr_Mod = @Usr_Mod,
        Fec_Mod = GETDATE()
    WHERE Clima_Id = @Clima_Id
END
GO
