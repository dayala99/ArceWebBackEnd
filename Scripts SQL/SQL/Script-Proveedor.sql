--CREAR BASE DE DATOS
--CREATE DATABASE ArceDataBase
--USE ArceDataBase
--GO	
--TABLA PROVEEDORES
CREATE TABLE Lg_Proveedor(
	Prv_Id						INT IDENTITY(1, 1)
,	Prv_Nom						VARCHAR(255)
,	Prv_Ruc						VARCHAR(15) PRIMARY KEY
,	Prv_Tel						VARCHAR(15)
,	Prv_Dir						VARCHAR(255)
,	Prv_Nom_Con					VARCHAR(255)
,	Flg_Est						CHAR(1)
,	Usr_Reg						VARCHAR(55)
,	Fec_Reg						DATETIME
,	Usr_Mod						VARCHAR(55) NULL
,	Fec_Mod						DATETIME NULL
)
GO

--CRUD
SELECT * FROM Lg_Proveedor
GO

--LISTAR Y FILTRAR TODOS LOS PROVEEDORES ACTIVOS/INACTIVOS
CREATE PROCEDURE PA_Lg_Proveedor_S0001
	@Prv_Id			INT
,	@Prv_Nom		VARCHAR(255)
,	@Prv_Ruc		VARCHAR(15)
,	@Prv_Nom_Con	VARCHAR(255)
,	@Flg_Est		CHAR(1)
AS
BEGIN
	SELECT *
	FROM Lg_Proveedor
	WHERE ((Prv_Id = @Prv_Id AND Prv_Id > 0) OR (@Prv_Id = 0 AND  Prv_Id > 0))
	AND ((Prv_Nom LIKE '%' + @Prv_Nom + '%' AND Prv_Nom <> '') OR (@Prv_Nom = '' AND Prv_Nom <> ''))
	AND ((Prv_Ruc LIKE '%' + @Prv_Ruc + '%' AND Prv_Ruc <> '') OR (@Prv_Ruc = '' AND Prv_Ruc <> ''))
	AND ((Prv_Nom_Con LIKE '%' + @Prv_Nom_Con + '%' AND Prv_Ruc <> '') OR (@Prv_Nom_Con = '' AND Prv_Nom_Con <> ''))
	AND Flg_Est = @Flg_Est
END
GO

--REGISTRAR NUEVO PROVEEDOR
CREATE PROCEDURE PA_Lg_Proveedor_I0001
	@Prv_Nom			VARCHAR(255)
,	@Prv_Ruc			VARCHAR(15)
,	@Prv_Tel			VARCHAR(15)
,	@Prv_Dir			VARCHAR(255)
,	@Prv_Nom_Con		VARCHAR(255)
,	@Usr_Reg			VARCHAR(55)
,	@Codigo				INT	OUTPUT
,	@sMsj				VARCHAR(55) OUTPUT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

		IF EXISTS (SELECT 1 FROM Lg_Proveedor WHERE Prv_Ruc = @Prv_Ruc)
		BEGIN
			DECLARE @Cod_Prv INT
			
			SET @Cod_Prv = (SELECT Prv_Id FROM Lg_Proveedor WHERE Prv_Ruc = @Prv_Ruc)
			
			SET @Codigo = 1
			SET @sMsj = 'EL CLIENTE YA EXISTE CON EL C�DIGO: ' + CAST(@Cod_Prv AS CHAR(5))

			ROLLBACK TRANSACTION
			RETURN
		END


		INSERT INTO Lg_Proveedor(Prv_Nom, Prv_Ruc, Prv_Tel, Prv_Dir, Prv_Nom_Con, Flg_Est, Usr_Reg, Fec_Reg)
		VALUES (@Prv_Nom, @Prv_Ruc, @Prv_Tel, @Prv_Dir, @Prv_Nom_Con, 'A', @Usr_Reg, GETDATE())

		SET @Codigo = 0
		SET @sMsj = 'PROVEEDOR REGISTRADO CORRECTAMENTE'

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK TRANSACTION
		END

		SET @Codigo = ERROR_NUMBER()
		SET @sMsj = ERROR_MESSAGE()
		
	END CATCH
END
GO

EXEC PA_Lg_Proveedor_I0001 'DOMINIC', '20198765432', '999999999', 'LOS PLANETAS','ELDOMINIC', 'SISTEMAS', 0, ''
GO

--MODIFICAR PROVEEDOR
ALTER PROCEDURE PA_Lg_Proveedor_U0001
	@Prv_Id				INT
,	@Prv_Nom			VARCHAR(255)
,	@Prv_Ruc			VARCHAR(15)
,	@Prv_Tel			VARCHAR(15)
,	@Prv_Dir			VARCHAR(255)
,	@Prv_Nom_Con		VARCHAR(255)
,	@Usr_Mod			VARCHAR(55)
,	@Flg_Est			CHAR(1)
,	@Codigo				INT	OUTPUT
,	@sMsj				VARCHAR(55) OUTPUT
AS
BEGIN
	BEGIN TRY
		UPDATE Lg_Proveedor SET Prv_Nom = @Prv_Nom, Prv_Ruc = @Prv_Ruc, Prv_Tel = @Prv_Tel, Prv_Dir = @Prv_Dir, Prv_Nom_Con = @Prv_Nom_Con,
								Flg_Est = @Flg_Est,	Usr_Mod = @Usr_Mod, Fec_Mod = GETDATE()
		WHERE Prv_Id = @Prv_Id

		SET @Codigo = 0
		SET @sMsj = 'ACTUALIZADO CORRECTAMENTE'
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK TRANSACTION
		END

		SET @Codigo = ERROR_NUMBER()
		SET @sMsj = 'ERROR AL ACTUALIZAR EL PROVEEDOR'
	END CATCH
END
GO

--DECLARAR PROVEEDOR COMO ACTIVO O INACTIVO
CREATE PROCEDURE PA_Lg_Provedor_U0002
	@Prv_Id				INT
,	@Flg_Est			CHAR(1)
,	@Codigo				INT	OUTPUT
,	@sMsj				VARCHAR(55) OUTPUT
AS
BEGIN
	BEGIN TRY

		UPDATE Lg_Proveedor SET Flg_Est = @Flg_Est
		WHERE Prv_Id = @Prv_Id

	END TRY
	BEGIN CATCH

		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK TRANSACTION
		END

		SET @Codigo = ERROR_NUMBER()
		SET @sMsj = 'ERROR AL DESHABILITAR PROVEEDOR'

	END CATCH
END