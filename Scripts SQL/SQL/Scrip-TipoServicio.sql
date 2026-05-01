CREATE TABLE Lg_Tip_Ser
(
    Tip_Ser_Id INT IDENTITY(1, 1),
    Tip_Ser_Des VARCHAR(55),
    Flg_Est CHAR(1),
    Usr_Reg VARCHAR(55),
    Fec_Reg DATETIME,
    Usr_Mod VARCHAR(55) NULL,
    Fec_Mod DATETIME NULL
)
GO

CREATE PROCEDURE PA_Lg_Tip_Ser_S0001
@Tip_Ser_Id INT
,   @Tip_Ser_Des VARCHAR(55)
,   @Flg_Est CHAR(1)
AS
BEGIN
    SELECT *
    FROM Lg_Tip_Ser
    WHERE ((Tip_Ser_Id = @Tip_Ser_Id AND Tip_Ser_Id > 0) OR (Tip_Ser_Id = 0 AND Tip_Ser_Id > 0))
    AND ((Tip_Ser_Des = @Tip_Ser_Des AND Tip_Ser_Des <> '') OR (Tip_Ser_Des = '' AND Tip_Ser_Id <> ''))
    AND Flg_Est = @Flg_Est
END
GO

CREATE PROCEDURE PA_Lg_Tip_Ser_I0001
@Tip_Ser_Des VARCHAR(55)
,   @Usr_Reg VARCHAR(55)
,   @Codigo INT OUTPUT
,   @sMsj VARCHAR(55) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
            INSERT INTO Lg_Tip_Ser (Tip_Ser_Des, Flg_Est, Usr_Reg)
            VALUES (@Tip_Ser_Des, 'A', @Usr_Reg)

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

CREATE PROCEDURE PA_Lg_Tip_Ser_U0001
@Tip_Ser_Id INT
,   @Tip_Ser_Des VARCHAR(55)
,   @Flg_Est CHAR(1)
,   @Usr_Mod VARCHAR(55)
,   @Codigo INT OUTPUT
,   @sMsj VARCHAR(55) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
            UPDATE Lg_Tip_Ser SET Tip_Ser_Des = @Tip_Ser_Des, Flg_Est = @Flg_Est, Usr_Mod = @Usr_Mod
            WHERE Tip_Ser_Id = @Tip_Ser_Id

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

SELECT * FROM Lg_Tip_Ser
GO