CREATE OR ALTER PROCEDURE SP_Mostrar_Actualizar_Clima
    @Clima_Id INT
AS
BEGIN
    SELECT t1.Clima_Id, t1.Clima_Nombre, t1.Estado
    FROM Ins_Clima t1
    WHERE t1.Clima_Id = @Clima_Id
END
GO
