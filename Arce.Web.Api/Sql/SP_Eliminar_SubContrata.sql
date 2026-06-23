CREATE OR ALTER PROCEDURE SP_Eliminar_SubContrata
    @SubContrata_Id INT,
    @Usr_Mod VARCHAR (55)
AS
BEGIN
    UPDATE Ins_SubContrata
    SET
        Estado = 'I',
        Usr_Mod = @Usr_Mod,
        Fec_Mod = GETDATE()
    WHERE SubContrata_Id = @SubContrata_Id
END
GO
