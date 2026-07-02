CREATE OR ALTER PROCEDURE SP_Mostrar_Actualizar_Tipo_Inspeccion
    @Tipo_Id INT
AS
BEGIN
    SELECT t1.Tipo_Nombre, t1.Estado
    FROM Ins_Tipo_Inspeccion t1
    WHERE t1.Tipo_Id = @Tipo_Id;
END
GO
