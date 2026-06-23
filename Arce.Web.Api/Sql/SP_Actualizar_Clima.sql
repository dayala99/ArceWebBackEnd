CREATE OR ALTER PROCEDURE SP_Actualizar_Clima
    @Clima_Id INT,
    @Clima_Nombre VARCHAR (255),
    @Estado CHAR (1),
    @Usr_Mod VARCHAR (55)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Ins_Clima
    SET Clima_Nombre = @Clima_Nombre,
        Estado = @Estado,
        Usr_Mod = @Usr_Mod,
        Fec_Mod = GETDATE()
    WHERE Clima_Id = @Clima_Id
END
GO
