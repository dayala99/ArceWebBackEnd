CREATE OR ALTER PROCEDURE SP_Eliminar_Tipo_Inspeccion
    @Tipo_Id INT,
    @Usr_Mod VARCHAR(55)
AS
BEGIN
    UPDATE Ins_Tipo_Inspeccion
    SET
        Estado  = 'I',
        Usr_Mod = @Usr_Mod,
        Fec_Mod = GETDATE()
    WHERE Tipo_Id = @Tipo_Id;
END
GO
