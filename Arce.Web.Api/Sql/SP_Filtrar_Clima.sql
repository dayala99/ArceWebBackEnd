CREATE OR ALTER PROCEDURE SP_Filtrar_Clima
    @Clima_Id INT,
    @Clima_Nombre VARCHAR (255),
    @Estado CHAR (1)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        t1.Clima_Id,
        t1.Clima_Nombre,
        t1.Estado
    FROM Ins_Clima t1
    WHERE ((t1.Clima_Id = @Clima_Id AND t1.Clima_Id > 0)
            OR (@Clima_Id = 0 AND t1.Clima_Id > 0))
      AND ((t1.Clima_Nombre LIKE '%' + @Clima_Nombre + '%' AND t1.Clima_Nombre <> '')
            OR (@Clima_Nombre = '' AND t1.Clima_Nombre <> ''))
      AND t1.Estado = @Estado
END
GO
