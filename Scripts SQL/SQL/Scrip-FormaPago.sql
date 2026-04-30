CREATE TABLE Lg_For_Pag
(
    For_Pag_Id INT IDENTITY(1, 1),
    For_Pag_Des VARCHAR(255),
    Flg_Est CHAR(1),
    Usr_Reg VARCHAR(55),
    Fec_Reg DATETIME,
    Usr_Mod VARCHAR(55) NULL,
    Fec_Mod DATETIME NULL
)
GO

ALTER PROCEDURE PA_Lg_For_Pag_S0001
@For_Pag_Id INT
,   @For_Pag_Des VARCHAR(255)
,   @Flg_Est CHAR(1)
AS
BEGIN
    SELECT *
    FROM Lg_For_Pag
    WHERE ((For_Pag_Id = @For_Pag_Id AND For_Pag_Id > 0) OR (@For_Pag_Id = 0 AND For_Pag_Id > 0))
    AND ((For_Pag_Des LIKE '%' + @For_Pag_Des + '%' AND For_Pag_Des <> '') OR (For_Pag_Des = '' AND For_Pag_Des <> ''))
    AND Flg_Est = @Flg_Est
END
GO

CREATE PROCEDURE PA_Lg_For_Pag_I0001
@For_Pag_Des VARCHAR(255)
,   @Usr_Reg VARCHAR(55)
,   @Codigo INT OUTPUT
,   @sMsj VARCHAR(55) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
            
            INSERT INTO Lg_For_Pag (For_Pag_Des, Flg_Est, Usr_Reg, Fec_Reg)
            VALUES (@For_Pag_Des, 'A', @Usr_Reg, GETDATE())

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

CREATE PROCEDURE PA_Lg_For_Pag_U0001
@For_Pag_Id INT
,   @For_Pag_Des VARCHAR(255)
,   @Flg_Est CHAR(1)
,   @Usr_Mod VARCHAR(55)
,   @Codigo INT OUTPUT
,   @sMsj VARCHAR(55) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

            UPDATE Lg_For_Pag SET For_Pag_Des = @For_Pag_Des, Flg_Est = @Flg_Est, Usr_Mod = @Usr_Mod,
            Fec_Mod = GETDATE()
            WHERE For_Pag_Id = @For_Pag_Id

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

SELECT * FROM  Lg_For_Pag
Go

EXEC PA_Lg_For_Pag_I0001 'Adelanto 15 dias', 'mayala', 0, ''
GO

EXEC PA_Lg_For_Pag_S0001 0, '', 'A'