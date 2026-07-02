CREATE OR ALTER PROCEDURE SP_Actualizar_Tipo_Inspeccion
    @Tipo_Id INT,
    @Tipo_Nombre VARCHAR(255),
    @Usr_Reg VARCHAR(55),
    @Estado CHAR(1)
AS
BEGIN
    UPDATE Ins_Tipo_Inspeccion
    SET
        Tipo_Nombre = @Tipo_Nombre,
        Estado      = @Estado,
        Usr_Reg     = @Usr_Reg,
        Fec_Reg     = GETDATE()
    WHERE Tipo_Id = @Tipo_Id;
END
GO
