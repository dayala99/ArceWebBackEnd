CREATE OR ALTER PROCEDURE SP_Filtrar_SubContrata
    @SubContrata_Id INT,
    @SubContrata_Nombre VARCHAR (255),
    @Estado CHAR (1)
AS
BEGIN
    SELECT t1.SubContrata_Id, t1.SubContrata_Nombre, t1.Estado
    FROM Ins_SubContrata t1
    WHERE ((t1.SubContrata_Id = @SubContrata_Id AND t1.SubContrata_Id > 0)
            OR (@SubContrata_Id = 0 AND t1.SubContrata_Id > 0))
        AND ((t1.SubContrata_Nombre LIKE '%' + @SubContrata_Nombre + '%' AND t1.SubContrata_Nombre <> '')
            OR (@SubContrata_Nombre = '' AND t1.SubContrata_Nombre <> ''))
        AND t1.Estado = @Estado
END
GO
