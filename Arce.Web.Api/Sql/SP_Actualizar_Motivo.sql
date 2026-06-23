CREATE OR ALTER PROCEDURE SP_Actualizar_Motivo
    @Motivo_Id INT,
    @Motivo_Nombre VARCHAR (255),
    @Estado CHAR (1),
    @Usr_Mod VARCHAR (55)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Ins_Motivo
    SET Motivo_Nombre = @Motivo_Nombre,
        Estado = @Estado,
        Usr_Mod = @Usr_Mod,
        Fec_Mod = GETDATE()
    WHERE Motivo_Id = @Motivo_Id
END
GO
