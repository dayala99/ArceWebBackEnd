CREATE OR ALTER PROCEDURE SP_Filtrar_Tarea
    @Tarea_Id INT,
    @Tarea_Nombre VARCHAR (255),
    @Estado CHAR (1)
AS
BEGIN
    SELECT t1.Tarea_Id, t1.Tarea_Nombre, t1.Estado
    FROM Ins_Tarea t1
    WHERE ((t1.Tarea_Id = @Tarea_Id AND t1.Tarea_Id > 0)
            OR (@Tarea_Id = 0 AND t1.Tarea_Id > 0))
      AND ((t1.Tarea_Nombre LIKE '%' + @Tarea_Nombre + '%' AND t1.Tarea_Nombre <> '')
            OR (@Tarea_Nombre = '' AND t1.Tarea_Nombre <> ''))
      AND t1.Estado = @Estado
END
GO
