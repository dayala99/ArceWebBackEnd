ALTER PROCEDURE SP_Mostrar_Medio_Ambiente
	@Medio_Ambiente_Id INT
AS
BEGIN
	SELECT
		-- Datos del supervisor (usuario que registró)
		t2.Usr_Nom,
		t3.Cen_Cos_Des,
		t2.Usr_Doc_Nro,
		-- Datos de la inspección
		t4.Cliente_Nombre,
		t5.Subestacion_Nombre,
		t6.SubContrata_Nombre,
		-- Datos del jefe (aliases explícitos para evitar colisión con supervisor)
		t7.Usr_Nom        AS Jef_Nombre,
		t7.Usr_Doc_Nro    AS Jef_DNI,
		t8.Cen_Cos_Des    AS Cen_Cos_Des_Jefe,
		t1.Jefe_Cod,
		-- Resto de campos
		t1.Actividad,
		t1.Orden_Trabajo,
		t1.Procedimiento_Trabajo,
		t9.Tipo_Nombre,
		t1.Estado
	FROM Ins_Inspecciones_Medio_Ambiente t1
	JOIN Sg_Usuario t2
		ON (t1.Usr_Cod = t2.Usr_Cod)
	JOIN Lg_Cen_Cos t3
		ON (t2.Usr_Cen_Cos_Id = t3.Cen_Cos_Id)
	JOIN Ins_Cliente t4
		ON (t1.Cliente_Id = t4.Cliente_Id)
	JOIN Ins_SubEstacion t5
		ON (t1.Subestacion_Id = t5.Subestacion_Id)
	JOIN Ins_SubContrata t6
		ON (t1.SubContrata_Id = t6.SubContrata_Id)
	JOIN Sg_Usuario t7
		ON (t1.Jefe_Cod = t7.Usr_Cod)
	JOIN Lg_Cen_Cos t8
		ON (t7.Usr_Cen_Cos_Id = t8.Cen_Cos_Id)
	JOIN Ins_Tipo_Inspeccion t9
		ON (t1.Tipo_Id = t9.Tipo_Id)
	WHERE t1.Medio_Ambiente_Id = @Medio_Ambiente_Id
END
GO
