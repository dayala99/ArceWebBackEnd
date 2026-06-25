CREATE PROCEDURE SP_Mostrar_Prevencion
    @Prevencion_Id INT
AS
BEGIN
    SELECT t1.Usr_Cod, t2.Usr_Doc_Nro, t2.Usr_Nom, t4.Cliente_Id, t4.Cliente_Nombre, t5.Subestacion_Id, t5.Subestacion_Nombre,
           t6.SubContrata_Id, t6.SubContrata_Nombre, t7.Jefe_Id, t7.Jef_Nombre, t7.Jef_DNI, t8.Cen_Cos_Des,
           t1.Actividad, t1.Orden_Trabajo, t1.Procedimiento_Trabajo, t9.Tipo_Id, t9.Tipo_Nombre, t1.Estado
    FROM Ins_Inspecciones_Prevencion t1
    JOIN Sg_Usuario t2 ON (t1.Usr_Cod = t2.Usr_Cod)
    JOIN Lg_Cen_Cos t3 ON (t2.Usr_Cen_Cos_Id = t3.Cen_Cos_Id)
    JOIN Ins_Cliente t4 ON (t1.Cliente_Id = t4.Cliente_Id)
    JOIN Ins_SubEstacion t5 ON (t1.Subestacion_Id = t5.Subestacion_Id)
    JOIN Ins_SubContrata t6 ON (t1.SubContrata_Id = t6.SubContrata_Id)
    JOIN Ins_Jefe t7 ON (t1.Jefe_Id = t7.Jefe_Id)
    JOIN Lg_Cen_Cos t8 ON (t8.Cen_Cos_Id = t7.Cen_Cos_Id)
    JOIN Ins_Tipo_Inspeccion t9 ON (t1.Tipo_Id = t9.Tipo_Id)
    WHERE t1.Prevencion_Id = @Prevencion_Id
END
GO
