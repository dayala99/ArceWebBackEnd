CREATE OR ALTER PROCEDURE SP_Filtrar_Cliente
    @Cliente_Id INT,
    @Cliente_Nombre VARCHAR(255),
    @Estado CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Cliente_Id,
        Cliente_Nombre,
        Estado
    FROM Ins_Cliente
    WHERE
        ((Cliente_Id = @Cliente_Id AND Cliente_Id > 0)
         OR (@Cliente_Id = 0 AND Cliente_Id > 0))

    AND ((Cliente_Nombre LIKE '%' + @Cliente_Nombre + '%'
          AND Cliente_Nombre <> '')
         OR (@Cliente_Nombre = '' AND Cliente_Nombre <> ''))

    AND Estado = @Estado
END
GO
