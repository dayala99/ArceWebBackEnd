CREATE OR ALTER PROCEDURE SP_Mostrar_Actualizar_SubContrata
    @SubContrata_Id INT
AS
BEGIN
    SELECT t1.SubContrata_Nombre, t1.Estado
    FROM Ins_SubContrata t1
    WHERE t1.SubContrata_Id = @SubContrata_Id
END
GO
