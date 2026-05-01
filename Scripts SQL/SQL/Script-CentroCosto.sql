CREATE TABLE Lg_Cen_Cos
(
    Cen_Cos_Id INT IDENTITY(1, 1),
    Cen_Cos_Des VARCHAR(255),
    Flg_Est CHAR(1),
    Usr_Reg VARCHAR(55),
    Fec_Reg DATETIME,
    Usr_Mod VARCHAR(55) NULL,
    Fec_Mod DATETIME NULL
)
GO

CREATE PROCEDURE PA_Lg_Cen_Cos_S0001
@Cen_Cos_Id INT
,   @Cen_Cos_Des VARCHAR(255)
,   @Flg_Est CHAR(1)
AS
BEGIN
    SELECT * 
    FROM Lg_Cen_Cos
    WHERE ((Cen_Cos_Id = @Cen_Cos_Id AND Cen_Cos_Id > 0) OR (@Cen_Cos_Id = 0 AND Cen_Cos_Id > 0))
    AND ((Cen_Cos_Des = @Cen_Cos_Des AND Cen_Cos_Des <> '') OR (@Cen_Cos_Des = '' AND Cen_Cos_Des <> ''))
    AND Flg_Est = @Flg_Est
END
GO

CREATE PROCEDURE PA_Lg_Cen_Cos_I0001
@Cen_Cos_Des VARCHAR(255)
,   @Usr_Reg VARCHAR(55)
,   @Codigo INT OUTPUT
,   @sMsj VARCHAR(55) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

            INSERT INTO Lg_Cen_Cos (Cen_Cos_Des, Flg_Est, Usr_Reg, Fec_Reg)
            VALUES (@Cen_Cos_Des, 'A', @Usr_Reg ,GETDATE())

            SET @Codigo = 0
            SET @sMsj = 'REGISTRADO CORRECTAMENTE'

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        
        IF @@TRANCOUNT > 0

        SET @Codigo = ERROR_NUMBER()
        SET @sMsj = ERROR_MESSAGE()

    END CATCH
END
GO

CREATE PROCEDURE PA_Lg_Cen_Cos_U0001
@Cen_Cos_Id INT
,   @Cen_Cos_Des VARCHAR(255)
,   @Flg_Est CHAR(1)
,   @Usr_Mod VARCHAR(55)
,   @Codigo INT OUTPUT
,   @sMsj VARCHAR(55) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

            UPDATE Lg_Cen_Cos SET Cen_Cos_Des = @Cen_Cos_Des, Flg_Est = @Flg_Est, Usr_Mod = @Usr_Mod, 
            Fec_Mod = GETDATE()
            WHERE Cen_Cos_Id = @Cen_Cos_Id

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0

        SET @Codigo = ERROR_NUMBER()
        SET @sMsj = ERROR_MESSAGE()
    END CATCH
END
GO

SELECT * FROM  Lg_Cen_Cos
Gos

EXEC PA_Lg_Cen_Cos_I0001 'Recursos Humanos', 'mayala', 0, ''
GO