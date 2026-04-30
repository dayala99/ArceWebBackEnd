CREATE TABLE Sg_Usuario
(
	Usr_Id INT IDENTITY(1, 1)
,	Usr_Cod VARCHAR(55)
,	Usr_Nom VARCHAR(255)
,	Flg_Est CHAR(1)
,	Usr_Reg VARCHAR(55)
,	Fec_Reg DATETIME
,	Usr_Mod VARCHAR(55) NULL
,	Fec_Mod DATETIME NULL
)
GO

CREATE PROCEDURE PA_Sg_Usuario_I0001
	@Usr_Cod	VARCHAR(55)
,	@Usr_Nom	VARCHAR(55)
,	@Usr_Reg	VARCHAR(55)
,	@Codigo		INT OUTPUT
,	@sMsj		VARCHAR(55) OUTPUT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			
			IF EXISTS(SELECT 1 FROM Sg_Usuario WHERE Usr_Cod = @Usr_Cod)
			BEGIN
				SET @Codigo = 1
				SET @sMsj = 'EL CODIGO DE USUARIO YA SE ENCUENTRA REGISTRADO'

				ROLLBACK TRANSACTION
				RETURN
			END

			INSERT INTO Sg_Usuario (Usr_Cod, Usr_Nom, Flg_Est, Usr_Reg, Fec_Reg)
			VALUES (@Usr_Cod, @Usr_Nom, 'A', @Usr_Reg, GETDATE())

			SET @Codigo = 0
			SET @sMsj = 'REGISTRADO CORRECTAMENTE'

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

EXEC PA_Sg_Usuario_I0001 'dayala', 'Dominic Ayala', 'sistemas', 0, ''
GO

--SELECT * 
--FROM Sg_Usuario 

CREATE PROCEDURE PA_Sg_Usuario_S0001
	@Usr_Id INT
,	@Usr_Cod VARCHAR(55)
,	@Usr_Nom VARCHAR(55)
,	@Flg_Est char(1)
AS
BEGIN
	SELECT *
	FROM Sg_Usuario
	WHERE ((Usr_Id = @Usr_Id AND Usr_Id > 0) OR (@Usr_Id = 0 AND  Usr_Id > 0))
	AND ((Usr_Cod LIKE '%' + @Usr_Cod + '%' AND Usr_Cod <> '') OR (@Usr_Cod = '' AND Usr_Cod <> ''))
	AND ((Usr_Nom LIKE '%' + @Usr_Nom + '%' AND Usr_Nom <> '') OR (@Usr_Nom = '' AND Usr_Nom <> ''))
	AND Flg_Est = @Flg_Est
END
GO

EXEC PA_Sg_Usuario_S0001  0, '','', 'A' 
GO

CREATE PROCEDURE PA_Sg_Usuario_U0001
	@Usr_Id		INT	
,	@Usr_Cod	VARCHAR(55)
,	@Usr_Nom	VARCHAR(55)
,	@Flg_Est	CHAR(1)
,	@Usr_Mod	VARCHAR(55)
,	@Codigo		INT OUTPUT
,	@sMsj		VARCHAR(55) OUTPUT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

			UPDATE Sg_Usuario SET Usr_Cod = @Usr_Cod, Usr_Nom = @Usr_Nom, Flg_Est = @Flg_Est, Usr_Mod = @Usr_Mod, Fec_Mod = GETDATE()
			WHERE Usr_Id = @Usr_Id

			SET @Codigo = 0
			SET @sMsj = 'ACTUALIZADO CORRECTAMENTE'
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