CREATE OR ALTER PROCEDURE SP_Filtrar_Tipo_Inspeccion
    @Tipo_Id INT,
    @Tipo_Nombre VARCHAR(255),
    @Estado CHAR(1)
AS
BEGIN
    SELECT t1.Tipo_Id, t1.Tipo_Nombre, t1.Estado
    FROM Ins_Tipo_Inspeccion t1
    WHERE (
            (@Tipo_Id = 0 AND t1.Tipo_Id > 0)
            OR (t1.Tipo_Id = @Tipo_Id AND t1.Tipo_Id > 0)
          )
      AND (
            (@Tipo_Nombre = '' AND t1.Tipo_Nombre <> '')
            OR (t1.Tipo_Nombre LIKE '%' + @Tipo_Nombre + '%')
          )
      AND t1.Estado = @Estado;
END
GO
