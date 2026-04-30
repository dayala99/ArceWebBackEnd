CREATE TABLE Lg_Pedido_Cab_Cen_Cos
(
    Ped_Cen_Cos_Id INT IDENTITY(1, 1),
    Ped_Id INT,
    Ped_Cen_Cos VARCHAR(55), 
    Ped_Can NUMERIC(13, 3)
)
GO

CREATE PROCEDURE PA_Lg_Pedido_Cab_Cen_Cos_S0001
@Ped_Id INT
AS
BEGIN
    SELECT *
    FROM Lg_Pedido_Cab_Cen_Cos
    WHERE Ped_Id = @Ped_Id
END
GO

CREATE PROCEDURE PA_Lg_Pedido_Cab_Cen_Cos_I0001
@Ped_Id INT
,   @Ped_Cen_Cos VARCHAR(55)
,   @Ped_Can NUMERIC(13, 3)
,   @Codigo INT OUTPUT
,   @sMsj VARCHAR(55) OUTPUT
AS
BEGIN
    BEGIN TRY
        INSERT INTO Lg_Pedido_Cab_Cen_Cos (Ped_Id, Ped_Cen_Cos, Ped_Can)
        VALUES (@Ped_Id, @Ped_Cen_Cos, @Ped_Can)

        SET @Codigo = 0
        SET @sMsj = 'REGISTRADO CORRECTAMENTE'
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

SELECT * FROM Lg_Pedido_Cab_Cen_Cos
GO