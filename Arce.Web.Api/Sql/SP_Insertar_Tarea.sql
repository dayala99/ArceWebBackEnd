CREATE OR ALTER PROCEDURE SP_Insertar_Tarea
    @Tarea_Nombre VARCHAR (55),
    @Usr_Reg VARCHAR (55)
AS
BEGIN
    INSERT INTO Ins_Tarea (Tarea_Nombre, Estado, Usr_Reg, Fec_Reg)
    VALUES (@Tarea_Nombre, 'A', @Usr_Reg, GETDATE())
END
GO
