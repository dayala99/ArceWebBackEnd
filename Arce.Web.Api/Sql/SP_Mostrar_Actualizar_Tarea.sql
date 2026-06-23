CREATE OR ALTER PROCEDURE SP_Mostrar_Actualizar_Tarea
    @Tarea_Id INT
AS
BEGIN
    SELECT t1.Tarea_Id, t1.Tarea_Nombre, t1.Estado
    FROM Ins_Tarea t1
    WHERE t1.Tarea_Id = @Tarea_Id
END
GO
