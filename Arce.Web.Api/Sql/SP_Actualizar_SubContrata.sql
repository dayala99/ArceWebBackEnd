CREATE OR ALTER PROCEDURE SP_Actualizar_SubContrata
    @SubContrata_Id INT,
    @SubContrata_Nombre VARCHAR (55),
    @Usr_Mod VARCHAR (55),
    @Estado CHAR (1)
AS
BEGIN
    UPDATE Ins_SubContrata
    SET
        SubContrata_Nombre = @SubContrata_Nombre,
        Estado = @Estado,
        Usr_Mod = @Usr_Mod,
        Fec_Mod = GETDATE()
    WHERE SubContrata_Id = @SubContrata_Id
END
GO
