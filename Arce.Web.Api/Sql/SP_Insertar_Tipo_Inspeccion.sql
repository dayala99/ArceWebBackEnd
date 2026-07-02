CREATE OR ALTER PROCEDURE SP_Insertar_Tipo_Inspeccion
    @Tipo_Nombre VARCHAR(255),
    @Usr_Reg VARCHAR(55)
AS
BEGIN
    INSERT INTO Ins_Tipo_Inspeccion (Tipo_Nombre, Estado, Usr_Reg, Fec_Reg)
    VALUES (@Tipo_Nombre, 'A', @Usr_Reg, GETDATE());
END
GO
