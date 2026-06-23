CREATE OR ALTER PROCEDURE Mostrar_Actualizar_Motivo
    @Motivo_Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        t1.Motivo_Id,
        t1.Motivo_Nombre,
        t1.Estado
    FROM Ins_Motivo t1
    WHERE t1.Motivo_Id = @Motivo_Id
END
GO
