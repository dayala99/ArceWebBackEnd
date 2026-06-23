CREATE OR ALTER PROCEDURE SP_Insertar_SubContrata
    @SubContrata_Nombre VARCHAR (55),
    @Usr_Reg VARCHAR (55)
AS
BEGIN
    INSERT INTO Ins_SubContrata (SubContrata_Nombre, Estado, Usr_Reg, Fec_Reg)
    VALUES (@SubContrata_Nombre, 'A', @Usr_Reg, GETDATE())
END
GO
