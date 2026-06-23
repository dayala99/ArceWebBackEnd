CREATE OR ALTER PROCEDURE SP_Filtrar_Motivo
    @Motivo_Id INT,
    @Motivo_Nombre VARCHAR (255),
    @Estado CHAR (1)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        t1.Motivo_Id,
        t1.Motivo_Nombre,
        t1.Estado
    FROM Ins_Motivo t1
    WHERE ((t1.Motivo_Id = @Motivo_Id AND t1.Motivo_Id > 0)
            OR (@Motivo_Id = 0 AND t1.Motivo_Id > 0))
    AND ((t1.Motivo_Nombre LIKE '%' + @Motivo_Nombre + '%' AND t1.Motivo_Nombre <> '')
        OR (@Motivo_Nombre = '' AND t1.Motivo_Nombre <> ''))
    AND t1.Estado = @Estado
END
GO
