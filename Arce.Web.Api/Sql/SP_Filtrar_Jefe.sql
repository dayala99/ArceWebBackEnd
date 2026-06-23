CREATE OR ALTER PROCEDURE SP_Filtrar_Jefe
    @Jefe_Id INT,
    @Jef_Nombre VARCHAR(100),
    @Jef_DNI VARCHAR(8),
    @Estado CHAR(1),
    @Cen_Cos_Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        t1.Jefe_Id,
        t1.Jef_Nombre,
        t1.Jef_DNI,
        t2.Cen_Cos_Des AS Area,
        t1.Estado,
        t1.Cen_Cos_Id
    FROM Ins_Jefe t1
    INNER JOIN Lg_Cen_Cos t2
        ON t1.Cen_Cos_Id = t2.Cen_Cos_Id
    WHERE
        ((t1.Jefe_Id = @Jefe_Id AND t1.Jefe_Id > 0)
         OR (@Jefe_Id = 0 AND t1.Jefe_Id > 0))
    AND ((t1.Jef_Nombre LIKE '%' + @Jef_Nombre + '%'
          AND t1.Jef_Nombre <> '')
         OR (@Jef_Nombre = '' AND t1.Jef_Nombre <> ''))
    AND ((t1.Jef_DNI LIKE '%' + @Jef_DNI + '%'
          AND t1.Jef_DNI <> '')
         OR (@Jef_DNI = '' AND t1.Jef_DNI <> ''))
    AND ((t1.Cen_Cos_Id = @Cen_Cos_Id
          AND t1.Cen_Cos_Id > 0)
         OR (@Cen_Cos_Id = 0 AND t1.Cen_Cos_Id > 0))
    AND t1.Estado = @Estado
END
GO
