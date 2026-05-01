CREATE TABLE Lg_Pedido_Det
(
    Ped_Det_Id INT IDENTITY(1, 1),
    Ped_Cab_Id INT,
    Ped_Cod_Itm VARCHAR(55),
    Ped_Uni_Med VARCHAR(15),
    Ped_Can NUMERIC(13, 3),
    Ped_Cos_Uni NUMERIC(13, 3),
    Ped_Cos_Tot NUMERIC(13, 3),
    Usr_Reg VARCHAR(55),
    Fec_Reg DATETIME,
    Usr_Mod VARCHAR(55),
    Fec_Mod DATETIME
)
GO

CREATE PROCEDURE PA_Lg_Pedido_Det_S0001
@Ped_Cab_Id INT
AS
BEGIN
    SELECT * 
    FROM Lg_Pedido_Det 
    WHERE Ped_Cab_Id = @Ped_Cab_Id
END
GO

CREATE PROCEDURE PA_Lg_Pedido_Det_S0002
@Ped_Det_Id INT
AS
BEGIN
    SELECT *
    FROM Lg_Pedido_Det
    WHERE Ped_Det_Id = @Ped_Det_Id
END
GO

CREATE PROCEDURE PA_Lg_Pedido_Det_I0001
@Ped_Cab_Id INT
,   @Ped_Cod_Itm VARCHAR(55)
,   @Ped_Uni_Med VARCHAR(15)
,   @Ped_Can NUMERIC(13, 3)
,   @Ped_Cos_Uni NUMERIC(13, 3)
,   @Ped_Cos_Tot NUMERIC(13, 3)
,   @Usr_Reg VARCHAR(55)
,   @Codigo INT OUTPUT
,   @sMsj INT OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
            INSERT INTO Lg_Pedido_Det (Ped_Cab_Id, Ped_Cod_Itm, Ped_Uni_Med, Ped_Can, Ped_Cos_Uni, Ped_Cos_Tot, Usr_Reg, Fec_Reg)
            VALUES (@Ped_Cab_Id, @Ped_Cod_Itm, @Ped_Uni_Med, @Ped_Can, @Ped_Cos_Uni, @Ped_Cos_Tot, @Usr_Reg, GETDATE())

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

CREATE PROCEDURE PA_Lg_Pedido_Det_U0001
@Ped_Det_Id INT
--@Ped_Cab_Id INT
,   @Ped_Cod_Itm VARCHAR(55)
,   @Ped_Uni_Med VARCHAR(15)
,   @Ped_Can NUMERIC(13, 3)
,   @Ped_Cos_Uni NUMERIC(13, 3)
,   @Ped_Cos_Tot NUMERIC(13, 3)
,   @Usr_Mod VARCHAR(55)
,   @Codigo INT OUTPUT
,   @sMsj INT OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
            UPDATE Lg_Pedido_Det SET 
            Ped_Cod_Itm         = @Ped_Cod_Itm
            ,   Ped_Uni_Med     = @Ped_Uni_Med
            ,   Ped_Can         = @Ped_Can
            ,   Ped_Cos_Uni     = @Ped_Cos_Uni
            ,   Ped_Cos_Tot     = @Ped_Cos_Tot
            ,   Usr_Mod         = @Usr_Mod 
            ,   Fec_Mod         = GETDATE()
            WHERE Ped_Det_Id = @Ped_Det_Id

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