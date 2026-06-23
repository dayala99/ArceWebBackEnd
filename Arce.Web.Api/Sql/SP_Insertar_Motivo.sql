CREATE OR ALTER PROCEDURE SP_Insertar_Motivo
    @Motivo_Nombre VARCHAR (255),
    @Usr_Reg VARCHAR (55)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Ins_Motivo (Motivo_Nombre, Estado, Usr_Reg, Fec_Reg)
    VALUES (@Motivo_Nombre, 'A', @Usr_Reg, GETDATE())
END
GO
